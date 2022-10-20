using UnityEngine;
using DG.Tweening;

namespace Platform
{
    public class PlatformPatrol : MonoBehaviour
    {
        [SerializeField] private Vector3 point1;
        [SerializeField] private Vector3 point2;
        [SerializeField] private float timeMove = 2;
        [SerializeField] private float delay = 1;

        private Vector3 _currentPoint1;
        private Vector3 _currentPoint2;

        private void Awake()
        {
            var position = transform.position;
            _currentPoint1 = position + point1;
            _currentPoint2 = position + point2;
        }

        // Start is called before the first frame update
        void Start()
        {
            MoveToPoint1();
        }

        private void OnDrawGizmos()
        {
            var position = transform.position;
            Vector3 p1 = position + point1;
            Vector3 p2 = position + point2;
            
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(p1,0.2f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(p2,0.2f);
        }

        private void MoveToPoint1()
        {
            transform.DOMove(_currentPoint1, timeMove).OnComplete(() =>
            {
                Invoke(nameof(MoveToPoint2), delay);
            });
        }

        private void MoveToPoint2()
        {
            transform.DOMove(_currentPoint2, timeMove).OnComplete(() =>
            {
                Invoke(nameof(MoveToPoint1), delay);
            });
        }
    }
}
