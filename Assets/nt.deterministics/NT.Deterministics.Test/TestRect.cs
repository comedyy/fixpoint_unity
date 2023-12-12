using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Nt.Deterministics;


public class TestRect : MonoBehaviour
{
    public Rect ur;
    public rect myRect;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

#if UNITY_EDITOR
    // [Button("TestRect")]
    public static void BtnTestRect()
    {
        int i1 = 1 << 31;
        uint u1 = 1u << 31;
        int i2 = i1 ^ (int)u1;
        Rect r1 = new Rect(5f, 0f, -5f, 5f);
        Rect r2 = new Rect(0f, 0f, 5f, 6f);
        bool b = r1.Overlaps(r2);
        r2.x = 3.5f;
        b = r1.Overlaps(r2);
        r2.x = 5f;
        b = r1.Overlaps(r2);
        r2.width = -3f;
        b = r1.Overlaps(r2);
        b = r1.Overlaps(r2, true);
        r2.x = -1f;
        r2.width = 3f;
        b = r1.Overlaps(r2);
        b = r1.Overlaps(r2, true);
        r2.width = -3f;
        b = r1.Overlaps(r2);
        b = r1.Overlaps(r2, true);
        Debug.Log("Hello world!");
    }
#endif
}
