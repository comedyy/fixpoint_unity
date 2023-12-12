using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nt.Deterministics;
using Random = Nt.Deterministics.Random;

public class TestNumberPerformance : MonoBehaviour
{
    int loop_count = 250;
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning("TestNumberPerformance - Start");
    }

    // Update is called once per frame
    void Update()
    {
        // var random = new Random();
        // for (int k = 0; k < loop_count; ++k)
        // {
        //     //Random
        //     number n = random.NextNumber();
        //     var tmpn = random.NextNumber(number.one);
        //     tmpn = random.NextNumber(-number.one, number.one);
        //     //加减乘除
        //     n += 123165.9f;
        //     n -= 45646.3f;
        //     n *= 13164546f;
        //     n /= 456431.45f;
        //     //自增、自减
        //     n++;
        //     --n;
        //     //求余
        //     n %= 1321365f;
        //     //大小判断
        //     bool b = n == number.one;
        //     b = n != number.one;
        //     b = n > number.one;
        //     b = n < number.one;
        //     b = n >= number.one;
        //     b = n <= number.one;
        //     //移位操作
        //     n >>= 3;
        //     n <<= 4;
        //     //convert
        //     var l = (long)n;
        //     var f = (float)n;
        //     var i = (int)n;
        //     var d = (double)n;
        //     var de = (decimal)n;
        //     //IsPositiveInfinity、IsNegativeInfinity、IsInfinity、IsNaN
        //     b = number.IsPositiveInfinity(n);
        //     b = number.IsNegativeInfinity(n);
        //     b = number.IsInfinity(n);
        //     b = number.IsNaN(n);
        //     //Equal
        //     b = n.Equals(number.one);
        //     b = n.Equals(78.0f);
        //     i = n.CompareTo(number.zero);
        //     //ToString
        //     var str = n.ToString();
        //     //Sign、Abs、Floor、Ceiling、Round、Truncate
        //     i = number.Sign(n);
        //     n = number.Abs(n);
        //     n = number.Floor(n);
        //     n = number.Ceiling(123.1646546f - n);
        //     n = number.Round(n + 46465.46456f);
        //     n = number.Truncate(n - 465456.56465f);
        //     //Pow、Sqrt、Exp、Log、Log10
        //     n = number.Pow(number.EXP, (number)415.12f);
        //     n = number.Sqrt(n);
        //     n = number.Exp((number)415.12f);
        //     n = number.Log10(n);
        //     n = number.Log(n, number.EXP);
        //     //Sin, Cos, Tan, Asin, Acos, Atan, Atan2
        //     n = number.Sin(n);
        //     n = number.Cos(n + 1323f);
        //     n = number.Tan(n);
        //     n = number.Asin(number.one / 12.54f);
        //     n = number.Acos(number.one / 12.54f);
        //     n = number.Atan((number)13456.1254f);
        //     n = number.Atan2((number)132164.1321f, (number)164646.13f);
        //     //Sinh, Cosh, Tanh
        //     n = number.Sinh(number.one);
        //     n = number.Cosh(number.one);
        //     n = number.Tanh(number.one);
        //     //testNativeSqrt/testNativeDiv
        //     d = testNativeSqrt(d);
        //     f = testNativeDiv(f, 123.33f);
        //     //Debug.LogWarning("TestNumberPerformance - Update : final n = " + n.ToString());
        // }
    }

    double testNativeSqrt(double d)
    {
        return Math.Sqrt(d);
    }

    float testNativeDiv(float d1, float d2)
    {
        return d1 / d2;
    }
}
