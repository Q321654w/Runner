using System;
using UnityEngine;
using Values;

namespace Features.Common
{
    [Serializable]
    public struct IntRange : IRange<int>
    {
        [SerializeField] private int _min;
        [SerializeField] private int _max;

        public IntRange(int min, int max)
        {
            _min = min;
            _max = max;
        }
        
        public int Min()
        {
            return _min;
        }

        public int Max()
        {
            return _max;
        }
    }
}