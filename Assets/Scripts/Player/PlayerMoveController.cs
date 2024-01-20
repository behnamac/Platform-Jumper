using Controllers;
using Levels;
using Tools;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField, Tooltip("Target distance for the jump.")]
        private float jumpTarget;

        [SerializeField, Tooltip("Curve value for the jump trajectory.")]
        private float curve;

        private Rigidbody _rigidbody;
        private bool _canJump;
        private bool _isGrounded;
        private bool _finishLineMove;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            SubscribeToLevelEvents();

            SetGroundedState(true);
        }

        private void Update()
        {
            HandleInput();
            HandleFinishLineMovement();
        }

        private void OnDestroy()
        {
            UnsubscribeFromLevelEvents();
        }

        private void OnDrawGizmos()
        {
            DrawJumpTargetGizmo();
        }

        private void DrawJumpTargetGizmo()
        {
            Gizmos.color = Color.green;
            Vector3 targetPoint = transform.position + transform.forward * jumpTarget;
            Gizmos.DrawSphere(targetPoint, 0.3f);
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0) && _canJump && _isGrounded)
            {
                PerformJump(jumpTarget, curve);
            }
        }

        private void HandleFinishLineMovement()
        {
            if (_finishLineMove)
            {
                FinishMove();
            }
        }

        public void PerformJump(float jumpDistance, float curveValue)
        {
            transform.SetParent(null);
            Vector3 targetPoint = transform.position + transform.forward * jumpDistance;
            Vector3 velocity = Calculate.CalculateVelocity(targetPoint, transform.position, curveValue);
            _rigidbody.velocity = velocity;
            SetGroundedState(false);
        }

        private void FinishMove()
        {
            transform.Translate(0, 0, 20 * Time.deltaTime);
        }

        public void SetGroundedState(bool isGrounded)
        {
            _isGrounded = isGrounded;
        }

        private void SubscribeToLevelEvents()
        {
            LevelManager.onLevelStart += HandleLevelStart;
            LevelManager.onLevelComplete += HandleLevelComplete;
            LevelManager.onLevelStageComplete += HandleLevelStageComplete;
            LevelManager.onLevelFail += HandleLevelFail;
        }

        private void UnsubscribeFromLevelEvents()
        {
            LevelManager.onLevelStart -= HandleLevelStart;
            LevelManager.onLevelComplete -= HandleLevelComplete;
            LevelManager.onLevelStageComplete -= HandleLevelStageComplete;
            LevelManager.onLevelFail -= HandleLevelFail;
        }

        private void HandleLevelStart(Level level)
        {
            _canJump = true;
        }

        private void HandleLevelComplete(Level level)
        {
            _canJump = false;
            _finishLineMove = false;
        }

        private void HandleLevelStageComplete(Level level, int index)
        {
            _canJump = false;
            _finishLineMove = true;
        }

        private void HandleLevelFail(Level level)
        {
            _canJump = false;
        }
    }
}
