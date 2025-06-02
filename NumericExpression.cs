using System;
using System.Collections.Generic;

public class NumericExpression
{
    private readonly IExpressionNode _root;

    private NumericExpression(IExpressionNode root)
    {
        _root = root;
    }

    public static NumericExpression Parse(string expression)
    {
        var parser = new ExpressionParser(expression);
        return new NumericExpression(parser.Parse());
    }

    public bool Evaluate(float x)
    {
        return _root.Evaluate(x);
    }

    internal static bool AreEqual(float a, float b)
    {
        return Math.Abs(a - b) < 0.0001f;
    }

    internal static bool NotEqual(float a, float b)
    {
        return Math.Abs(a - b) >= 0.0001f;
    }
}

interface IExpressionNode
{
    bool Evaluate(float x);
}

interface IValueNode
{
    float Evaluate(float x);
}

class ConstantNode : IValueNode
{
    private readonly float _value;

    public ConstantNode(float value)
    {
        _value = value;
    }

    public float Evaluate(float x) => _value;
}

class ConstantExpressionNode : IExpressionNode
{
    public readonly bool _value;

    public ConstantExpressionNode(bool value)
    {
        _value = value;
    }

    public bool Evaluate(float x) => _value;
}

class VariableNode : IValueNode
{
    public float Evaluate(float x) => x;
}

class BinaryValueNode : IValueNode
{
    private readonly IValueNode _left;
    private readonly IValueNode _right;
    private readonly Func<float, float, float> _operation;

    public BinaryValueNode(IValueNode left, IValueNode right, Func<float, float, float> operation)
    {
        _left = left;
        _right = right;
        _operation = operation;
    }

    public float Evaluate(float x)
    {
        return _operation(_left.Evaluate(x), _right.Evaluate(x));
    }
}

class ComparisonNode : IExpressionNode
{
    private readonly IValueNode _left;
    private readonly IValueNode _right;
    private readonly Func<float, float, bool> _operation;

    public ComparisonNode(IValueNode left, IValueNode right, Func<float, float, bool> operation)
    {
        _left = left;
        _right = right;
        _operation = operation;
    }

    public bool Evaluate(float x)
    {
        return _operation(_left.Evaluate(x), _right.Evaluate(x));
    }
}

class AndNode : IExpressionNode
{
    private readonly IExpressionNode _left;
    private readonly IExpressionNode _right;

    public AndNode(IExpressionNode left, IExpressionNode right)
    {
        if (left is ConstantExpressionNode cl)
        {
            _left = cl._value ? right : cl;
            _right = cl._value ? cl : right;
        }
        else if (right is ConstantExpressionNode cr)
        {
            _left = cr._value ? left : cr;
            _right = cr._value ? cr : left;
        }
        else
        {
            _left = left;
            _right = right;
        }
    }

    public bool Evaluate(float x)
    {
        return _left.Evaluate(x) && _right.Evaluate(x);
    }
}

class ExpressionParser
{
    private readonly string _input;
    private int _pos;

    public ExpressionParser(string input)
    {
        _input = input.Replace(" ", "");
        _pos = 0;
    }

    public IExpressionNode Parse()
    {
        var expressions = new List<IExpressionNode> { ParseComparison() };

        while (IsComparisonOperator())
        {
            string op = ParseOperator();
            var right = ParseArithmetic();
            expressions.Add(OptimizeComparison(new VariableNode(), right, op));
        }

        if (expressions.Count == 1)
            return expressions[0];

        IExpressionNode result = expressions[0];
        for (int i = 1; i < expressions.Count; i++)
        {
            result = new AndNode(result, expressions[i]);
        }
        return result;
    }

    private IExpressionNode ParseComparison()
    {
        var left = ParseArithmetic();

        if (!IsComparisonOperator())
            throw new Exception("Expected comparison operator");

        string op = ParseOperator();
        var right = ParseArithmetic();

        return OptimizeComparison(left, right, op);
    }

    private IValueNode ParseArithmetic()
    {
        var node = ParseTerm();

        while (Match("+") || Match("-"))
        {
            char op = _input[_pos++];
            var right = ParseTerm();
            node = OptimizeBinary(node, right, GetArithmeticOperation(op));
        }

        return node;
    }

    private IValueNode ParseTerm()
    {
        var node = ParseFactor();

        while (Match("*") || Match("/") || Match("%"))
        {
            char op = _input[_pos++];
            var right = ParseFactor();
            node = OptimizeBinary(node, right, GetArithmeticOperation(op));
        }

        return node;
    }

    private IValueNode ParseFactor()
    {
        if (Match("x") || Match("X")) { _pos++; return new VariableNode(); }

        if (Match("("))
        {
            _pos++;
            var expr = ParseArithmetic();
            if (!Match(")")) throw new Exception("Expected ')'");
            _pos++;
            return expr;
        }

        bool isNegative = false;
        if (Match("-"))
        {
            isNegative = true;
            _pos++;
        }

        int start = _pos;
        while (_pos < _input.Length && (char.IsDigit(_input[_pos]) || _input[_pos] == '.')) _pos++;
        string num = _input.Substring(start, _pos - start);
        if (float.TryParse(num, out float value))
            return new ConstantNode(isNegative ? -value : value);

        throw new Exception("Invalid number: " + num);
    }

    private string ParseOperator()
    {
        string[] ops = { "<=", ">=", "==", "!=", "<", ">" };
        foreach (var op in ops)
        {
            if (_input.Substring(_pos).StartsWith(op))
            {
                _pos += op.Length;
                return op;
            }
        }
        throw new Exception("Invalid operator at: " + _input.Substring(_pos));
    }

    private Func<float, float, bool> GetComparisonOperation(string op)
    {
        return op switch
        {
            "==" => NumericExpression.AreEqual,
            "!=" => NumericExpression.NotEqual,
            "<" => (a, b) => a < b,
            "<=" => (a, b) => a <= b,
            ">" => (a, b) => a > b,
            ">=" => (a, b) => a >= b,
            _ => throw new Exception("Unknown operator: " + op)
        };
    }

    private Func<float, float, float> GetArithmeticOperation(char op)
    {
        return op switch
        {
            '+' => (a, b) => a + b,
            '-' => (a, b) => a - b,
            '*' => (a, b) => a * b,
            '/' => (a, b) => a / b,
            '%' => (a, b) => a % b,
            _ => throw new Exception("Unknown arithmetic op: " + op)
        };
    }

    private bool Match(string token)
    {
        return _pos < _input.Length && _input.Substring(_pos).StartsWith(token);
    }

    private bool IsComparisonOperator()
    {
        string[] ops = { "<=", ">=", "==", "!=", "<", ">" };
        foreach (var op in ops)
        {
            if (_input.Substring(_pos).StartsWith(op))
                return true;
        }
        return false;
    }

    private IValueNode OptimizeBinary(IValueNode left, IValueNode right, Func<float, float, float> op)
    {
        if (left is ConstantNode cl && right is ConstantNode cr)
        {
            return new ConstantNode(op(cl.Evaluate(0), cr.Evaluate(0)));
        }
        return new BinaryValueNode(left, right, op);
    }

    private IExpressionNode OptimizeComparison(IValueNode left, IValueNode right, string op)
    {
        if (left is ConstantNode cl && right is ConstantNode cr)
        {
            var result = GetComparisonOperation(op)(cl.Evaluate(0), cr.Evaluate(0));
            return new ConstantExpressionNode(result);
        }
        return new ComparisonNode(left, right, GetComparisonOperation(op));
    }
}
