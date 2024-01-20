using Controllers;
using UnityEngine;
using Platform;
using DG.Tweening;

namespace Player
{
    [RequireComponent(typeof(PlayerMoveController)),
     RequireComponent(typeof(PlayerColorController))]
    public class PlayerCollisionController : MonoBehaviour
    {
        private PlayerMoveController _playerMove;
        private PlayerColorController _playerColor;
        private PlayerHealthController _playerHealth;
        private int _xNumber;
        private void Awake()
        {
            _playerMove = GetComponent<PlayerMoveController>();
            _playerColor = GetComponent<PlayerColorController>();
            _playerHealth = GetComponent<PlayerHealthController>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<PlatformController>())
            {
                //Values
                var platform = collision.gameObject.GetComponent<PlatformController>();
                var difference = _playerColor.PlayerColor.GetHashCode() - platform.platformColor.GetHashCode();
                difference = Mathf.Abs(difference);

                //Active Grounded
                _playerMove.SetGroundedState(true);

                //Check For Lose or Change Color
                if (platform.changeColor)
                    _playerColor.ChangeColor(platform.platformColor);
                else if (difference > 9999999)
                    LevelManager.instance.LevelFail();

                //Check Spring
                if (difference <= 9999999 && platform.spring)
                {
                    _playerMove.PerformJump(platform.targetJump, platform.curve);
                }

                //Set Position, Rotation and Parent
                if (!platform.spring)
                {
                    transform.SetParent(platform.transform);
                    transform.DOLocalMoveZ(0, 0.03f);
                }

                transform.eulerAngles = platform.transform.eulerAngles;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag($"Out"))
            {
                LevelManager.instance.LevelFail();
            }
            else if (other.gameObject.CompareTag($"FinishLine"))
            {
                LevelManager.instance.LevelStageComplete();
            }
            else if (other.gameObject.CompareTag($"X"))
            {
                _xNumber++;
                _playerHealth.FinishDamage(_playerHealth.PostLevelDamageValue);
                if (_xNumber >= 10)
                {
                    LevelManager.instance.LevelComplete();
                }
            }
        }
    }
}
