using Controllers;
using Levels;
using UnityEngine;

namespace Player
{
    public class PlayerHealthController : MonoBehaviour
    {
        [SerializeField, Tooltip("Maximum health of the player.")]
        private float maxHealth;

        [SerializeField, Tooltip("The rate at which the player loses health over time.")]
        private float speedDamage;

        private float _currentHealth;
        private bool _isDead;
        private bool _isLevelStarted;

        public float PostLevelDamageValue { get; private set; }

        private void Awake()
        {
            _currentHealth = maxHealth > 0 ? maxHealth : 100;
            PostLevelDamageValue = maxHealth / 15;

            LevelManager.onLevelStart += OnLevelStart;
            LevelManager.onLevelStageComplete += OnLevelStageComplete;
        }

        private void OnDestroy()
        {
            LevelManager.onLevelStart -= OnLevelStart;
            LevelManager.onLevelStageComplete -= OnLevelStageComplete;
        }

        private void Update()
        {
            if (_isDead || !_isLevelStarted) return;

            UpdateHealth(-speedDamage * Time.deltaTime);
        }

        public void FinishDamage(float value)
        {
            UpdateHealth(-value);
            if (_currentHealth <= 0)
            {
                LevelManager.instance.LevelComplete();
            }
        }

        private void UpdateHealth(float delta)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + delta, 0, maxHealth);
            UiController.instance.UpdatePlayerHealthBar(_currentHealth / maxHealth);

            if (_currentHealth <= 0 && !_isDead)
            {
                _isDead = true;
                LevelManager.instance.LevelFail();
            }
        }

        private void OnLevelStart(Level level)
        {
            _isLevelStarted = true;
        }

        private void OnLevelStageComplete(Level level, int index)
        {
            _isLevelStarted = false;
        }
    }
}
