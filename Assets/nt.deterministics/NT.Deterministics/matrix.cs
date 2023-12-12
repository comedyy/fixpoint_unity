using System.Runtime.CompilerServices;
using Unity.Mathematics;
using static Nt.Deterministics.math;
using RotationOrder = Unity.Mathematics.math.RotationOrder;

namespace Nt.Deterministics
{
    public partial struct float2x2
    {
        /// <summary>
        /// Computes a float2x2 matrix representing a counter-clockwise rotation by an angle in radians.
        /// </summary>
        /// <remarks>
        /// A positive rotation angle will produce a counter-clockwise rotation and a negative rotation angle will
        /// produce a clockwise rotation.
        /// </remarks>
        /// <param name="angle">Rotation angle in radians.</param>
        /// <returns>Returns the 2x2 rotation matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2x2 Rotate(number angle)
        {
            number s, c;
            sincos(angle, out s, out c);
            return float2x2(c, -s,
                            s,  c);
        }

        /// <summary>Returns a float2x2 matrix representing a uniform scaling of both axes by s.</summary>
        /// <param name="s">The scaling factor.</param>
        /// <returns>The float2x2 matrix representing uniform scale by s.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2x2 Scale(number s)
        {
            return float2x2(s,    number.zero,
                            number.zero, s);
        }

        /// <summary>Returns a float2x2 matrix representing a non-uniform axis scaling by x and y.</summary>
        /// <param name="x">The x-axis scaling factor.</param>
        /// <param name="y">The y-axis scaling factor.</param>
        /// <returns>The float2x2 matrix representing a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2x2 Scale(number x, number y)
        {
            return float2x2(x, number.zero,
                            number.zero, y);
        }

        /// <summary>Returns a float2x2 matrix representing a non-uniform axis scaling by the components of the float2 vector v.</summary>
        /// <param name="v">The float2 containing the x and y axis scaling factors.</param>
        /// <returns>The float2x2 matrix representing a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2x2 Scale(float2 v)
        {
            return Scale(v.x, v.y);
        }
    }

    public partial struct float3x3
    {
        /// <summary>
        /// Constructs a float3x3 from the upper left 3x3 of a float4x4.
        /// </summary>
        /// <param name="f4x4"><see cref="float4x4"/> to extract a float3x3 from.</param>
        public float3x3(float4x4 f4x4)
        {
            c0 = f4x4.c0.xyz;
            c1 = f4x4.c1.xyz;
            c2 = f4x4.c2.xyz;
        }

        /// <summary>Constructs a float3x3 matrix from a unit quaternion.</summary>
        /// <param name="q">The quaternion rotation.</param>
        public float3x3(quaternion q)
        {
            float4 v = new float4(q.x, q.y, q.z, q.w);
            float4 v2 = v + v;

            number signNeg = number.FromRaw(constant.sign64);
            float3 npn = new float3(signNeg, number.zero, signNeg);
            float3 nnp = new float3(signNeg, signNeg, number.zero);
            float3 pnn = new float3(number.zero,signNeg, signNeg);
            c0 = v2.y * v.yxw ^ npn - v2.z * v.zwx ^ pnn + float3(number.one, number.zero, number.zero);
            c1 = v2.z * v.wzy ^ nnp - v2.x * v.yxw ^ npn + float3(number.zero, number.one, number.zero);
            c2 = v2.x * v.zwx ^ pnn - v2.y * v.wzy ^ nnp + float3(number.zero, number.zero, number.one);
        }

        /// <summary>Constructs a float3x3 matrix from a unit quaternion.</summary>
        /// <param name="q">The quaternion rotation.</param>
        public float3x3(Unity.Mathematics.quaternion q)
        {
            float4 v = (float4)q.value;
            float4 v2 = v + v;

            number signNeg = number.FromRaw(constant.sign64);
            float3 npn = new float3(signNeg, number.zero, signNeg);
            float3 nnp = new float3(signNeg, signNeg, number.zero);
            float3 pnn = new float3(number.zero, signNeg, signNeg);
            c0 = v2.y * v.yxw ^ npn - v2.z * v.zwx ^ pnn + float3(number.one, number.zero, number.zero);
            c1 = v2.z * v.wzy ^ nnp - v2.x * v.yxw ^ npn + float3(number.zero, number.one, number.zero);
            c2 = v2.x * v.zwx ^ pnn - v2.y * v.wzy ^ nnp + float3(number.zero, number.zero, number.one);
        }

        /// <summary>
        /// Returns a float3x3 matrix representing a rotation around a unit axis by an angle in radians.
        /// The rotation direction is clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="axis">The rotation axis.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <returns>The float3x3 matrix representing the rotation around an axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 AxisAngle(float3 axis, number angle)
        {
            number sina, cosa;
            math.sincos(angle, out sina, out cosa);

            float3 u = axis;
            float3 u_yzx = u.yzx;
            float3 u_zxy = u.zxy;
            float3 u_inv_cosa = u - u * cosa;  // u * (1.0f - cosa);
            float4 t = float4(u * sina, cosa);

            number signNeg = number.FromRaw(constant.sign64);
            float3 ppn = float3(number.zero, number.zero, signNeg);
            float3 npp = float3(signNeg, number.zero, number.zero);
            float3 pnp = float3(number.zero, signNeg, number.zero);

            return float3x3(
                u.x * u_inv_cosa + t.wzy ^ ppn,
                u.y * u_inv_cosa + t.zwx ^ npp,
                u.z * u_inv_cosa + t.yxw ^ pnp
                );
            /*
            return float3x3(
                cosa + u.x * u.x * (1.0f - cosa),       u.y * u.x * (1.0f - cosa) - u.z * sina, u.z * u.x * (1.0f - cosa) + u.y * sina,
                u.x * u.y * (1.0f - cosa) + u.z * sina, cosa + u.y * u.y * (1.0f - cosa),       u.y * u.z * (1.0f - cosa) - u.x * sina,
                u.x * u.z * (1.0f - cosa) - u.y * sina, u.y * u.z * (1.0f - cosa) + u.x * sina, cosa + u.z * u.z * (1.0f - cosa)
                );
                */
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in x-y-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerXYZ(float3 xyz)
        {
            // return mul(rotateZ(xyz.z), mul(rotateY(xyz.y), rotateX(xyz.x)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float3x3(
                c.y * c.z,  c.z * s.x * s.y - c.x * s.z,    c.x * c.z * s.y + s.x * s.z,
                c.y * s.z,  c.x * c.z + s.x * s.y * s.z,    c.x * s.y * s.z - c.z * s.x,
                -s.y,       c.y * s.x,                      c.x * c.y
                );
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in x-z-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerXZY(float3 xyz)
        {
            // return mul(rotateY(xyz.y), mul(rotateZ(xyz.z), rotateX(xyz.x))); }
            float3 s, c;
            sincos(xyz, out s, out c);
            return float3x3(
                c.y * c.z,  s.x * s.y - c.x * c.y * s.z,    c.x * s.y + c.y * s.x * s.z,
                s.z,        c.x * c.z,                      -c.z * s.x,
                -c.z * s.y, c.y * s.x + c.x * s.y * s.z,    c.x * c.y - s.x * s.y * s.z
                );
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in y-x-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerYXZ(float3 xyz)
        {
            // return mul(rotateZ(xyz.z), mul(rotateX(xyz.x), rotateY(xyz.y)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float3x3(
                c.y * c.z - s.x * s.y * s.z,    -c.x * s.z, c.z * s.y + c.y * s.x * s.z,
                c.z * s.x * s.y + c.y * s.z,    c.x * c.z,  s.y * s.z - c.y * c.z * s.x,
                -c.x * s.y,                     s.x,        c.x * c.y
                );
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in y-z-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerYZX(float3 xyz)
        {
            // return mul(rotateX(xyz.x), mul(rotateZ(xyz.z), rotateY(xyz.y)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float3x3(
                c.y * c.z,                      -s.z,       c.z * s.y,
                s.x * s.y + c.x * c.y * s.z,    c.x * c.z,  c.x * s.y * s.z - c.y * s.x,
                c.y * s.x * s.z - c.x * s.y,    c.z * s.x,  c.x * c.y + s.x * s.y * s.z
                );
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// This is the default order rotation order in Unity.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in z-x-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerZXY(float3 xyz)
        {
            // return mul(rotateY(xyz.y), mul(rotateX(xyz.x), rotateZ(xyz.z)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float3x3(
                c.y * c.z + s.x * s.y * s.z,    c.z * s.x * s.y - c.y * s.z,    c.x * s.y,
                c.x * s.z,                      c.x * c.z,                      -s.x,
                c.y * s.x * s.z - c.z * s.y,    c.y * c.z * s.x + s.y * s.z,    c.x * c.y
                );
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in z-y-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerZYX(float3 xyz)
        {
            // return mul(rotateX(xyz.x), mul(rotateY(xyz.y), rotateZ(xyz.z)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float3x3(
                c.y * c.z,                      -c.y * s.z,                     s.y,
                c.z * s.x * s.y + c.x * s.z,    c.x * c.z - s.x * s.y * s.z,    -c.y * s.x,
                s.x * s.z - c.x * c.z * s.y,    c.z * s.x + c.x * s.y * s.z,    c.x * c.y
                );
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in x-y-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerXYZ(number x, number y, number z) { return EulerXYZ(float3(x, y, z)); }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in x-z-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerXZY(number x, number y, number z) { return EulerXZY(float3(x, y, z)); }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in y-x-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerYXZ(number x, number y, number z) { return EulerYXZ(float3(x, y, z)); }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in y-z-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerYZX(number x, number y, number z) { return EulerYZX(float3(x, y, z)); }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// This is the default order rotation order in Unity.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in z-x-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerZXY(number x, number y, number z) { return EulerZXY(float3(x, y, z)); }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in z-y-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerZYX(number x, number y, number z) { return EulerZYX(float3(x, y, z)); }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing 3 rotations around the principal axes in a given order.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
        /// Euler rotation constructors such as EulerZXY(...).
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <param name="order">The order in which the rotations are applied.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in the given order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 Euler(float3 xyz, RotationOrder order = RotationOrder.Default)
        {
            switch (order)
            {
                case RotationOrder.XYZ:
                    return EulerXYZ(xyz);
                case RotationOrder.XZY:
                    return EulerXZY(xyz);
                case RotationOrder.YXZ:
                    return EulerYXZ(xyz);
                case RotationOrder.YZX:
                    return EulerYZX(xyz);
                case RotationOrder.ZXY:
                    return EulerZXY(xyz);
                case RotationOrder.ZYX:
                    return EulerZYX(xyz);
                default:
                    return float3x3.identity;
            }
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing 3 rotations around the principal axes in a given order.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
        /// Euler rotation constructors such as EulerZXY(...).
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <param name="order">The order in which the rotations are applied.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in the given order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 Euler(number x, number y, number z, RotationOrder order = RotationOrder.Default)
        {
            return Euler(float3(x, y, z), order);
        }

        /// <summary>Returns a float3x3 matrix that rotates around the x-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the x-axis towards the origin in radians.</param>
        /// <returns>The float3x3 rotation matrix representing a rotation around the x-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 RotateX(number angle)
        {
            // {{1, 0, 0}, {0, c_0, -s_0}, {0, s_0, c_0}}
            number s, c;
            sincos(angle, out s, out c);
            return float3x3(number.one, number.zero, number.zero,
                            number.zero, c,    -s,
                            number.zero, s,    c);
        }

        /// <summary>Returns a float3x3 matrix that rotates around the y-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the y-axis towards the origin in radians.</param>
        /// <returns>The float3x3 rotation matrix representing a rotation around the y-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 RotateY(number angle)
        {
            // {{c_1, 0, s_1}, {0, 1, 0}, {-s_1, 0, c_1}}
            number s, c;
            sincos(angle, out s, out c);
            return float3x3(c, number.zero, s,
                            number.zero, number.one, number.zero,
                            -s, number.zero, c);
        }

        /// <summary>Returns a float3x3 matrix that rotates around the z-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the z-axis towards the origin in radians.</param>
        /// <returns>The float3x3 rotation matrix representing a rotation around the z-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 RotateZ(number angle)
        {
            // {{c_2, -s_2, 0}, {s_2, c_2, 0}, {0, 0, 1}}
            number s, c;
            sincos(angle, out s, out c);
            return float3x3(c,    -s, number.zero,
                            s,    c, number.zero,
                            number.zero, number.zero, number.one);
        }

        /// <summary>Returns a float3x3 matrix representing a uniform scaling of all axes by s.</summary>
        /// <param name="s">The uniform scaling factor.</param>
        /// <returns>The float3x3 matrix representing a uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 Scale(number s)
        {
            return float3x3(s, number.zero, number.zero,
                            number.zero, s, number.zero,
                            number.zero, number.zero, s);
        }

        /// <summary>Returns a float3x3 matrix representing a non-uniform axis scaling by x, y and z.</summary>
        /// <param name="x">The x-axis scaling factor.</param>
        /// <param name="y">The y-axis scaling factor.</param>
        /// <param name="z">The z-axis scaling factor.</param>
        /// <returns>The float3x3 rotation matrix representing a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 Scale(number x, number y, number z)
        {
            return float3x3(x, number.zero, number.zero,
                            number.zero, y, number.zero,
                            number.zero, number.zero, z);
        }

        /// <summary>Returns a float3x3 matrix representing a non-uniform axis scaling by the components of the float3 vector v.</summary>
        /// <param name="v">The vector containing non-uniform scaling factors.</param>
        /// <returns>The float3x3 rotation matrix representing a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 Scale(float3 v)
        {
            return Scale(v.x, v.y, v.z);
        }

        /// <summary>
        /// Returns a float3x3 view rotation matrix given a unit length forward vector and a unit length up vector.
        /// The two input vectors are assumed to be unit length and not collinear.
        /// If these assumptions are not met use float3x3.LookRotationSafe instead.
        /// </summary>
        /// <param name="forward">The forward vector to align the center of view with.</param>
        /// <param name="up">The up vector to point top of view toward.</param>
        /// <returns>The float3x3 view rotation matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 LookRotation(float3 forward, float3 up)
        {
            float3 t = normalizesafe(cross(up, forward));
            return float3x3(t, cross(forward, t), forward);
        }

        /// <summary>
        /// Returns a float3x3 view rotation matrix given a forward vector and an up vector.
        /// The two input vectors are not assumed to be unit length.
        /// If the magnitude of either of the vectors is so extreme that the calculation cannot be carried out reliably or the vectors are collinear,
        /// the identity will be returned instead.
        /// </summary>
        /// <param name="forward">The forward vector to align the center of view with.</param>
        /// <param name="up">The up vector to point top of view toward.</param>
        /// <returns>The float3x3 view rotation matrix or the identity matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 LookRotationSafe(float3 forward, float3 up)
        {
            number forwardLengthSq = dot(forward, forward);
            number upLengthSq = dot(up, up);

            forward *= rsqrt(forwardLengthSq);
            up *= rsqrt(upLengthSq);

            float3 t = cross(up, forward);
            number tLengthSq = dot(t, t);
            t *= rsqrt(tLengthSq);

            number mn = min(min(forwardLengthSq, upLengthSq), tLengthSq);
            number mx = max(max(forwardLengthSq, upLengthSq), tLengthSq);

            bool accept = mn > 1e-35f && mx < 1e35f && isfinite(forwardLengthSq) && isfinite(upLengthSq) && isfinite(tLengthSq);
            return float3x3(
                select(float3(number.one, number.zero, number.zero), t, accept),
                select(float3(number.zero, number.one, number.zero), cross(forward, t), accept),
                select(float3(number.zero, number.zero, number.one), forward, accept));
        }

        /// <summary>
        /// Converts a float4x4 to a float3x3.
        /// </summary>
        /// <param name="f4x4">The float4x4 to convert to a float3x3.</param>
        /// <returns>The float3x3 constructed from the upper left 3x3 of the input float4x4 matrix.</returns>
        public static explicit operator float3x3(float4x4 f4x4) => new float3x3(f4x4);
    }

    public partial struct float4x4
    {
        /// <summary>Constructs a float4x4 from a float3x3 rotation matrix and a float3 translation vector.</summary>
        /// <param name="rotation">The float3x3 rotation matrix.</param>
        /// <param name="translation">The translation vector.</param>
        public float4x4(float3x3 rotation, float3 translation)
        {
            c0 = float4(rotation.c0, number.zero);
            c1 = float4(rotation.c1, number.zero);
            c2 = float4(rotation.c2, number.zero);
            c3 = float4(translation, number.one);
        }

        /// <summary>Constructs a float4x4 from a quaternion and a float3 translation vector.</summary>
        /// <param name="rotation">The quaternion rotation.</param>
        /// <param name="translation">The translation vector.</param>
        public float4x4(quaternion rotation, float3 translation)
        {
            float3x3 rot = float3x3(rotation);
            c0 = float4(rot.c0, number.zero);
            c1 = float4(rot.c1, number.zero);
            c2 = float4(rot.c2, number.zero);
            c3 = float4(translation, number.one);
        }

        /// <summary>Constructs a float4x4 from a RigidTransform.</summary>
        /// <param name="transform">The RigidTransform.</param>
        public float4x4(RigidTransform transform)
        {
            float3x3 rot = float3x3(transform.rot);
            c0 = float4(rot.c0, number.zero);
            c1 = float4(rot.c1, number.zero);
            c2 = float4(rot.c2, number.zero);
            c3 = float4((float3)transform.pos, number.one);
        }

        /// <summary>
        /// Returns a float4x4 matrix representing a rotation around a unit axis by an angle in radians.
        /// The rotation direction is clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <returns>The float4x4 matrix representing the rotation about an axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 AxisAngle(float3 axis, number angle)
        {
            number sina, cosa;
            math.sincos(angle, out sina, out cosa);

            float4 u = float4(axis, number.zero);
            float4 u_yzx = u.yzxx;
            float4 u_zxy = u.zxyx;
            float4 u_inv_cosa = u - u * cosa;  // u * (1.0f - cosa);
            float4 t = float4(u.xyz * sina, cosa);

            number signNeg = number.FromRaw(constant.sign64);
            number AllF = ~number.zero;
            float4 ppnp = float4(number.zero, number.zero, signNeg, number.zero);
            float4 nppp = float4(signNeg, number.zero, number.zero, number.zero);
            float4 pnpp = float4(number.zero, signNeg, number.zero, number.zero);
            float4 mask = float4(AllF, AllF, AllF, number.zero);

            return float4x4(
                u.x * u_inv_cosa + t.wzyx ^ ppnp & mask,
                u.y * u_inv_cosa + t.zwxx ^ nppp & mask,
                u.z * u_inv_cosa + t.yxwx ^ pnpp & mask,
                float4(number.zero, number.zero, number.zero, number.one)
                );

        }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in x-y-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerXYZ(float3 xyz)
        {
            // return mul(rotateZ(xyz.z), mul(rotateY(xyz.y), rotateX(xyz.x)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float4x4(
                c.y * c.z,  c.z * s.x * s.y - c.x * s.z,    c.x * c.z * s.y + s.x * s.z, number.zero,
                c.y * s.z,  c.x * c.z + s.x * s.y * s.z,    c.x * s.y * s.z - c.z * s.x, number.zero,
                -s.y,       c.y * s.x,                      c.x * c.y,                   number.zero,
                number.zero,        number.zero,                    number.zero,         number.one
                );
        }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in x-z-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerXZY(float3 xyz)
        {
            // return mul(rotateY(xyz.y), mul(rotateZ(xyz.z), rotateX(xyz.x))); }
            float3 s, c;
            sincos(xyz, out s, out c);
            return float4x4(
                c.y * c.z,  s.x * s.y - c.x * c.y * s.z,    c.x * s.y + c.y * s.x * s.z, number.zero,
                s.z,        c.x * c.z,                      -c.z * s.x,                  number.zero,
                -c.z * s.y, c.y * s.x + c.x * s.y * s.z,    c.x * c.y - s.x * s.y * s.z, number.zero,
                number.zero,        number.zero,                    number.zero,         number.one
                );
        }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in y-x-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerYXZ(float3 xyz)
        {
            // return mul(rotateZ(xyz.z), mul(rotateX(xyz.x), rotateY(xyz.y)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float4x4(
                c.y * c.z - s.x * s.y * s.z,    -c.x * s.z, c.z * s.y + c.y * s.x * s.z, number.zero,
                c.z * s.x * s.y + c.y * s.z,    c.x * c.z,  s.y * s.z - c.y * c.z * s.x, number.zero,
                -c.x * s.y,                     s.x,        c.x * c.y,                   number.zero,
                number.zero,                    number.zero,        number.zero,         number.one
                );
        }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in y-z-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerYZX(float3 xyz)
        {
            // return mul(rotateX(xyz.x), mul(rotateZ(xyz.z), rotateY(xyz.y)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float4x4(
                c.y * c.z,                      -s.z,       c.z * s.y,                   number.zero,
                s.x * s.y + c.x * c.y * s.z,    c.x * c.z,  c.x * s.y * s.z - c.y * s.x, number.zero,
                c.y * s.x * s.z - c.x * s.y,    c.z * s.x,  c.x * c.y + s.x * s.y * s.z, number.zero,
                number.zero,                    number.zero,        number.zero,         number.one
                );
        }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// This is the default order rotation order in Unity.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in z-x-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerZXY(float3 xyz)
        {
            // return mul(rotateY(xyz.y), mul(rotateX(xyz.x), rotateZ(xyz.z)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float4x4(
                c.y * c.z + s.x * s.y * s.z,    c.z * s.x * s.y - c.y * s.z,    c.x * s.y,  number.zero,
                c.x * s.z,                      c.x * c.z,                      -s.x,       number.zero,
                c.y * s.x * s.z - c.z * s.y,    c.y * c.z * s.x + s.y * s.z,    c.x * c.y,  number.zero,
                number.zero,                            number.zero,            number.zero, number.one
                );
        }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in z-y-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerZYX(float3 xyz)
        {
            // return mul(rotateX(xyz.x), mul(rotateY(xyz.y), rotateZ(xyz.z)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float4x4(
                c.y * c.z,                      -c.y * s.z,                     s.y,        number.zero,
                c.z * s.x * s.y + c.x * s.z,    c.x * c.z - s.x * s.y * s.z,    -c.y * s.x, number.zero,
                s.x * s.z - c.x * c.z * s.y,    c.z * s.x + c.x * s.y * s.z,    c.x * c.y,  number.zero,
                number.zero,                            number.zero,            number.zero, number.one
                );
        }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in x-y-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerXYZ(number x, number y, number z) { return EulerXYZ(float3(x, y, z)); }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in x-z-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerXZY(number x, number y, number z) { return EulerXZY(float3(x, y, z)); }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in y-x-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerYXZ(number x, number y, number z) { return EulerYXZ(float3(x, y, z)); }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in y-z-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerYZX(number x, number y, number z) { return EulerYZX(float3(x, y, z)); }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// This is the default order rotation order in Unity.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in z-x-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerZXY(number x, number y, number z) { return EulerZXY(float3(x, y, z)); }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in z-y-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerZYX(number x, number y, number z) { return EulerZYX(float3(x, y, z)); }

        /// <summary>
        /// Returns a float4x4 constructed by first performing 3 rotations around the principal axes in a given order.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
        /// Euler rotation constructors such as EulerZXY(...).
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <param name="order">The order in which the rotations are applied.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in given order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 Euler(float3 xyz, RotationOrder order = RotationOrder.Default)
        {
            switch (order)
            {
                case RotationOrder.XYZ:
                    return EulerXYZ(xyz);
                case RotationOrder.XZY:
                    return EulerXZY(xyz);
                case RotationOrder.YXZ:
                    return EulerYXZ(xyz);
                case RotationOrder.YZX:
                    return EulerYZX(xyz);
                case RotationOrder.ZXY:
                    return EulerZXY(xyz);
                case RotationOrder.ZYX:
                    return EulerZYX(xyz);
                default:
                    return float4x4.identity;
            }
        }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing 3 rotations around the principal axes in a given order.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
        /// Euler rotation constructors such as EulerZXY(...).
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <param name="order">The order in which the rotations are applied.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in given order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 Euler(number x, number y, number z, RotationOrder order = RotationOrder.Default)
        {
            return Euler(float3(x, y, z), order);
        }

        /// <summary>Returns a float4x4 matrix that rotates around the x-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the x-axis towards the origin in radians.</param>
        /// <returns>The float4x4 rotation matrix that rotates around the x-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 RotateX(number angle)
        {
            // {{1, 0, 0}, {0, c_0, -s_0}, {0, s_0, c_0}}
            number s, c;
            sincos(angle, out s, out c);
            return float4x4(number.one, number.zero, number.zero,  number.zero,
                            number.zero,    c,          -s,         number.zero,
                            number.zero,    s,          c,          number.zero,
                            number.zero, number.zero, number.zero,  number.one);
        }

        /// <summary>Returns a float4x4 matrix that rotates around the y-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the y-axis towards the origin in radians.</param>
        /// <returns>The float4x4 rotation matrix that rotates around the y-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 RotateY(number angle)
        {
            // {{c_1, 0, s_1}, {0, 1, 0}, {-s_1, 0, c_1}}
            number s, c;
            sincos(angle, out s, out c);
            return float4x4(   c,       number.zero,   s,           number.zero,
                            number.zero, number.one,    number.zero, number.zero,
                            -s,          number.zero,   c,           number.zero,
                            number.zero, number.zero,   number.zero, number.one);
        }

        /// <summary>Returns a float4x4 matrix that rotates around the z-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the z-axis towards the origin in radians.</param>
        /// <returns>The float4x4 rotation matrix that rotates around the z-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 RotateZ(number angle)
        {
            // {{c_2, -s_2, 0}, {s_2, c_2, 0}, {0, 0, 1}}
            number s, c;
            sincos(angle, out s, out c);
            return float4x4(c,             -s,         number.zero, number.zero,
                            s,              c,          number.zero, number.zero,
                            number.zero, number.zero,   number.one,  number.zero,
                            number.zero, number.zero,   number.zero, number.one);

        }

        /// <summary>Returns a float4x4 scale matrix given 3 axis scales.</summary>
        /// <param name="s">The uniform scaling factor.</param>
        /// <returns>The float4x4 matrix that represents a uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 Scale(number s)
        {
            return float4x4(   s,       number.zero, number.zero,  number.zero,
                            number.zero,    s,        number.zero,  number.zero,
                            number.zero, number.zero,   s,          number.zero,
                            number.zero, number.zero, number.zero,  number.one);
        }

        /// <summary>Returns a float4x4 scale matrix given a float3 vector containing the 3 axis scales.</summary>
        /// <param name="x">The x-axis scaling factor.</param>
        /// <param name="y">The y-axis scaling factor.</param>
        /// <param name="z">The z-axis scaling factor.</param>
        /// <returns>The float4x4 matrix that represents a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 Scale(number x, number y, number z)
        {
            return float4x4(   x,       number.zero, number.zero, number.zero,
                            number.zero,    y,        number.zero, number.zero,
                            number.zero, number.zero,   z,         number.zero,
                            number.zero, number.zero, number.zero, number.one);
        }

        /// <summary>Returns a float4x4 scale matrix given a float3 vector containing the 3 axis scales.</summary>
        /// <param name="scales">The vector containing scale factors for each axis.</param>
        /// <returns>The float4x4 matrix that represents a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 Scale(float3 scales)
        {
            return Scale(scales.x, scales.y, scales.z);
        }

        /// <summary>Returns a float4x4 translation matrix given a float3 translation vector.</summary>
        /// <param name="vector">The translation vector.</param>
        /// <returns>The float4x4 translation matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 Translate(float3 vector)
        {
            return float4x4(float4(number.one, number.zero,   number.zero, number.zero),
                            float4(number.zero, number.one,    number.zero, number.zero),
                            float4(number.zero, number.zero,   number.one, number.zero),
                            float4(vector.x,   vector.y,       vector.z,   number.one));
        }

        /// <summary>
        /// Returns a float4x4 view matrix given an eye position, a target point and a unit length up vector.
        /// The up vector is assumed to be unit length, the eye and target points are assumed to be distinct and
        /// the vector between them is assumes to be collinear with the up vector.
        /// If these assumptions are not met use float4x4.LookRotationSafe instead.
        /// </summary>
        /// <param name="eye">The eye position.</param>
        /// <param name="target">The view target position.</param>
        /// <param name="up">The eye up direction.</param>
        /// <returns>The float4x4 view matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 LookAt(float3 eye, float3 target, float3 up)
        {
            float3x3 rot = float3x3.LookRotation(normalizesafe(target - eye), up);

            float4x4 matrix;
            matrix.c0 = float4(rot.c0, number.zero);
            matrix.c1 = float4(rot.c1, number.zero);
            matrix.c2 = float4(rot.c2, number.zero);
            matrix.c3 = float4(eye, number.one);
            return matrix;
        }

        /// <summary>
        /// Returns a float4x4 centered orthographic projection matrix.
        /// </summary>
        /// <param name="width">The width of the view volume.</param>
        /// <param name="height">The height of the view volume.</param>
        /// <param name="near">The distance to the near plane.</param>
        /// <param name="far">The distance to the far plane.</param>
        /// <returns>The float4x4 centered orthographic projection matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 Ortho(number width, number height, number near, number far)
        {
            number rcpdx = 1.0f / width;
            number rcpdy = 1.0f / height;
            number rcpdz = 1.0f / (far - near);

            return float4x4(
                2.0f * rcpdx,   number.zero,    number.zero,    number.zero,
                number.zero,    2.0f * rcpdy,   number.zero,    number.zero,
                number.zero,    number.zero,    -2.0f * rcpdz,  -(far + near) * rcpdz,
                number.zero,    number.zero,    number.zero,    number.one
                );
        }

        /// <summary>
        /// Returns a float4x4 off-center orthographic projection matrix.
        /// </summary>
        /// <param name="left">The minimum x-coordinate of the view volume.</param>
        /// <param name="right">The maximum x-coordinate of the view volume.</param>
        /// <param name="bottom">The minimum y-coordinate of the view volume.</param>
        /// <param name="top">The minimum y-coordinate of the view volume.</param>
        /// <param name="near">The distance to the near plane.</param>
        /// <param name="far">The distance to the far plane.</param>
        /// <returns>The float4x4 off-center orthographic projection matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 OrthoOffCenter(number left, number right, number bottom, number top, number near, number far)
        {
            number rcpdx = 1.0f / (right - left);
            number rcpdy = 1.0f / (top - bottom);
            number rcpdz = 1.0f / (far - near);

            return float4x4(
                2.0f * rcpdx,   number.zero,    number.zero,    -(right + left) * rcpdx,
                number.zero,    2.0f * rcpdy,   number.zero,    -(top + bottom) * rcpdy,
                number.zero,    number.zero,    -2.0f * rcpdz,  -(far + near) * rcpdz,
                number.zero,    number.zero,    number.zero,    number.one
                );
        }

        /// <summary>
        /// Returns a float4x4 perspective projection matrix based on field of view.
        /// </summary>
        /// <param name="verticalFov">Vertical Field of view in radians.</param>
        /// <param name="aspect">X:Y aspect ratio.</param>
        /// <param name="near">Distance to near plane. Must be greater than zero.</param>
        /// <param name="far">Distance to far plane. Must be greater than zero.</param>
        /// <returns>The float4x4 perspective projection matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 PerspectiveFov(number verticalFov, number aspect, number near, number far)
        {
            number cotangent = 1.0f / tan(verticalFov * 0.5f);
            number rcpdz = 1.0f / (near - far);

            return float4x4(
                cotangent / aspect, number.zero,    number.zero,            number.zero,
                number.zero,        cotangent,      number.zero,            number.zero,
                number.zero,        number.zero,    (far + near) * rcpdz,   2.0f * near * far * rcpdz,
                number.zero,        number.zero,    -number.one,            number.zero
                );
        }

        /// <summary>
        /// Returns a float4x4 off-center perspective projection matrix.
        /// </summary>
        /// <param name="left">The x-coordinate of the left side of the clipping frustum at the near plane.</param>
        /// <param name="right">The x-coordinate of the right side of the clipping frustum at the near plane.</param>
        /// <param name="bottom">The y-coordinate of the bottom side of the clipping frustum at the near plane.</param>
        /// <param name="top">The y-coordinate of the top side of the clipping frustum at the near plane.</param>
        /// <param name="near">Distance to the near plane. Must be greater than zero.</param>
        /// <param name="far">Distance to the far plane. Must be greater than zero.</param>
        /// <returns>The float4x4 off-center perspective projection matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 PerspectiveOffCenter(number left, number right, number bottom, number top, number near, number far)
        {
            number rcpdz = 1.0f / (near - far);
            number rcpWidth = 1.0f / (right - left);
            number rcpHeight = 1.0f / (top - bottom);

            return float4x4(
                2.0f * near * rcpWidth, number.zero,                (left + right) * rcpWidth,  number.zero,
                number.zero,            2.0f * near * rcpHeight,    (bottom + top) * rcpHeight, number.zero,
                number.zero,            number.zero,                (far + near) * rcpdz,       2.0f * near * far * rcpdz,
                number.zero,            number.zero,                -number.one,                number.zero
                );
        }

        /// <summary>
        /// Returns a float4x4 matrix representing a combined scale-, rotation- and translation transform.
        /// Equivalent to mul(translationTransform, mul(rotationTransform, scaleTransform)).
        /// </summary>
        /// <param name="translation">The translation vector.</param>
        /// <param name="rotation">The quaternion rotation.</param>
        /// <param name="scale">The scaling factors of each axis.</param>
        /// <returns>The float4x4 matrix representing the translation, rotation, and scale by the inputs.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 TRS(float3 translation, quaternion rotation, float3 scale)
        {
            float3x3 r = float3x3(rotation);
            return float4x4(  float4(r.c0 * scale.x, number.zero),
                              float4(r.c1 * scale.y, number.zero),
                              float4(r.c2 * scale.z, number.zero),
                              float4(translation, number.one));
        }
    }

    partial class math
    {
        /// <summary>
        /// Extracts a float3x3 from the upper left 3x3 of a float4x4.
        /// </summary>
        /// <param name="f4x4"><see cref="float4x4"/> to extract a float3x3 from.</param>
        /// <returns>Upper left 3x3 matrix as float3x3.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 float3x3(float4x4 f4x4)
        {
            return new float3x3(f4x4);
        }

        /// <summary>Returns a float3x3 matrix constructed from a quaternion.</summary>
        /// <param name="rotation">The quaternion representing a rotation.</param>
        /// <returns>The float3x3 constructed from a quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 float3x3(quaternion rotation)
        {
            return new float3x3(rotation);
        }

        /// <summary>Returns a float3x3 matrix constructed from a quaternion.</summary>
        /// <param name="rotation">The quaternion representing a rotation.</param>
        /// <returns>The float3x3 constructed from a quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 float3x3(Unity.Mathematics.quaternion rotation)
        {
            return new float3x3(rotation);
        }

        /// <summary>Returns a float4x4 constructed from a float3x3 rotation matrix and a float3 translation vector.</summary>
        /// <param name="rotation">The float3x3 rotation matrix.</param>
        /// <param name="translation">The translation vector.</param>
        /// <returns>The float4x4 constructed from a rotation and translation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 float4x4(float3x3 rotation, float3 translation)
        {
            return new float4x4(rotation, translation);
        }

        /// <summary>Returns a float4x4 constructed from a quaternion and a float3 translation vector.</summary>
        /// <param name="rotation">The quaternion rotation.</param>
        /// <param name="translation">The translation vector.</param>
        /// <returns>The float4x4 constructed from a rotation and translation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 float4x4(quaternion rotation, float3 translation)
        {
            return new float4x4(rotation, translation);
        }

        /// <summary>Returns a float4x4 constructed from a RigidTransform.</summary>
        /// <param name="transform">The rigid transformation.</param>
        /// <returns>The float4x4 constructed from a RigidTransform.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 float4x4(RigidTransform transform)
        {
            return new float4x4(transform);
        }

        /// <summary>Returns an orthonormalized version of a float3x3 matrix.</summary>
        /// <param name="i">The float3x3 to be orthonormalized.</param>
        /// <returns>The orthonormalized float3x3 matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 orthonormalize(float3x3 i)
        {
            float3x3 o;

            float3 u = i.c0;
            float3 v = i.c1 - i.c0 * math.dot(i.c1, i.c0);

            number lenU = math.length(u);
            number lenV = math.length(v);

            bool c = lenU > 1e-30f && lenV > 1e-30f;

            o.c0 = math.select(float3(number.one, number.zero, number.zero), u / lenU, c);
            o.c1 = math.select(float3(number.zero, number.one, number.zero), v / lenV, c);
            o.c2 = math.cross(o.c0, o.c1);

            return o;
        }
    }
}
