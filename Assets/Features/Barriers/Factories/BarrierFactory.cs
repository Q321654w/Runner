using Features.Common;
using UnityEngine;

namespace Features.Barriers.Factories
{
    public class BarrierFactory : IFactory<Barrier>
    {
        private readonly Barrier _prefab;

        public BarrierFactory(Barrier prefab)
        {
            _prefab = prefab;
        }

        public Barrier Create()
        {
            var instance = Object.Instantiate(_prefab);
            return instance;
        }
    }
}