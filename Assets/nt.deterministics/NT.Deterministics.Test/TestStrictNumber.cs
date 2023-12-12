// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Nt.Deterministics;

// public class TestStrictNumber : MonoBehaviour, IStatistics
// {
//     number[] _randoms;
//     float2[] _randomVectors;
//     int _random_count = 1000;
//     int _cur_state = 0;
//     int loop_count = 0;//_random_count x _random_count
//     public int sort;
//     public int execSort { get; set; }
//     public Statistics stat { get; set; }
//     public Dictionary<string, OpsItem> opsItems { get; set; } = new Dictionary<string, OpsItem>();
//     // Start is called before the first frame update
//     void Start()
//     {
//         execSort = sort;
//         stat = new Statistics() 
//         { 
//             libname = "NT Strict版本"
//         };
//         StatisticsManager.Instance.AddStatisticsOp(this);
//         InitRandoms();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (_cur_state == 0)
//             return;
//         StatState state = (StatState)_cur_state;
//         StatisticsManager.Instance.AddELKOpsItem(this, state);
//         switch (state)
//         {
//             case StatState.Idx:
//                 stat.ops_idx = testIdx();
//                 break;
//             case StatState.Add:
//                 stat.ops_add = testAdd();
//                 break;
//             case StatState.Sub:
//                 stat.ops_sub = testSub();
//                 break;
//             case StatState.Mul:
//                 stat.ops_mul = testMul();
//                 break;
//             case StatState.Div:
//                 stat.ops_div = testDiv();
//                 break;
//             case StatState.SelfInc:
//                 stat.ops_self_inc = testSelfInc();
//                 break;
//             case StatState.SelfSub:
//                 stat.ops_self_sub = testSelfSub();
//                 break;
//             case StatState.Mod:
//                 stat.ops_mod = testMod();
//                 break;
//             case StatState.Equal:
//                 stat.ops_equal = testEqual();
//                 break;
//             case StatState.NEqual:
//                 stat.ops_nequal = testNEqual();
//                 break;
//             case StatState.LessThan:
//                 stat.ops_less_than = testLessThan();
//                 break;
//             case StatState.LessEqualThan:
//                 stat.ops_less_equal_than = testLessEqualThan();
//                 break;
//             case StatState.GreatThan:
//                 stat.ops_great_than = testGreatThan();
//                 break;
//             case StatState.GreatEqualThan:
//                 stat.ops_great_equal_than = testGreatEqualThan();
//                 break;
//             case StatState.LeftShift:
//                 stat.ops_left_shift = testLeftShift();
//                 break;
//             case StatState.RightShift:
//                 stat.ops_right_shift = testRightShift();
//                 break;
//             case StatState.AsLong:
//                 stat.ops_as_long = testAsLong();
//                 break;
//             case StatState.AsInt:
//                 stat.ops_as_int = testAsInt();
//                 break;
//             case StatState.AsFloat:
//                 stat.ops_as_float = testAsFloat();
//                 break;
//             case StatState.AsDouble:
//                 stat.ops_as_double = testAsDouble();
//                 break;
//             case StatState.AsDecimal:
//                 stat.ops_as_decimal = testAsDecimal();
//                 break;
//             case StatState.IsPositiveInfinity:
//                 stat.ops_is_posInfinity = testIsPositiveInfinity();
//                 break;
//             case StatState.IsNegativeInfinity:
//                 stat.ops_is_negInfinity = testIsNegativeInfinity();
//                 break;
//             case StatState.IsInfinity:
//                 stat.ops_is_infinity = testIsInfinity();
//                 break;
//             case StatState.IsNaN:
//                 stat.ops_is_nan = testIsNaN();
//                 break;
//             case StatState.ToString:
//                 stat.ops_to_string = testToString();
//                 break;
//             case StatState.Sign:
//                 stat.ops_sign = testSign();
//                 break;
//             case StatState.Abs:
//                 stat.ops_abs = testAbs();
//                 break;
//             case StatState.Floor:
//                 stat.ops_floor = testFloor();
//                 break;
//             case StatState.Ceiling:
//                 stat.ops_ceil = testCeiling();
//                 break;
//             case StatState.Round:
//                 stat.ops_round = testRound();
//                 break;
//             case StatState.Trunc:
//                 stat.ops_trunc = testTrunc();
//                 break;
//             case StatState.Pow:
//                 stat.ops_pow = testPow();
//                 break;
//             case StatState.Sqrt:
//                 stat.ops_sqrt = testSqrt();
//                 break;
//             case StatState.Exp:
//                 stat.ops_exp = testExp();
//                 break;
//             case StatState.Log:
//                 stat.ops_log = testLog();
//                 break;
//             case StatState.Log2:
//                 stat.ops_log2 = testLog2();
//                 break;
//             case StatState.Log10:
//                 stat.ops_log10 = testLog10();
//                 break;
//             case StatState.Sin:
//                 stat.ops_sin = testSin();
//                 break;
//             case StatState.Cos:
//                 stat.ops_cos = testCos();
//                 break;
//             case StatState.Tan:
//                 stat.ops_tan = testTan();
//                 break;
//             case StatState.Asin:
//                 stat.ops_asin = testAsin();
//                 break;
//             case StatState.ACos:
//                 stat.ops_acos = testACos();
//                 break;
//             case StatState.Atan:
//                 stat.ops_atan = testAtan();
//                 break;
//             case StatState.Atan2:
//                 stat.ops_atan2 = testAtan2();
//                 break;
//             case StatState.Sinh:
//                 stat.ops_sinh = testSinh();
//                 break;
//             case StatState.Cosh:
//                 stat.ops_cosh = testCosh();
//                 break;
//             case StatState.Tanh:
//                 stat.ops_tanh = testTanh();
//                 break;
//             case StatState.Min:
//                 stat.ops_min = testMin();
//                 break;
//             case StatState.Max:
//                 stat.ops_max = testMax();
//                 break;
//             default:
//                 break;
//         }
//         stat.IsFinishStat = state == StatState.Finish;
//         if (!stat.IsFinishStat)
//             _cur_state++;
//         else if (StatisticsManager.Instance.IsFinishStat())
//         {
//             _cur_state = 0;
//         }
//     }

//     void InitRandoms()
//     {
//         _random_count = StatisticsManager.Instance.randomCount;
//         loop_count = _random_count * _random_count;
//         var tmp_randoms = StatisticsManager.Instance.randoms;
//         _randoms = new number[_random_count];
//         _randomVectors = new float2[_random_count];
//         long UsableMax = 1L << 31;
//         for (int k = 0; k < _random_count; ++k)
//         {
//             _randoms[k] = number.FromRaw(tmp_randoms[k] % UsableMax);
//         }
//         for (int k = 0; k < _random_count; ++k)
//         {
//             _randomVectors[k] = new float2(_randoms[(2 * k) % _random_count], _randoms[(2 * k + 1) % _random_count]);
//         }
//     }

//     public void Play()
//     {
//         _cur_state = 1;
//         stat.IsFinishStat = false;
//     }

//     float testIdx()
//     {
//         number num;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     num = _randoms[n];
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testAdd()
//     {
//         number num;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     num = number.StrictAdd(_randoms[m], _randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0);
//     }

//     float testSub()
//     {
//         number num;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     num = number.StrictSubstract(_randoms[m], _randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0);
//     }

//     float testMul()
//     {
//         number num;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     num = number.StrictMutiply(_randoms[m], _randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0);
//     }

//     float testDiv()
//     {
//         number num;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     num = number.StrictDivide(_randoms[m], _randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0);
//     }

//     float testSelfInc()
//     {
//         number num;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     num = number.StrictSelfInc(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testSelfSub()
//     {
//         number num;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     num = number.StrictSelfSub(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testMod()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     tmp = number.StrictMod(_randoms[m], _randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testEqual()
//     {
//         bool b;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     b = number.StrictEqualTo(_randoms[m], _randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testNEqual()
//     {
//         bool b;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     b = number.StrictNEqualTo(_randoms[m], _randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testLessThan()
//     {
//         bool b;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     b = number.StrictLessThan(_randoms[m], _randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testLessEqualThan()
//     {
//         bool b;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     b = number.StrictLessEqualThan(_randoms[m], _randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testGreatThan()
//     {
//         bool b;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     b = number.StrictGreatThan(_randoms[m], _randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testGreatEqualThan()
//     {
//         bool b;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     b = number.StrictGreatEqualThan(_randoms[m], _randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testLeftShift()
//     {
//         int bits = number.FRACTIONAL_PLACES;
//         number num;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     num = _randoms[n] << bits;
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testRightShift()
//     {
//         int bits = number.FRACTIONAL_PLACES;
//         number num;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     num = _randoms[n] >> bits;
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testAsLong()
//     {
//         long tmp;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     tmp = number.StrictAsLong(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testAsInt()
//     {
//         int tmp;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     tmp = number.StrictAsInt(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testAsFloat()
//     {
//         float tmp;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     tmp = number.StrictAsFloat(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testAsDouble()
//     {
//         double tmp;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     tmp = number.StrictAsDouble(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testAsDecimal()
//     {
//         decimal tmp;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     tmp = number.StrictAsDecimal(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testIsPositiveInfinity()
//     {
//         bool tmp;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     tmp = number.IsPositiveInfinity(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testIsNegativeInfinity()
//     {
//         bool tmp;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     tmp = number.IsNegativeInfinity(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testIsInfinity()
//     {
//         bool tmp;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     tmp = number.IsInfinity(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testIsNaN()
//     {
//         bool tmp;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     tmp = number.IsNaN(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testToString()
//     {
//         string tmp;
//         var time0 = DateTime.Now;
//         for (int m = 0; m < _random_count; ++m)
//             for (int n = 0; n < _random_count; ++n)
//                 tmp = _randoms[n].ToString();
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(loop_count / deltatime / 1000000.0f);
//     }

//     float testSign()
//     {
//         int tmp;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     tmp = number.Sign(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testAbs()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     tmp = number.Abs(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testFloor()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     tmp = number.StrictFloor(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testCeiling()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     tmp = number.StrictCeiling(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testRound()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     tmp = number.StrictRound(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testTrunc()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int k = 0; k < 10; ++k)
//             for (int m = 0; m < _random_count; ++m)
//                 for (int n = 0; n < _random_count; ++n)
//                     tmp = number.StrictTruncate(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(10 * loop_count / deltatime / 1000000.0f);
//     }

//     float testPow()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int m = 0; m < _random_count; ++m)
//             for (int n = 0; n < _random_count; ++n)
//                 tmp = number.StrictPow(_randoms[m], _randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(loop_count / deltatime / 1000000.0f);
//     }

//     float testSqrt()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int m = 0; m < _random_count; ++m)
//             for (int n = 0; n < _random_count; ++n)
//                 tmp = number.StrictSqrt(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(loop_count / deltatime / 1000000.0f);
//     }

//     float testExp()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int m = 0; m < _random_count; ++m)
//             for (int n = 0; n < _random_count; ++n)
//                 tmp = number.StrictExp(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(loop_count / deltatime / 1000000.0f);
//     }

//     float testLog()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int m = 0; m < _random_count; ++m)
//             for (int n = 0; n < _random_count; ++n)
//                 tmp = number.StrictLog(_randoms[m], _randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(loop_count / deltatime / 1000000.0f);
//     }

//     float testLog2()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int m = 0; m < _random_count; ++m)
//             for (int n = 0; n < _random_count; ++n)
//                 tmp = number.StrictLog2(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(loop_count / deltatime / 1000000.0f);
//     }

//     float testLog10()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int m = 0; m < _random_count; ++m)
//             for (int n = 0; n < _random_count; ++n)
//                 tmp = number.StrictLog10(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(loop_count / deltatime / 1000000.0f);
//     }

//     float testSin()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int m = 0; m < _random_count; ++m)
//             for (int n = 0; n < _random_count; ++n)
//                 tmp = number.StrictSin(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(loop_count / deltatime / 1000000.0f);
//     }

//     float testCos()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int m = 0; m < _random_count; ++m)
//             for (int n = 0; n < _random_count; ++n)
//                 tmp = number.StrictCos(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(loop_count / deltatime / 1000000.0f);
//     }

//     float testTan()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int m = 0; m < _random_count; ++m)
//             for (int n = 0; n < _random_count; ++n)
//                 tmp = number.StrictTan(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(loop_count / deltatime / 1000000.0f);
//     }

//     float testAsin()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int m = 0; m < _random_count; ++m)
//             for (int n = 0; n < _random_count; ++n)
//                 tmp = number.StrictAsin(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(loop_count / deltatime / 1000000.0f);
//     }

//     float testACos()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int m = 0; m < _random_count; ++m)
//             for (int n = 0; n < _random_count; ++n)
//                 tmp = number.StrictAcos(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(loop_count / deltatime / 1000000.0f);
//     }

//     float testAtan()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int m = 0; m < _random_count; ++m)
//             for (int n = 0; n < _random_count; ++n)
//                 tmp = number.StrictAtan(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(loop_count / deltatime / 1000000.0f);
//     }

//     float testAtan2()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int m = 0; m < _random_count; ++m)
//             for (int n = 0; n < _random_count; ++n)
//                 tmp = number.StrictAtan2(_randoms[m], _randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(loop_count / deltatime / 1000000.0f);
//     }

//     float testSinh()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int m = 0; m < _random_count; ++m)
//             for (int n = 0; n < _random_count; ++n)
//                 tmp = number.StrictSinh(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(loop_count / deltatime / 1000000.0f);
//     }

//     float testCosh()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int m = 0; m < _random_count; ++m)
//             for (int n = 0; n < _random_count; ++n)
//                 tmp = number.StrictCosh(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(loop_count / deltatime / 1000000.0f);
//     }

//     float testTanh()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int m = 0; m < _random_count; ++m)
//             for (int n = 0; n < _random_count; ++n)
//                 tmp = number.StrictTanh(_randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(loop_count / deltatime / 1000000.0f);
//     }

//     float testMin()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int m = 0; m < _random_count; ++m)
//             for (int n = 0; n < _random_count; ++n)
//                 tmp = number.Min(_randoms[m], _randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(loop_count / deltatime / 1000000.0f);
//     }

//     float testMax()
//     {
//         number tmp;
//         var time0 = DateTime.Now;
//         for (int m = 0; m < _random_count; ++m)
//             for (int n = 0; n < _random_count; ++n)
//                 tmp = number.Max(_randoms[m], _randoms[n]);
//         double deltatime = (DateTime.Now - time0).TotalSeconds;
//         return (float)(loop_count / deltatime / 1000000.0f);
//     }
// }
