using System.Collections.Generic;
using Movements.DeltaPositions;
using UnityEngine;

namespace Features.Common
{
    public class FollowForNearest<T> : IDeltaPosition where T : MonoBehaviour
    {
        private readonly IDeltaPosition _objectDelta;
        
        private readonly List<T> _objects;

        private Vector3 _lastPosition;
        private T _lastNearest;

        public FollowForNearest(List<T> objects, IDeltaPosition objectDelta)
        {
            _objects = objects;
            _objectDelta = objectDelta;
            _lastNearest = CalculateNearest(_objects);
            _lastPosition = _lastNearest.transform.position;
        }

        public Vector3 Evaluate()
        {
            var currentNearest = CalculateNearest(_objects);

            if (_lastNearest == null)
            {
                _lastNearest = currentNearest;

                var lastNearestCurrentPosition = _lastPosition + _objectDelta.Evaluate();
                var delta = currentNearest.transform.position - lastNearestCurrentPosition;
                _lastPosition = _lastNearest.transform.position;

                return delta;
            }

            if (_lastNearest != currentNearest)
                return CalculateDeltaBetweenLastAndCurrentObjects(currentNearest);

            _lastPosition = _lastNearest.transform.position;
            return Vector3.zero;
        }

        private Vector3 CalculateDeltaBetweenLastAndCurrentObjects(T nearestObject)
        {
            var currentPosition = nearestObject.transform.position;

            var delta = currentPosition - _lastNearest.transform.position;
            _lastNearest = nearestObject;

            return delta;
        }

        private T CalculateNearest(List<T> objects)
        {
            var min = new Vector3(0, 0, float.MaxValue);
            T obj = default;

            for (int i = 0; i < objects.Count; i++)
            {
                var currentObj = objects[i];

                if (currentObj.transform.position.z >= min.z)
                    continue;

                min = currentObj.transform.position;
                obj = currentObj;
            }

            return obj;
        }
    }
}