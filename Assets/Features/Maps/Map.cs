using Features.Markers;
using UnityEngine;

namespace Features.Maps
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private FinishMarker _finishMarker;
        
        [SerializeField] private LevelMarker _start;
        [SerializeField] private LevelMarker _end;

        [SerializeField] private Vector3 _startOffset;
        [SerializeField] private PlayerStartPosition _playerStartPosition;

        [SerializeField] private float _ySize;
        private Vector3 _halfScale;

        public Vector3 HalfScale => _halfScale;
        public LevelMarker Start => _start;
        public LevelMarker End => _end;
        public FinishMarker FinishMarker => _finishMarker;

        private void OnValidate()
        {
            _halfScale = transform.lossyScale / 2;
            
            var end = _end.transform.position;
            var start = _start.transform.position;

            var zSize = end.z - start.z;
            var xSize = end.x - start.x;
            var halfX = Mathf.Lerp(start.x, end.x, 0.5f);
            var halfZ = Mathf.Lerp(start.z, end.z, 0.5f);

            var position = transform.position;
            position = new Vector3(halfX, position.y, halfZ);

            transform.position = position;
            transform.localScale = new Vector3(xSize, _ySize, zSize);
        }

        [ContextMenu("PlacePlayerStartAtStart")]
        private void PlacePlayerStartAtStart()
        {
            var end = _end.transform.position;
            var start = _start.transform.position;
            var halfX = Mathf.Lerp(start.x, end.x, 0.5f);

            var yOffset = transform.position.y + _ySize / 2 + _playerStartPosition.PlayerSize / 2;
            _playerStartPosition.transform.position = new Vector3(halfX, yOffset, start.z) + _startOffset;
        }
    }
}