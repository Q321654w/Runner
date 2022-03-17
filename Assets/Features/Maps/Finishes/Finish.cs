using System;
using Features.Markers;
using UnityEngine;
using Update;

namespace Features.Maps.Finishes
{
    public class Finish : IUpdate
    {
        public event Action Reached;

        private readonly FinishMarker _finishMarker;
        private readonly Transform _target;

        public Finish(Transform target, FinishMarker finishMarker)
        {
            _target = target;
            _finishMarker = finishMarker;
        }

        public void Update()
        {
            if (_target.transform.position.z > _finishMarker.transform.position.z)
                Reached?.Invoke();
        }
    }
}