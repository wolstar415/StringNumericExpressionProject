# StringNumericExpressionProject

        string expr1 = "x >= 50";
        var cond1 = NumericExpression.Parse(expr1);
        bool r1_1 = cond1.Evaluate(80); // ✅ 80 >= 50
        bool r1_2 = cond1.Evaluate(30); // ❌

        string expr2 = "x <= 30";
        var cond2 = NumericExpression.Parse(expr2);
        bool r2_1 = cond2.Evaluate(20); // ✅
        bool r2_2 = cond2.Evaluate(50); // ❌

        string expr3 = "1 <= x <= 10";
        var cond3 = NumericExpression.Parse(expr3);
        bool r3_1 = cond3.Evaluate(5);  // ✅
        bool r3_2 = cond3.Evaluate(11); // ❌

        string expr4 = "x % 2 == 1";
        var cond4 = NumericExpression.Parse(expr4);
        bool r4_1 = cond4.Evaluate(3); // ✅
        bool r4_2 = cond4.Evaluate(6); // ❌

        string expr5 = "x % 3 == 0";
        var cond5 = NumericExpression.Parse(expr5);
        bool r5_1 = cond5.Evaluate(6); // ✅
        bool r5_2 = cond5.Evaluate(7); // ❌

        string expr6 = "25 < x < 100";
        var cond6 = NumericExpression.Parse(expr6);
        bool r6_1 = cond6.Evaluate(50); // ✅
        bool r6_2 = cond6.Evaluate(10); // ❌

        string expr7 = "x == 1";
        var cond7 = NumericExpression.Parse(expr7);
        bool r7_1 = cond7.Evaluate(1f);     // ✅
        bool r7_2 = cond7.Evaluate(0.99f);  // ❌

        string expr8 = "x < 0.3";
        var cond8 = NumericExpression.Parse(expr8);
        bool r8_1 = cond8.Evaluate(0.2f); // ✅
        bool r8_2 = cond8.Evaluate(0.5f); // ❌

        string expr9 = "x * x >= 9";
        var cond9 = NumericExpression.Parse(expr9);
        bool r9_1 = cond9.Evaluate(3); // ✅
        bool r9_2 = cond9.Evaluate(2); // ❌

        string expr10 = "x >= 0.25";
        var cond10 = NumericExpression.Parse(expr10);
        bool r10_1 = cond10.Evaluate(0.3f); // ✅
        bool r10_2 = cond10.Evaluate(0.1f); // ❌

        string expr11 = "x + 5 == 10";
        var cond11 = NumericExpression.Parse(expr11);
        bool r11_1 = cond11.Evaluate(5); // ✅
        bool r11_2 = cond11.Evaluate(4); // ❌

        string expr12 = "x - 2 <= 3";
        var cond12 = NumericExpression.Parse(expr12);
        bool r12_1 = cond12.Evaluate(4); // ✅
        bool r12_2 = cond12.Evaluate(6); // ❌

        string expr13 = "x / 2 >= 5";
        var cond13 = NumericExpression.Parse(expr13);
        bool r13_1 = cond13.Evaluate(10); // ✅
        bool r13_2 = cond13.Evaluate(8);  // ❌

        string expr14 = "x % 4 == 2";
        var cond14 = NumericExpression.Parse(expr14);
        bool r14_1 = cond14.Evaluate(6); // ✅
        bool r14_2 = cond14.Evaluate(8); // ❌

        string expr15 = "x + x == 10";
        var cond15 = NumericExpression.Parse(expr15);
        bool r15_1 = cond15.Evaluate(5); // ✅
        bool r15_2 = cond15.Evaluate(4); // ❌

        string expr16 = "x * 0.5 < 2";
        var cond16 = NumericExpression.Parse(expr16);
        bool r16_1 = cond16.Evaluate(3); // ✅
        bool r16_2 = cond16.Evaluate(5); // ❌

        string expr17 = "x + 1 <= 6";
        var cond17 = NumericExpression.Parse(expr17);
        bool r17_1 = cond17.Evaluate(5); // ✅
        bool r17_2 = cond17.Evaluate(6); // ❌

        string expr18 = "x - 3 >= 0";
        var cond18 = NumericExpression.Parse(expr18);
        bool r18_1 = cond18.Evaluate(5); // ✅
        bool r18_2 = cond18.Evaluate(2); // ❌

        string expr19 = "x * 2 <= 10";
        var cond19 = NumericExpression.Parse(expr19);
        bool r19_1 = cond19.Evaluate(5); // ✅
        bool r19_2 = cond19.Evaluate(6); // ❌

        string expr20 = "x * x < 16";
        var cond20 = NumericExpression.Parse(expr20);
        bool r20_1 = cond20.Evaluate(3); // ✅
        bool r20_2 = cond20.Evaluate(5); // ❌

        string expr21 = "x / 3 == 2";
        var cond21 = NumericExpression.Parse(expr21);
        bool r21_1 = cond21.Evaluate(6); // ✅
        bool r21_2 = cond21.Evaluate(7); // ❌

        string expr22 = "x + 4 > 10";
        var cond22 = NumericExpression.Parse(expr22);
        bool r22_1 = cond22.Evaluate(7); // ✅
        bool r22_2 = cond22.Evaluate(5); // ❌

        string expr23 = "x * 3 == 9";
        var cond23 = NumericExpression.Parse(expr23);
        bool r23_1 = cond23.Evaluate(3); // ✅
        bool r23_2 = cond23.Evaluate(4); // ❌

        string expr24 = "x / 5 < 2";
        var cond24 = NumericExpression.Parse(expr24);
        bool r24_1 = cond24.Evaluate(9); // ✅
        bool r24_2 = cond24.Evaluate(11); // ❌

        string expr25 = "x - 10 >= -5";
        var cond25 = NumericExpression.Parse(expr25);
        bool r25_1 = cond25.Evaluate(5); // ✅
        bool r25_2 = cond25.Evaluate(4); // ❌

        string expr26 = "x + 2 < 5";
        var cond26 = NumericExpression.Parse(expr26);
        bool r26_1 = cond26.Evaluate(2); // ✅
        bool r26_2 = cond26.Evaluate(4); // ❌

        string expr27 = "x * x == 25";
        var cond27 = NumericExpression.Parse(expr27);
        bool r27_1 = cond27.Evaluate(5); // ✅
        bool r27_2 = cond27.Evaluate(4); // ❌

        string expr28 = "(x + 1) * 2 == 8";
        var cond28 = NumericExpression.Parse(expr28);
        bool r28_1 = cond28.Evaluate(3); // ✅
        bool r28_2 = cond28.Evaluate(2); // ❌

        string expr29 = "x * 2 + 1 == 9";
        var cond29 = NumericExpression.Parse(expr29);
        bool r29_1 = cond29.Evaluate(4); // ✅
        bool r29_2 = cond29.Evaluate(3); // ❌

        string expr30 = "x / 2 - 1 == 2";
        var cond30 = NumericExpression.Parse(expr30);
        bool r30_1 = cond30.Evaluate(6); // ✅
        bool r30_2 = cond30.Evaluate(5); // ❌
