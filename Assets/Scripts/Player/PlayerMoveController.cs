using Controllers;
using Levels;
using Tools;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private float jumpTarget;
        [SerializeField] private float curve;

        private Rigidbody _rigid;
        private bool _canJump;
        private bool _isGrounded;
        private bool _finishLineMove;
        
        private void Awake()
        {
            _rigid = GetComponent<Rigidbody>();

            LevelManager.onLevelStart += OnLevelStart;
            LevelManager.onLevelComplete += OnLevelComplete;
            LevelManager.onLevelStageComplete += OnLevelStageComplete;
            LevelManager.onLevelFail += OnLevelFail;

            SetActiveGrounded(true);
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _canJump)
            {
                Jump(jumpTarget, curve);
            }

            if (_finishLineMove)
            {
                FinihsMove();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            var thisTransform = transform;
            Vector3 targetPoint = thisTransform.position + thisTransform.forward * jumpTarget;
            Gizmos.DrawSphere(targetPoint, 0.3f);
        }

        private void OnDestroy()
        {
            LevelManager.onLevelStart -= OnLevelStart;
            LevelManager.onLevelComplete -= OnLevelComplete;
            LevelManager.onLevelStageComplete -= OnLevelStageComplete;
            LevelManager.onLevelFail -= OnLevelFail;
        }

        public void Jump(float jumpValue, float curveValue)
        {
            if (!_isGrounded) return;
            var thisTransform = transform;
            thisTransform.SetParent(null);
            var thisPosition = thisTransform.position;
            var targetPoint = thisPosition + thisTransform.forward * jumpValue;

            Vector3 vo = Calculate.CalculateVelocity(targetPoint, thisPosition, curveValue);

            _rigid.velocity = vo;
            SetActiveGrounded(false);
        }

        private void FinihsMove()
        {
            transform.Translate(0, 0, 20 * Time.deltaTime);
        }

        public void SetActiveGrounded(bool active)
        {
            _isGrounded = active;
        }

        private void OnLevelStart(Level level)
        {
            _canJump = true;
        }

        private void OnLevelComplete(Level level)
        {
            _canJump = false;
            _finishLineMove = false;
        }
        private void OnLevelStageComplete(Level level, int index)
        {
            _canJump = false;
            _finishLineMove = true;
        }

        private void OnLevelFail(Level level)
        {
            _canJump = false;
        }
    }
}
