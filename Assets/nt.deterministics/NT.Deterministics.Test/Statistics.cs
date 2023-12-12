using System.IO;
using UnityEngine;

public enum StatState
{
    None = 0,
    Idx,
    Add,
    Sub,
    Mul,
    Div,
    SelfInc,
    SelfSub,
    Mod,
    Equal,
    NEqual,
    LessThan,
    LessEqualThan,
    GreatThan,
    GreatEqualThan,
    LeftShift,
    RightShift,
    AsLong,
    AsInt,
    AsFloat,
    AsDouble,
    AsDecimal,
    IsPositiveInfinity,
    IsNegativeInfinity,
    IsInfinity,
    IsNaN,
    ToString,
    Sign,
    Abs,
    Floor,
    Ceiling,
    Round,
    Trunc,
    Pow,
    Sqrt,
    Exp,
    Log,
    Log2,
    Log10,
    Sin,
    Cos,
    Tan,
    Asin,
    ACos,
    Atan,
    Atan2,
    Sinh,
    Cosh,
    Tanh,
    Min,
    Max,
    Length,
    Normalize,
    Angle,

    Finish,
}

public class Statistics
{
    public bool IsFinishStat;
    //libname
    public string libname;
    //数组索引
    public float ops_idx;
    //加减乘除
    public float ops_add;
    public float ops_sub;
    public float ops_mul;
    public float ops_div;
    //自增、自减
    public float ops_self_inc;
    public float ops_self_sub;
    //求余
    public float ops_mod;
    //大小判断
    public float ops_equal;
    public float ops_nequal;
    public float ops_less_than;
    public float ops_less_equal_than;
    public float ops_great_than;
    public float ops_great_equal_than;
    //移位操作
    public float ops_left_shift;
    public float ops_right_shift;
    //convert
    public float ops_as_long;
    public float ops_as_int;
    public float ops_as_float;
    public float ops_as_double;
    public float ops_as_decimal;
    //IsPositiveInfinity、IsNegativeInfinity、IsInfinity、IsNaN
    public float ops_is_posInfinity;
    public float ops_is_negInfinity;
    public float ops_is_infinity;
    public float ops_is_nan;
    //ToString
    public float ops_to_string;
    //Sign、Abs、Floor、Ceiling、Round、Truncate
    public float ops_sign;
    public float ops_abs;
    public float ops_floor;
    public float ops_ceil;
    public float ops_round;
    public float ops_trunc;
    //Pow、Sqrt、Exp、Log、Log2、Log10
    public float ops_pow;
    public float ops_sqrt;
    public float ops_exp;
    public float ops_log;
    public float ops_log2;
    public float ops_log10;
    //Sin, Cos, Tan, Asin, Acos, Atan, Atan2
    public float ops_sin;
    public float ops_cos;
    public float ops_tan;
    public float ops_asin;
    public float ops_acos;
    public float ops_atan;
    public float ops_atan2;
    //Sinh, Cosh, Tanh
    public float ops_sinh;
    public float ops_cosh;
    public float ops_tanh;
    //Min, Max
    public float ops_min;
    public float ops_max;
    //Length, Normalize
    public float ops_length;
    public float ops_normalize;
    //Angle
    public float ops_angle;
}

public class OpsItem
{
    public long indexId;
    public string deviceName;
    public string libName;
    public string opGroup;
    public string opName;
    public float ops;
    public int execOrder;
    public int sort;
    public void WriteToFile(string jsonPath) => File.WriteAllText(jsonPath, this.ELKString());

    public string GetIndexHead() => "{\"index\":{\"_id\":\"" + indexId.ToString() + "\"}}";

    public string ELKString()
    {
        string head = GetIndexHead();
        string body = JsonUtility.ToJson(this);
        return head + "\r\n" + body + "\r\n";
    }
}
