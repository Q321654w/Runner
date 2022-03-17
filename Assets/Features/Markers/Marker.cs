using UnityEngine;

namespace Features.Markers
{
    public class Marker : MonoBehaviour
    {
        [SerializeField] private Color _color;
        [SerializeField] private float _radius;

        public void MoveToMe(Transform obj)
        {
            obj.position = transform.position;
        }
        
        public void RotateLikeMe(Transform obj)
        {
            obj.rotation = transform.rotation;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position, _radius);
        }
    }
}