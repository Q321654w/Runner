using System;
using Features.LevelGenerators.CellTypes;
using UnityEngine;

namespace Features.Common
{
    [Serializable]
    public struct CellTypeFloatPair
    {
        [SerializeField] private float _key;
        [SerializeField] private CellType _value;

        public CellTypeFloatPair(float key, CellType value)
        {
            _key = key;
            _value = value;
        }
        
        public float Key => _key;
        public CellType Value => _value;
    }
}