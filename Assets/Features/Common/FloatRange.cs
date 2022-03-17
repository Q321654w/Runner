using System;
using UnityEngine;
using Values;

namespace Features.Common
{
    [Serializable]
    public struct FloatRange : IRange<float>
    {
        [SerializeField] private float _min;
        [SerializeField] private float _max;

        public FloatRange(float min, float max)
        {
            _min = min;
            _max = max;
        }

        public float Min()
        {
            return _min;
        }

        public float Max()
        {
            return _max;
        }
    }
}