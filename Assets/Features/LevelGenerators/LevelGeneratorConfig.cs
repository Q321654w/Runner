using System;
using Features.Common;
using Features.LevelGenerators.CellTypes;
using UnityEngine;

namespace Features.LevelGenerators
{
    [CreateAssetMenu(menuName = "LevelGeneratorConfig")]
    public class LevelGeneratorConfig : ScriptableObject
    {
        [SerializeField] private Vector2Int _startOffset;
        [SerializeField] private Vector2Int _size;
        [SerializeField] private CellTypeFloatPair[] _pairs;
        [SerializeField] private Vector2Int _minContentDistance;

        public Vector2Int StartOffset => _startOffset;
        public Vector2Int Size => _size;
        public Vector2Int MinContentDistance => _minContentDistance;

        private void OnValidate()
        {
            for (var index = 0; index < _pairs.Length; index++)
            {
                var pair = _pairs[index];
                if (pair.Key > 1)
                {
                    _pairs[index] = new CellTypeFloatPair(1, pair.Value);
                    throw new Exception("Chance greater then 1 " + pair.Value);
                }

                if (pair.Key < 0)
                {
                    _pairs[index] = new CellTypeFloatPair(0, pair.Value);
                    throw new Exception("Chance less then 0 " + pair.Value);
                }
            }
        }

        public CellType GetCellType(float factor)
        {
            var min = new CellTypeFloatPair(1.1f, CellType.Empty);

            for (int i = 0; i < _pairs.Length; i++)
            {
                var pair = _pairs[i];
                if (pair.Key < factor)
                    continue;

                if (min.Key > pair.Key)
                    min = pair;
            }

            return min.Value;
        }
    }
}