using System;
using UnityEngine;

namespace Utilities
{
    [Serializable]
    public class ValueInterpolator
    {

        [SerializeField] private float valueUpperBound;
        [SerializeField] private float targetValueStart;
        [SerializeField] private float targetValueEnd;

        public float Interpolate(float value)
        {
            var percent = Math.Clamp(value / valueUpperBound, min: 0f, max: 1f);
            return Math.Max(percent * targetValueEnd, targetValueStart);
        }
        
    }
}