using UnityEngine;

namespace Features.Markers
{
    public class PlayerStartPosition : Marker
    {
        [SerializeField] private float _playerSize;

        public float PlayerSize => _playerSize;
    }
}