using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using UnityEngine;
using Nt.Deterministics;
using Random = Nt.Deterministics.Random;

public interface IStatistics
{
    int execSort { get; }
    Statistics stat { get; set; }
    Dictionary<string, OpsItem> opsItems { get; set; }
    void Play();
}

public class StatisticsManager
{
    static StatisticsManager _instance;
    static string s_ELKURL = "http://10.0.2.113:8780/deterministics/_bulk";
    public static int execOrder = 0;
    string deviceName = "";
    public List<IStatistics> opList => _listOps;
    List<IStatistics> _listOps = new List<IStatistics>();
    protected ConcurrentQueue<string> _logs = new ConcurrentQueue<string>();
    public static StatisticsManager Instance
    {
        get {
            if (null == _instance)
            {
                _instance = new StatisticsManager();
                _instance.deviceName = SystemInfo.deviceName;
            }
            return _instance;
        }
    }

    public int opsCount => _listOps.Count;

    public int randomCount => 1000;
    public long[] randoms;

    public void InitRandoms()
    {
        var TS = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        var random = new Random((int)TS.TotalMilliseconds);
        randoms = new long[randomCount];
        for (int k = 0; k < randomCount; ++k)
        {
            randoms[k] = random.NextNumber(100000 * number.one).RawValue;
        }
    }

    public void SortOps()
    {
        _listOps.Sort((IStatistics a, IStatistics b) => a.execSort.CompareTo(b.execSort));
    }

    public void PlayAt(int idx)
    {
        if (idx >= 0 && idx < _listOps.Count)
            _listOps[idx].Play();
    }

    public void Play()
    {
        foreach(var statOP in _listOps)
        {
            statOP.Play();
        }
    }

    public void Destory()
    {
        _instance = null;
    }

    public void AddStatisticsOp(IStatistics stat)
    {
        _listOps.Add(stat);
    }

    public bool IsFinishStat()
    {
        if (null == _listOps || _listOps.Count == 0) return true;
        foreach (var stat in _listOps)
            if (null != stat.stat && !stat.stat.IsFinishStat)
                return false;
        return true;
    }

    public void AppendRow(StringBuilder str, string title, string fieldname)
    {
        FieldInfo finfo = null;
        str.Append(title + ",");
        foreach(var stat in _listOps)
        {
            if (null == finfo)
                finfo = stat.stat.GetType().GetField(fieldname, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (null == finfo) break;
            object obj = finfo.GetValue(stat);
            str.Append(obj.ToString() + ",");
        }
        str.Append("\n");
    }

    public void AppendLog(string log) => _logs.Enqueue(log);

    public ConcurrentQueue<string> GetLogs() => _logs;

    public string GenCSVData()
    {
        StringBuilder str = new StringBuilder();
        AppendRow(str, "", "libname");
        AppendRow(str, "+", "ops_add");
        AppendRow(str, "-", "ops_sub");
        AppendRow(str, "*", "ops_mul");
        AppendRow(str, "/", "ops_div");
        AppendRow(str, "++", "ops_self_inc");
        AppendRow(str, "--", "ops_self_sub");
        AppendRow(str, "%", "ops_mod");
        AppendRow(str, "==", "ops_equal");
        AppendRow(str, "!=", "ops_nequal");
        AppendRow(str, "<", "ops_less_than");
        AppendRow(str, "<=", "ops_less_equal_than");
        AppendRow(str, ">", "ops_great_than");
        AppendRow(str, ">=", "ops_great_equal_than");
        AppendRow(str, "<<", "ops_left_shift");
        AppendRow(str, ">>", "ops_right_shift");
        AppendRow(str, "AsLong", "ops_as_long");
        AppendRow(str, "AsInt", "ops_as_int");
        AppendRow(str, "AsFloat", "ops_as_float");
        AppendRow(str, "AsDouble", "ops_as_double");
        AppendRow(str, "AsDecimal", "ops_as_decimal");
        AppendRow(str, "IsPositiveInfinity", "ops_is_posInfinity");
        AppendRow(str, "IsNegativeInfinity", "ops_is_negInfinity");
        AppendRow(str, "IsInfinity", "ops_is_infinity");
        AppendRow(str, "IsNaN", "ops_is_nan");
        AppendRow(str, "ToString", "ops_to_string");
        AppendRow(str, "Sign", "ops_sign");
        AppendRow(str, "Abs", "ops_abs");
        AppendRow(str, "Floor", "ops_floor");
        AppendRow(str, "Ceiling", "ops_ceil");
        AppendRow(str, "Round", "ops_round");
        AppendRow(str, "Trunc", "ops_trunc");
        AppendRow(str, "Pow", "ops_pow");
        AppendRow(str, "Sqrt", "ops_sqrt");
        AppendRow(str, "Exp", "ops_exp");
        AppendRow(str, "Log", "ops_log");
        AppendRow(str, "Log2", "ops_log2");
        AppendRow(str, "Log10", "ops_log10");
        AppendRow(str, "Sin", "ops_sin");
        AppendRow(str, "Cos", "ops_cos");
        AppendRow(str, "Tan", "ops_tan");
        AppendRow(str, "Asin", "ops_asin");
        AppendRow(str, "ACos", "ops_acos");
        AppendRow(str, "Atan", "ops_atan");
        AppendRow(str, "Atan2", "ops_atan2");
        AppendRow(str, "Sinh", "ops_sinh");
        AppendRow(str, "Cosh", "ops_cosh");
        AppendRow(str, "Tanh", "ops_tanh");
        AppendRow(str, "Min", "ops_min");
        AppendRow(str, "Max", "ops_max");
        AppendRow(str, "Idx", "ops_idx");
        AppendRow(str, "Length", "ops_length");
        AppendRow(str, "Normalize", "ops_normalize");
        AppendRow(str, "Angle", "ops_angle");
        return str.ToString();
    }

    public void exportCSV(string path)
    {
        FileInfo finfo = new FileInfo(path);
        if (!finfo.Directory.Exists)
            Directory.CreateDirectory(finfo.Directory.FullName);
        File.WriteAllText(path, GenCSVData());
    }

    public void AddELKOpsItem(IStatistics stat, StatState state)
    {
        if (null == stat) return;
        if (null == stat.opsItems) stat.opsItems = new Dictionary<string, OpsItem>();
        string field_name = GetOpsFieldName(state);
        if (stat.opsItems.TryGetValue(field_name, out OpsItem item))
            item.execOrder = execOrder++;
        else
        {
            stat.opsItems.Add(field_name, new OpsItem() { 
                opName = field_name,
                execOrder = execOrder++
            });
        }
    }

    private string GetOpsGroup(string fieldname)
    {
        switch (fieldname)
        {
            case "ops_add":
            case "ops_sub":
            case "ops_mul":
            case "ops_div":
            case "ops_self_inc":
            case "ops_self_sub":
            case "ops_mod":
                return "基础运算";
            case "ops_equal":
            case "ops_nequal":
            case "ops_less_than":
            case "ops_less_equal_than":
            case "ops_great_than":
            case "ops_great_equal_than":
                return "判断式";
            case "ops_left_shift":
            case "ops_right_shift":
                return "移位操作";
            case "ops_as_long":
            case "ops_as_int":
            case "ops_as_float":
            case "ops_as_double":
            case "ops_as_decimal":
            case "ops_to_string":
                return "类型转换";
            case "ops_is_posInfinity":
            case "ops_is_negInfinity":
            case "ops_is_infinity":
            case "ops_is_nan":
                return "有效性判断";
            case "ops_sign":
            case "ops_abs":
            case "ops_floor":
            case "ops_ceil":
            case "ops_round":
            case "ops_trunc":
                return "取整";
            case "ops_pow":
            case "ops_sqrt":
            case "ops_exp":
                return "指数/求幂";
            case "ops_log":
            case "ops_log2":
            case "ops_log10":
                return "对数操作";
            case "ops_sin":
            case "ops_cos":
            case "ops_tan":
            case "ops_asin":
            case "ops_acos":
            case "ops_atan":
            case "ops_atan2":
                return "三角函数";
            case "ops_sinh":
            case "ops_cosh":
            case "ops_tanh":
                return "双曲函数";
            case "ops_min":
            case "ops_max":
                return "取最大/最小值";
            case "ops_length":
            case "ops_normalize":
            case "ops_angle":
                return "常用向量操作";
            case "ops_idx":
            default:
                return "其余";
        }
    }

    private string GetOpsFieldName(StatState state)
    {
        switch(state)
        {
            case StatState.Add: return "ops_add";
            case StatState.Sub: return "ops_sub";
            case StatState.Mul: return "ops_mul";
            case StatState.Div: return "ops_div";
            case StatState.Mod: return "ops_mod";
            case StatState.SelfInc: return "ops_self_inc";
            case StatState.SelfSub: return "ops_self_sub";
            case StatState.Equal: return "ops_equal";
            case StatState.NEqual: return "ops_nequal";
            case StatState.LessThan: return "ops_less_than";
            case StatState.LessEqualThan: return "ops_less_equal_than";
            case StatState.GreatThan: return "ops_great_than";
            case StatState.GreatEqualThan: return "ops_great_equal_than";
            case StatState.LeftShift: return "ops_left_shift";
            case StatState.RightShift: return "ops_right_shift";
            case StatState.AsLong: return "ops_as_long";
            case StatState.AsInt: return "ops_as_int";
            case StatState.AsFloat: return "ops_as_float";
            case StatState.AsDouble: return "ops_as_double";
            case StatState.AsDecimal: return "ops_as_decimal";
            case StatState.ToString: return "ops_to_string";
            case StatState.IsPositiveInfinity: return "ops_is_posInfinity";
            case StatState.IsNegativeInfinity: return "ops_is_negInfinity";
            case StatState.IsInfinity: return "ops_is_infinity";
            case StatState.IsNaN: return "ops_is_nan";
            case StatState.Sign: return "ops_sign";
            case StatState.Abs: return "ops_abs";
            case StatState.Floor: return "ops_floor";
            case StatState.Ceiling: return "ops_ceil";
            case StatState.Round: return "ops_round";
            case StatState.Trunc: return "ops_trunc";
            case StatState.Pow: return "ops_pow";
            case StatState.Sqrt: return "ops_sqrt";
            case StatState.Exp: return "ops_exp";
            case StatState.Log: return "ops_log";
            case StatState.Log2: return "ops_log2";
            case StatState.Log10: return "ops_log10";
            case StatState.Sin: return "ops_sin";
            case StatState.Cos: return "ops_cos";
            case StatState.Tan: return "ops_tan";
            case StatState.Asin: return "ops_asin";
            case StatState.ACos: return "ops_acos";
            case StatState.Atan: return "ops_atan";
            case StatState.Atan2: return "ops_atan2";
            case StatState.Sinh: return "ops_sinh";
            case StatState.Cosh: return "ops_cosh";
            case StatState.Tanh: return "ops_tanh";
            case StatState.Min: return "ops_min";
            case StatState.Max: return "ops_max";
            case StatState.Idx: return "ops_idx";
            case StatState.Length: return "ops_length";
            case StatState.Normalize: return "ops_normalize";
            case StatState.Angle: return "ops_angle";
            default: return "";
        };
        //switch (state)
        //{
        //    case StatState.Add:
        //    case StatState.Sub:
        //    case StatState.Mul:
        //    case StatState.Div:
        //    case StatState.SelfInc:
        //    case StatState.SelfSub:
        //    case StatState.Mod:
        //        return "基础运算";
        //    case StatState.Equal:
        //    case StatState.NEqual:
        //    case StatState.LessThan:
        //    case StatState.LessEqualThan:
        //    case StatState.GreatThan:
        //    case StatState.GreatEqualThan:
        //        return "判断式";
        //    case StatState.LeftShift:
        //    case StatState.RightShift:
        //        return "移位操作";
        //    case StatState.AsLong:
        //    case StatState.AsInt:
        //    case StatState.AsFloat:
        //    case StatState.AsDouble:
        //    case StatState.AsDecimal:
        //    case StatState.ToString:
        //        return "类型转换";
        //    case StatState.IsPositiveInfinity:
        //    case StatState.IsNegativeInfinity:
        //    case StatState.IsInfinity:
        //    case StatState.IsNaN:
        //        return "有效性判断";
        //    case StatState.Sign:
        //    case StatState.Abs:
        //    case StatState.Floor:
        //    case StatState.Ceiling:
        //    case StatState.Round:
        //    case StatState.Trunc:
        //        return "取整";
        //    case StatState.Pow:
        //    case StatState.Sqrt:
        //    case StatState.Exp:
        //        return "指数/求幂";
        //    case StatState.Log:
        //    case StatState.Log2:
        //    case StatState.Log10:
        //        return "对数操作";
        //    case StatState.Sin:
        //    case StatState.Cos:
        //    case StatState.Tan:
        //    case StatState.Asin:
        //    case StatState.ACos:
        //    case StatState.Atan:
        //    case StatState.Atan2:
        //        return "三角函数";
        //    case StatState.Sinh:
        //    case StatState.Cosh:
        //    case StatState.Tanh:
        //        return "双曲函数";
        //    case StatState.Min:
        //    case StatState.Max:
        //        return "取最大/最小值";
        //    case StatState.Length:
        //    case StatState.Normalize:
        //    case StatState.Angle:
        //        return "常用向量操作";
        //    case StatState.Idx:
        //    default:
        //        return "其余";
        //}
    }

    private OpsItem GenOpsItem(IStatistics IStat, string ops, string fieldname, int sort)
    {
        var stat = IStat.stat;
        FieldInfo finfo = stat.GetType().GetField(fieldname, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (null == finfo) return null;
        object obj = finfo.GetValue(stat);
        long idx = (this.deviceName + stat.libname + ops).GetHashCode();
        if (idx < 0)
            idx = Math.Abs(idx) + int.MaxValue;
        if (IStat.opsItems.TryGetValue(fieldname, out OpsItem item))
        {
            item.deviceName = this.deviceName;
            item.libName = stat.libname;
            item.opGroup = GetOpsGroup(fieldname);
            item.indexId = idx;
            item.opName = ops;
            item.ops = (float)obj;
            item.sort = sort;
        }
        else
        {
            item = new OpsItem()
            {
                deviceName = this.deviceName,
                libName = stat.libname,
                opGroup = GetOpsGroup(fieldname),
                indexId = idx,
                opName = ops,
                ops = (float)obj,
                sort = sort,
            };
        }
        return item;
    }

    public List<OpsItem> GenOpsItemsForElk(IStatistics stat)
    {
        List<OpsItem> list = new List<OpsItem>();
        int k = 1;
        list.Add(GenOpsItem(stat, "加+", "ops_add", k++));
        list.Add(GenOpsItem(stat, "减-", "ops_sub", k++));
        list.Add(GenOpsItem(stat, "乘*", "ops_mul", k++));
        list.Add(GenOpsItem(stat, "除/", "ops_div", k++));
        list.Add(GenOpsItem(stat, "取模%", "ops_mod", k++));
        list.Add(GenOpsItem(stat, "自增++", "ops_self_inc", k++));
        list.Add(GenOpsItem(stat, "自减--", "ops_self_sub", k++));
        list.Add(GenOpsItem(stat, "==", "ops_equal", k++));
        list.Add(GenOpsItem(stat, "!=", "ops_nequal", k++));
        list.Add(GenOpsItem(stat, "<", "ops_less_than", k++));
        list.Add(GenOpsItem(stat, "<=", "ops_less_equal_than", k++));
        list.Add(GenOpsItem(stat, ">", "ops_great_than", k++));
        list.Add(GenOpsItem(stat, ">=", "ops_great_equal_than", k++));
        list.Add(GenOpsItem(stat, "<<", "ops_left_shift", k++)); 
        list.Add(GenOpsItem(stat, ">>", "ops_right_shift", k++));
        list.Add(GenOpsItem(stat, "AsLong", "ops_as_long", k++));
        list.Add(GenOpsItem(stat, "AsInt", "ops_as_int", k++));
        list.Add(GenOpsItem(stat, "AsFloat", "ops_as_float", k++));
        list.Add(GenOpsItem(stat, "AsDouble", "ops_as_double", k++));
        list.Add(GenOpsItem(stat, "AsDecimal", "ops_as_decimal", k++));
        list.Add(GenOpsItem(stat, "ToString", "ops_to_string", k++));
        list.Add(GenOpsItem(stat, "IsPositiveInfinity", "ops_is_posInfinity", k++));
        list.Add(GenOpsItem(stat, "IsNegativeInfinity", "ops_is_negInfinity", k++));
        list.Add(GenOpsItem(stat, "IsInfinity", "ops_is_infinity", k++));
        list.Add(GenOpsItem(stat, "IsNaN", "ops_is_nan", k++));
        list.Add(GenOpsItem(stat, "Sign", "ops_sign", k++));
        list.Add(GenOpsItem(stat, "Abs", "ops_abs", k++));
        list.Add(GenOpsItem(stat, "Floor", "ops_floor", k++));
        list.Add(GenOpsItem(stat, "Ceiling", "ops_ceil", k++));
        list.Add(GenOpsItem(stat, "Round", "ops_round", k++));
        list.Add(GenOpsItem(stat, "Trunc", "ops_trunc", k++));
        list.Add(GenOpsItem(stat, "Pow", "ops_pow", k++));
        list.Add(GenOpsItem(stat, "Sqrt", "ops_sqrt", k++));
        list.Add(GenOpsItem(stat, "Exp", "ops_exp", k++));
        list.Add(GenOpsItem(stat, "Log", "ops_log", k++));
        list.Add(GenOpsItem(stat, "Log2", "ops_log2", k++));
        list.Add(GenOpsItem(stat, "Log10", "ops_log10", k++));
        list.Add(GenOpsItem(stat, "Sin", "ops_sin", k++));
        list.Add(GenOpsItem(stat, "Cos", "ops_cos", k++));
        list.Add(GenOpsItem(stat, "Tan", "ops_tan", k++));
        list.Add(GenOpsItem(stat, "Asin", "ops_asin", k++));
        list.Add(GenOpsItem(stat, "ACos", "ops_acos", k++));
        list.Add(GenOpsItem(stat, "Atan", "ops_atan", k++));
        list.Add(GenOpsItem(stat, "Atan2", "ops_atan2", k++));
        list.Add(GenOpsItem(stat, "Sinh", "ops_sinh", k++));
        list.Add(GenOpsItem(stat, "Cosh", "ops_cosh", k++));
        list.Add(GenOpsItem(stat, "Tanh", "ops_tanh", k++));
        list.Add(GenOpsItem(stat, "Min", "ops_min", k++));
        list.Add(GenOpsItem(stat, "Max", "ops_max", k++));
        list.Add(GenOpsItem(stat, "Idx", "ops_idx", k++));
        list.Add(GenOpsItem(stat, "Length", "ops_length", k++));
        list.Add(GenOpsItem(stat, "Normalize", "ops_normalize", k++));
        list.Add(GenOpsItem(stat, "Angle", "ops_angle", k++));
        return list;
    }

    public Task uploadElkTask = null;
    public void UploadELK()
    {
        uploadElkTask = Task.Factory.StartNew(() =>
        {
            try
            {
                byte[] postData = null;
                string result = "";
                foreach (var stat in _listOps)
                {
                    var items = GenOpsItemsForElk(stat);
                    string data = "";
                    foreach (var item in items)
                        data += item.ELKString();
                    postData = System.Text.Encoding.UTF8.GetBytes(data);
                    if (SendDataToElk(s_ELKURL, postData, out result))
                        Debug.Log(result);
                    else
                    {
                        Debug.LogError(result);
                        StatisticsManager.Instance.AppendLog(result);
                    }
                }
            }
            catch (Exception e)
            {
                StatisticsManager.Instance.AppendLog(e.Message + "\n" + e.StackTrace);
            }
        });
    }

    private static bool SendDataToElk(string url, byte[] postData, out string result)
    {
        if (string.IsNullOrEmpty(url) || null == postData || postData.Length <= 0)
        {
            result = "empty url or post data";
            return false;
        }
        bool bRet = true;
        result = "";
        try
        {
            var request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-ndjson";
            var stream = request.GetRequestStream();
            stream.Write(postData, 0, postData.Length);
            stream.Close();
            //request.GetResponseAsync();//无需等待结果

            //以下为等待结果版本，调试用
            var response = request.GetResponse();
            if (null != response && response is HttpWebResponse httpRes)
            {
                result = httpRes.StatusDescription;
                response.Dispose();
            }
        }
        catch (Exception e)
        {
            bRet = false;
            result = e.Message + "\n" + e.StackTrace;
        }
        return bRet;
    }
}
