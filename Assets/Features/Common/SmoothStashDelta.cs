using Movements.DeltaPositions;
using Movements.DeltaPositions.Decorators;
using UnityEngine;
using Values;

namespace Features.Common
{
    public class SmoothStashDelta : DeltaPositionDecorator
    {
        private readonly IValue<float> _deltaTime;
        private readonly float _translationDelay;

        private Vector3 _partsSum;
        private Vector3 _stashedDelta;
        private Vector3 _previousDelta;
        private float _progress;

        public SmoothStashDelta(IDeltaPosition childDelta, IValue<float> deltaTime, float translationDelay) : base(childDelta)
        {
            _deltaTime = deltaTime;
            _translationDelay = translationDelay;
            Reset();
        }

        public override Vector3 Evaluate()
        {
            var childDelta = ChildDeltaPosition.Evaluate();

            if (childDelta == Vector3.zero && _stashedDelta == Vector3.zero)
                return Vector3.zero;

            if (childDelta != Vector3.zero)
                CalculateNewData(childDelta);

            _progress += _deltaTime.Evaluate();

            if (_progress < _translationDelay)
            {
                var part = CalculatePart();
                _partsSum += part;

                return part;
            }

            var correctedDelta = CorrectDelta();
            Reset();

            return correctedDelta;
        }

        private Vector3 CalculatePart()
        {
            var currentDelta = _stashedDelta * _progress;
            var part = currentDelta - _previousDelta;
            _previousDelta = currentDelta;

            return part;
        }

        private void CalculateNewData(Vector3 childDelta)
        {
            _progress = 0;

            _stashedDelta = _stashedDelta - _partsSum + childDelta;

            _previousDelta = Vector3.zero;
            _partsSum = Vector3.zero;
        }

        private Vector3 CorrectDelta()
        {
            return _stashedDelta - _partsSum;
        }

        private void Reset()
        {
            _previousDelta = Vector3.zero;
            _stashedDelta = Vector3.zero;
            _partsSum = Vector3.zero;
            _progress = 0;
        }
    }
}