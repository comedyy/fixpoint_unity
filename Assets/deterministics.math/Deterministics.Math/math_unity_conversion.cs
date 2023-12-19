#if !UNITY_DOTSPLAYER
using UnityEngine;

#pragma warning disable 0660, 0661

namespace Deterministics.Math
{
    public partial struct fp2
    {
        /// <summary>
        /// Converts a float3 to Vector3.
        /// </summary>
        /// <param name="v">float3 to convert.</param>
        /// <returns>The converted Vector3.</returns>
        public static implicit operator Vector2(fp2 v)     { return new Vector2(v.x, v.y); }

        // /// <summary>
        // /// Converts a Vector3 to float3.
        // /// </summary>
        // /// <param name="v">Vector3 to convert.</param>
        // /// <returns>The converted float3.</returns>
        // public static implicit operator float3(Vector3 v)     { return new float3(v.x, v.y, v.z); }
    }

    public partial struct fp3
    {
        /// <summary>
        /// Converts a float3 to Vector3.
        /// </summary>
        /// <param name="v">float3 to convert.</param>
        /// <returns>The converted Vector3.</returns>
        public static implicit operator Vector3(fp3 v)     { return new Vector3(v.x, v.y, v.z); }

        // /// <summary>
        // /// Converts a Vector3 to float3.
        // /// </summary>
        // /// <param name="v">Vector3 to convert.</param>
        // /// <returns>The converted float3.</returns>
        // public static implicit operator float3(Vector3 v)     { return new float3(v.x, v.y, v.z); }
    }

    public partial struct fpQuaternion
    {
        /// <summary>
        /// Converts a quaternion to Quaternion.
        /// </summary>
        /// <param name="q">quaternion to convert.</param>
        /// <returns>The converted Quaternion.</returns>
        public static implicit operator Quaternion(fpQuaternion q)  { return new Quaternion(q.value.x, q.value.y, q.value.z, q.value.w); }

        // /// <summary>
        // /// Converts a Quaternion to quaternion.
        // /// </summary>
        // /// <param name="q">Quaternion to convert.</param>
        // /// <returns>The converted quaternion.</returns>
        // public static implicit operator quaternion(Quaternion q)  { return new quaternion(q.x, q.y, q.z, q.w); }
    }

    public partial struct fpRect
    {
        /// <summary>
        /// Converts a quaternion to Quaternion.
        /// </summary>
        /// <param name="q">quaternion to convert.</param>
        /// <returns>The converted Quaternion.</returns>
        public static implicit operator Rect(fpRect rect)  { return new Rect(rect.x, rect.y, rect.width, rect.height); }

        // /// <summary>
        // /// Converts a Quaternion to quaternion.
        // /// </summary>
        // /// <param name="q">Quaternion to convert.</param>
        // /// <returns>The converted quaternion.</returns>
        // public static implicit operator quaternion(Quaternion q)  { return new quaternion(q.x, q.y, q.z, q.w); }
    }
}
#endif
