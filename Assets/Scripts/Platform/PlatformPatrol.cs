using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Platform
{
    /// <summary>
    /// Controls the patrol movement of a platform between two points.
    /// </summary>
    public class PlatformPatrol : MonoBehaviour
    {
        [SerializeField, Tooltip("First patrol point relative to the platform's starting position.")]
        private Vector3 point1;

        [SerializeField, Tooltip("Second patrol point relative to the platform's starting position.")]
        private Vector3 point2;

        [SerializeField, Tooltip("Time taken to move between points.")]
        private float timeMove = 2;

        [SerializeField, Tooltip("Delay between movements.")]
        private float delay = 1;

        private Vector3 _currentPoint1;
        private Vector3 _currentPoint2;
        private bool _isPatrolling = true;

        private void Awake()
        {
            _currentPoint1 = transform.position + point1;
            _currentPoint2 = transform.position + point2;
        }

        private void Start()
        {
            StartCoroutine(Patrol());
        }

        private void OnDrawGizmos()
        {
            DrawPatrolPoints();
        }

        private void DrawPatrolPoints()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position + point1, 0.2f);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + point2, 0.2f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position + point1, transform.position + point2);
        }

        private IEnumerator Patrol()
        {
            while (_isPatrolling)
            {
                yield return MoveToTarget(_currentPoint1);
                yield return new WaitForSeconds(delay);
                yield return MoveToTarget(_currentPoint2);
                yield return new WaitForSeconds(delay);
            }
        }

        private IEnumerator MoveToTarget(Vector3 target)
        {
            yield return transform.DOMove(target, timeMove).WaitForCompletion();
        }

        // Call this method to stop the platform's patrol movement.
        public void StopPatrolling()
        {
            _isPatrolling = false;
            StopAllCoroutines();
        }
    }
}
