using Controllers;
using Levels;
using UnityEngine;

namespace Player
{
    public class PlayerHealthController : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float speedDamage;

        private float _currentHealth;
        private bool _dead;
        private bool _start;

        public float postLevelDamageValue { get; private set; }

        private void Awake()
        {
            _currentHealth = maxHealth;
            postLevelDamageValue = maxHealth / 15;

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
            if (_dead || !_start)
            {
                return;
            }

            _currentHealth -= speedDamage * Time.deltaTime;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
            UiController.instance.UpdatePlayerHealthBar(_currentHealth / maxHealth);

            if (_currentHealth <= 0)
            {
                LevelManager.instance.LevelFail();
                _dead = true;
            }
        }

        public void FinishDamage(float value)
        {
            _currentHealth -= value;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
            UiController.instance.UpdatePlayerHealthBar(_currentHealth / maxHealth);

            if (_currentHealth <= 0)
            {
                LevelManager.instance.LevelComplete();
            }
        }

        private void OnLevelStart(Level level)
        {
            _start = true;
        }

        private void OnLevelStageComplete(Level level, int index)
        {
            _start = false;
        }
    }
}
