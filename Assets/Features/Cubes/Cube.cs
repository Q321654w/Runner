using System;
using Features.Barriers;
using Movements.DeltaPositions;
using UnityEngine;

namespace Features.Cubes
{
    public class Cube : MonoBehaviour, IGameUpdate
    {
        public event Action<Cube> Connected;
        public event Action<Cube> Died;

        [SerializeField] private ParticleSystem _dieParticle;
        [SerializeField] private ParticleSystem _connectParticle;
        [SerializeField] private Material _material;

        private IDeltaPosition _delta;
        private MeshRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public void Initialize(IDeltaPosition delta)
        {
            _delta = delta;
        }

        public void GameUpdate()
        {
            Move();
        }

        private void Move()
        {
            var delta = _delta.Evaluate();
            transform.position += delta;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Cube cube))
                InteractWith(cube);

            if (other.gameObject.TryGetComponent(out Barrier barrier))
                InteractWith(barrier);
        }

        private void InteractWith(Barrier barrier)
        {
            DestroySelf();
        }

        private void DestroySelf()
        {
            Died?.Invoke(this);
            Instantiate(_dieParticle, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        private void InteractWith(Cube cube)
        {
            Connected?.Invoke(cube);
            cube.ConnectWith(this);
        }

        private void ConnectWith(Cube cube)
        {
            var particles = Instantiate(_connectParticle, transform.position + Vector3.up, transform.rotation);
            particles.transform.SetParent(transform);
            _renderer.material = cube._material;
        }
    }
}