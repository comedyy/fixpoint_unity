using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nt.Deterministics;

public class StatisticsOp : MonoBehaviour
{
    public List<GameObject> _buttons;
    int _cur_state = 0;

    public UnityEngine.UI.Text console;
    public UnityEngine.UI.Text testAddData;

    private void Awake()
    {
        StatisticsManager.Instance.InitRandoms();
    }

    public void GenerateNumberLut()
    {
#if UNITY_EDITOR
        NumberLut.GenerateTables(Application.dataPath + "/../Packages/nt.deterministics/NT.Deterministics/Resources/NTLut");
#endif
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateConsole();
        if (StatisticsManager.Instance.uploadElkTask != null && StatisticsManager.Instance.uploadElkTask.IsCompleted)
        {
            StatisticsManager.Instance.uploadElkTask = null;
            EnableButtons(true);
        }
        if (_cur_state == 0)
            return;
        if (StatisticsManager.Instance.IsFinishStat())
        {
            _cur_state = 0;
            EnableButtons(true);
            ShowTestAddData();
        }
    }

    private void OnDestroy()
    {
        StatisticsManager.Instance.Destory();
    }

    public void OnBtnPlay()
    {
        StatisticsManager.execOrder = 0;
        StatisticsManager.Instance.Play();
        EnableButtons(false);
        _cur_state = 1;
    }

    void EnableButtons(bool bEnable)
    {
        if (null == _buttons)
            return;
        foreach (var btn in _buttons)
        {
            btn.SetActive(bEnable);
        }
    }

    public void OnBtnOutputCSV()
    {
        EnableButtons(false);
        StatisticsManager.Instance.exportCSV("D://performance.csv");
        EnableButtons(true);
    }

    public void OnBtnUploadELK()
    {
        EnableButtons(false);
        StatisticsManager.Instance.UploadELK();
        //EnableButtons(true);
    }

    private int lastLogCount = 0;
    private void UpdateConsole()
    {
        var logs = StatisticsManager.Instance.GetLogs();
        if (logs.Count > lastLogCount && null != console)
        {
            console.text = string.Join("\n", logs);
            lastLogCount = logs.Count;
        }
    }

    private void ShowTestAddData()
    {
        List<string> datas = new List<string>();
        foreach(var stat in StatisticsManager.Instance.opList)
        {
            if (null != stat && null != stat)
            {
                datas.Add($"{stat.stat.libname}:{stat.stat.ops_add}MOps/s");
            }
        }
        if (null != testAddData)
            testAddData.text = string.Join(";", datas);
    }
}
