using System;
using System.Globalization;
using System.Runtime.CompilerServices;

#pragma warning disable 0660, 0661

namespace Nt.Deterministics
{
    [System.Serializable]
    public struct rect : IEquatable<rect>, IFormattable
    {
        /// <summary>
        /// construct rect from the other rect
        /// </summary>
        /// <param name="source">the other rect</param>
        public rect(rect source)
        {
            x.RawValue = source.x.RawValue;
            y.RawValue = source.y.RawValue;
            width.RawValue = source.width.RawValue;
            height.RawValue = source.height.RawValue;
        }

        /// <summary>
        /// construct rect from position and size
        /// </summary>
        /// <param name="position">the position</param>
        /// <param name="size">the size</param>
        public rect(float2 position, float2 size)
        {
            x.RawValue = position.x.RawValue;
            y.RawValue = position.y.RawValue;
            width.RawValue = size.x.RawValue;
            height.RawValue = size.y.RawValue;
        }

        /// <summary>
        /// construct rect from position and size
        /// </summary>
        /// <param name="x">the x value of position</param>
        /// <param name="y">the y value of position</param>
        /// <param name="width">the width of size</param>
        /// <param name="height">the height of size</param>
        public rect(number x, number y, number width, number height)
        {
            this.x.RawValue = x.RawValue;
            this.y.RawValue = y.RawValue;
            this.width.RawValue = width.RawValue;
            this.height.RawValue = height.RawValue;
        }

        /// <summary>
        /// the zero rect
        /// </summary>
        public static rect zero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new rect(number.zero, number.zero, number.zero, number.zero);
        }

        /// <summary>
        /// the x value of position
        /// </summary>
        public number x;

        /// <summary>
        /// the y value of position
        /// </summary>
        public number y;

        /// <summary>
        /// the width of size
        /// </summary>
        public number width;

        /// <summary>
        /// the height of size
        /// </summary>
        public number height;

        /// <summary>
        /// get/set the minimum x coordinate of the rectangle.
        /// </summary>
        public number xMin
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => x;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value.RawValue != x.RawValue)
                {
                    width.RawValue += x.RawValue - value.RawValue;
                    x.RawValue = value.RawValue;
                }
            }
        }

        /// <summary>
        /// get/set the minimum y coordinate of the rectangle.
        /// </summary>
        public number yMin
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => y;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value.RawValue != y.RawValue)
                {
                    height.RawValue += y.RawValue - value.RawValue;
                    y.RawValue = value.RawValue;
                }
            }
        }

        /// <summary>
        /// get/set the maximum x coordinate of the rectangle.
        /// </summary>
        public number xMax
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new number(x.RawValue + width.RawValue);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value.RawValue != x.RawValue + width.RawValue)
                    width.RawValue = value.RawValue - x.RawValue;
            }
        }

        /// <summary>
        /// get/set the maximum y coordinate of the rectangle.
        /// </summary>
        public number yMax
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new number(y.RawValue + height.RawValue);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value.RawValue != y.RawValue + height.RawValue)
                    height.RawValue = value.RawValue - y.RawValue;
            }
        }

        /// <summary>
        /// get/set the position of the minimum corner of the rectangle.
        /// </summary>
        public float2 min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new float2(xMin, yMin);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                xMin = value.x;
                yMin = value.y;
            }
        }

        /// <summary>
        /// get/set the position of the maximum corner of the rectangle.
        /// </summary>
        public float2 max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new float2(xMax, yMax);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                xMax = value.x;
                yMax = value.y;
            }
        }

        /// <summary>
        /// get/set the position of the center of the rectangle.
        /// </summary>
        public float2 center
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new float2(x + width / 2, y + height / 2);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                x.RawValue = value.x.RawValue - width.RawValue / 2L;
                y.RawValue = value.y.RawValue - height.RawValue / 2L;
            }
        }

        /// <summary>
        /// get/set the x and y position of the rectangle.
        /// </summary>
        public float2 position
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new float2(x, y);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                x.RawValue = value.x.RawValue;
                y.RawValue = value.y.RawValue;
            }
        }

        /// <summary>
        /// get/set the width and height of the rectangle.
        /// </summary>
        public float2 size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new float2(width, height);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                width.RawValue = value.x.RawValue;
                height.RawValue = value.y.RawValue;
            }
        }

        /// <summary>
        /// get the minimum x coordinate of the rectangle. 
        /// </summary>
        [Obsolete("use xMin")]
        public number left
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => x;
        }

        /// <summary>
        /// get the maximum x coordinate of the rectangle.
        /// </summary>
        [Obsolete("use xMax")]
        public number right
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new number(x.RawValue + width.RawValue);
        }

        /// <summary>
        /// get the minimum y coordinate of the rectangle.
        /// </summary>
        [Obsolete("use yMin")]
        public number top
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => y;
        }

        /// <summary>
        /// get the maximum y coordinate of the rectangle.
        /// </summary>
        [Obsolete("use yMax")]
        public number bottom
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new number(y.RawValue + height.RawValue);
        }

        /// <summary>
        /// construct a rectangle from min/max coordinate values.
        /// </summary>
        /// <param name="xmin">the minimum x coordinate.</param>
        /// <param name="ymin">the minimum y coordinate.</param>
        /// <param name="xmax">the maximum x coordinate.</param>
        /// <param name="ymax">the maximum y coordinate.</param>
        /// <returns>A rectangle matching the specified coordinates.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static rect MinMaxRect(number xmin, number ymin, number xmax, number ymax) => new rect(xmin, ymin, xmax - xmin, ymax - ymin);

        /// <summary>
        /// get a point inside a rectangle, given normalized coordinates.
        /// </summary>
        /// <param name="rectangle">rectangle to get a point inside.</param>
        /// <param name="normalizedRectCoordinates">normalized coordinates to get a point for.</param>
        /// <returns>the point</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 NormalizedToPoint(rect rectangle, float2 normalizedRectCoordinates)
        {
            return new float2(math.lerp(rectangle.x, rectangle.xMax, normalizedRectCoordinates.x), 
                math.lerp(rectangle.y, rectangle.yMax, normalizedRectCoordinates.y));
        }

        /// <summary>
        /// get the normalized coordinates corresponding the the point.
        /// </summary>
        /// <param name="rectangle">rectangle to get normalized coordinates inside.</param>
        /// <param name="point"></param>
        /// <returns>A point inside the rectangle to get normalized coordinates for.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 PointToNormalized(rect rectangle, float2 point)
        {
            var result = new float2();
            if (rectangle.width.RawValue != 0L)
                result.x = math.clamp(math.unlerp(rectangle.x, rectangle.xMax, point.x), number.zero, number.one);
            if (rectangle.height.RawValue != 0L)
                result.y = math.clamp(math.unlerp(rectangle.y, rectangle.yMax, point.y), number.zero, number.one);
            return result;
        }

        /// <summary>
        /// Returns true if the x and y components of point is a point inside this rectangle.
        ///     If allowInverse is present and true, the width and height of the Rect are allowed
        ///     to take negative values (ie, the min value is greater than the max), and the
        ///     test will still work.
        /// </summary>
        /// <param name="point">Point to test.</param>
        /// <param name="allowInverse">Does the test allow the Rect's width and height to be negative?</param>
        /// <returns>true if the point lies within the specified rectangle.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(float3 point, bool allowInverse)
        {
            if (allowInverse)
            {
                bool xAxis = (this.width < 0f && point.x <= this.xMin && point.x > this.xMax) 
                    || (this.width >= 0f && point.x >= this.xMin && point.x < this.xMax);
                bool yAxis = (this.height < 0f && point.y <= this.yMin && point.y > this.yMax) 
                    || (this.height >= 0f && point.y >= this.yMin && point.y < this.yMax);
                return (xAxis && yAxis);
            }
            return Contains(point);
        }

        /// <summary>
        /// Returns true if the x and y components of point is a point inside this rectangle.
        ///     If allowInverse is present and true, the width and height of the Rect are allowed
        ///     to take negative values (ie, the min value is greater than the max), and the
        ///     test will still work.
        /// </summary>
        /// <param name="point">Point to test.</param>
        /// <returns>true if the point lies within the specified rectangle.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(float3 point) => point.x >= this.xMin && point.x< this.xMax && point.y >= this.yMin && point.y< this.yMax;

        /// <summary>
        /// Returns true if the x and y components of point is a point inside this rectangle.
        ///     If allowInverse is present and true, the width and height of the Rect are allowed
        ///     to take negative values (ie, the min value is greater than the max), and the
        ///     test will still work.
        /// </summary>
        /// <param name="point">Point to test.</param>
        /// <returns>true if the point lies within the specified rectangle.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(float2 point) => point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;

        /// <summary>
        /// is this rect equal to the other object.
        /// </summary>
        /// <param name="other">the other object</param>
        /// <returns>true if the other object is rect object and equal to this rect, otherwise false</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object other) => other is rect rOther && this.Equals(rOther);

        /// <summary>
        /// is this rect equal to the other rect
        /// </summary>
        /// <param name="other">the other rect</param>
        /// <returns>true if this rect is equal to the other rect, otherwise false</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(rect other) => other.x.RawValue == this.x.RawValue && other.y.RawValue == this.y.RawValue
            && other.width.RawValue == this.width.RawValue && other.height.RawValue == this.height.RawValue;

        /// <summary>
        /// get hash code of this rect
        /// </summary>
        /// <returns>the hash code</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => x.RawValue.GetHashCode() ^ width.RawValue.GetHashCode() << 2 ^
            y.RawValue.GetHashCode() >> 2 ^ height.RawValue.GetHashCode() >> 1;

        /// <summary>
        /// Returns true if the other rectangle overlaps this one. If allowInverse is present
        ///     and true, the widths and heights of the Rects are allowed to take negative values
        ///     (ie, the min value is greater than the max), and the test will still work.
        /// </summary>
        /// <param name="other">Other rectangle to test overlapping with.</param>
        /// <param name="allowInverse">Does the test allow the widths and heights of the Rects to be negative?</param>
        /// <returns>true if this rectangle overlaps the other rectangle, otherwise false</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(rect other, bool allowInverse)
        {
            if (allowInverse)
            {
                var temp = rect.OrderMinMax(this);
                other = rect.OrderMinMax(other);
                return temp.Overlaps(other);
            }
            return this.Overlaps(other);
        }

        /// <summary>
        /// Returns true if the other rectangle overlaps this one. If allowInverse is present
        ///     and true, the widths and heights of the Rects are allowed to take negative values
        ///     (ie, the min value is greater than the max), and the test will still work.
        /// </summary>
        /// <param name="other">Other rectangle to test overlapping with.</param>
        /// <returns>true if this rectangle overlaps the other rectangle, otherwise false</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(rect other) => other.xMax > this.xMin && other.xMin < this.xMax && other.yMax > this.yMin && other.yMin < this.yMax;

        /// <summary>
        /// Set components of an existing Rect.
        /// </summary>
        /// <param name="x">the x value of position</param>
        /// <param name="y">the y value of position</param>
        /// <param name="width">the width of size</param>
        /// <param name="height">the height of size</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(number x, number y, number width, number height)
        {
            this.x.RawValue = x.RawValue;
            this.y.RawValue = y.RawValue;
            this.width.RawValue = width.RawValue;
            this.height.RawValue = height.RawValue;
        }

        /// <summary>
        /// get a formatted string for this Rect.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="formatProvider">An object that specifies culture-specific formatting.</param>
        /// <returns>the formated string</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            bool flag = string.IsNullOrEmpty(format);
            if (flag) format = "F2";
            return string.Format(CultureInfo.InvariantCulture.NumberFormat, "(x:{0}, y:{1}, width:{2}, height:{3})", new object[]
            {
                this.x.ToString(format, formatProvider),
                this.y.ToString(format, formatProvider),
                this.width.ToString(format, formatProvider),
                this.height.ToString(format, formatProvider)
            });
        }

        /// <summary>
        /// get a formatted string for this Rect.
        /// </summary>
        /// <returns>the formated string</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);

        /// <summary>
        /// get a formatted string for this Rect.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <returns>the formated string</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format) => this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);

        /// <summary>
        /// is the lhs rect equal to the rhs rect
        /// </summary>
        /// <param name="lhs">the lhs rect</param>
        /// <param name="rhs">the rhs rect</param>
        /// <returns>true if lhs rect is equal to rhs rect, otherwise false</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(rect lhs, rect rhs) => lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;

        /// <summary>
        /// is the lhs rect not equal to the rhs rect
        /// </summary>
        /// <param name="lhs">the lhs rect</param>
        /// <param name="rhs">the rhs rect</param>
        /// <returns>true if lhs rect is not equal to rhs rect, otherwise false</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(rect lhs, rect rhs) => lhs.x != rhs.x || lhs.y != rhs.y || lhs.width != rhs.width || lhs.height != rhs.height;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static rect OrderMinMax(rect r)
        {
            if (r.xMin > r.xMax)
            {
                r.x = r.xMax;
                r.width.RawValue *= -1L;
            }
            if (r.yMin > r.yMax)
            {
                r.y = r.yMax;
                r.height.RawValue *= -1L;
            }
            return r;
        }
    }
}
