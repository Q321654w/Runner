using System;
using System.Collections.Generic;
using Features.Cubes;
using Movements.DeltaPositions;
using UnityEngine;

namespace Features.Players
{
    public class Player : MonoBehaviour, IGameUpdate
    {
        public event Action Died;

        private IDeltaPosition _delta;
        private List<Cube> _cubes;
        
        public IReadOnlyList<Cube> Cubes => _cubes;

        public void Initialize(List<Cube> cubes, IDeltaPosition delta)
        {
            _delta = delta;
            _cubes = cubes;

            foreach (var cube in _cubes)
            {
                SubscribeOn(cube);
            }
        }

        private void OnCubeDied(Cube cube)
        {
            _cubes.Remove(cube);

            cube.Died -= OnCubeDied;
            cube.Connected -= OnConnected;

            if (_cubes.Count <= 0)
                Died?.Invoke();
        }

        private void OnConnected(Cube cube)
        {
            if (_cubes.Contains(cube))
                return;
        
            _cubes.Add(cube);
            SubscribeOn(cube);
        }

        private void SubscribeOn(Cube cube)
        {
            cube.Died += OnCubeDied;
            cube.Connected += OnConnected;
        }

        public void GameUpdate()
        {
            var delta = _delta.Evaluate();
            transform.position += delta;
            
            for (int i = 0; i < _cubes.Count; i++)
            {
                var cube = _cubes[i];
                cube.GameUpdate();
            }
        }
    }
}