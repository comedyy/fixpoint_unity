﻿namespace Mathematics.FixedPoint
{
    /// <summary>
    /// Used by property drawers when vectors should be post normalized.
    /// </summary>
    public class PostNormalizeAttribute : UnityEngine.PropertyAttribute {}

    /// <summary>
    /// Used by property drawers when vectors should not be normalized.
    /// </summary>
    public class DoNotNormalizeAttribute : UnityEngine.PropertyAttribute {}
    public class NumberLabelAttribute : UnityEngine.PropertyAttribute
    {
        public readonly string title;
        public NumberLabelAttribute(string title)
        {
            this.title = title;
        }
    }
}
