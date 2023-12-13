using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 测试LookRotation
public class TestRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Nt.Deterministics.NumberLut.Init((path)=>{
            return Resources.Load<TextAsset>(Path.Combine("NTLut", path)).bytes;
        });
        for(int i = 0; i < 10; i++)
        {
            var f1 = Random.insideUnitSphere.normalized;
            var f2 = Random.insideUnitSphere.normalized;
            Nt.Deterministics.fp3 f11 = new Nt.Deterministics.fp3(Nt.Deterministics.fp.ConvertFrom(f1.x), Nt.Deterministics.fp.ConvertFrom(f1.y), Nt.Deterministics.fp.ConvertFrom(f1.z));
            Nt.Deterministics.fp3 f12 = new Nt.Deterministics.fp3(Nt.Deterministics.fp.ConvertFrom(f2.x), Nt.Deterministics.fp.ConvertFrom(f2.y), Nt.Deterministics.fp.ConvertFrom(f2.z));

            var q1 = Nt.Deterministics.fpQuaternion.LookRotation(f11, f12);
            var q2 = Unity.Mathematics.quaternion.LookRotation(f1, f2);

            Debug.LogError(q1 +" "+ q2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
