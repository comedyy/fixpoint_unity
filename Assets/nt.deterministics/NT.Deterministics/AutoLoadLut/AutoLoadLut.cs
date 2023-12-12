
using UnityEngine;

namespace Nt.Deterministics
{
    public static class AutoLoadLut
    {
#if UNITY_2019_3_OR_NEWER
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
#else
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        public static void AutoLoad()
        {
#if NT_NUMBER_AUTOLOAD_LUT
            LoadLut();
#endif
        }

        public static void LoadLut()
        {
            if (!NumberLut.IsLoaded)
            {
                Debug.Log("Nt.Deterministics.AutoLoadLut::LoadLut - load lut");
                NumberLut.Init(file => UnityEngine.Resources.Load<TextAsset>("NTLut/" + file).bytes);
            }
        }

        public static void UnloadLut()
        {
            if (NumberLut.IsLoaded)
            {
                Debug.Log("Nt.Deterministics.AutoLoadLut::UnloadLut - unload lut");
                NumberLut.Dispose();
            }
        }
    }
}
