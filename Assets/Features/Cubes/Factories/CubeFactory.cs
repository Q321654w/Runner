using Movements.DeltaPositions;
using UnityEngine;

namespace Features.Cubes.Factories
{
    public class CubeFactory
    {
        private readonly Cube _cubeWithParent;
        private readonly Cube _defaultPrefab;
        private readonly IDeltaPosition _delta;

        public CubeFactory(Cube defaultPrefab, IDeltaPosition delta, Cube cubeWithParent)
        {
            _defaultPrefab = defaultPrefab;
            _delta = delta;
            _cubeWithParent = cubeWithParent;
        }

        public Cube Create(bool hasParent)
        {
            var prefab = hasParent ? _cubeWithParent : _defaultPrefab;
            
            var instance = Object.Instantiate(prefab);
            instance.Initialize(_delta);

            return instance;
        }
    }
}