using Game.Scripts.Config;
using Game.Scripts.Enemy;
using Game.Scripts.Game;
using Game.Scripts.UI;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PolygonCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance;

        private string _currentString = "";
        [SerializeField] private TMP_InputField typingDisplay;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField, CanBeNull] private AudioSource fireSound;
        private Animator _animator;
        private readonly int attackTriggerID = Animator.StringToHash("attack");

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if (typingDisplay)
            {
                typingDisplay.text = "";
            }

            _animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag(GameTag.Enemy))
            {
                GameController.Instance.Lose();
            }
        }

        private void OnGUI()
        {
            HandleTyping();
        }

        void HandleTyping()
        {
            if (!Event.current.isKey) return;
            char c = Event.current.character;
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
            {
                _currentString += c.ToString().ToUpper();
                typingDisplay.text = _currentString;
            }
        }

        public void HandleBackspace(InputAction.CallbackContext ctx)
        {
            if (!ctx.started) return;
            if (_currentString.Length > 0)
            {
                _currentString = _currentString.Remove(_currentString.Length - 1, 1);
                typingDisplay.text = _currentString;
            }
        }

        public void Shoot(InputAction.CallbackContext ctx)
        {
            if (!ctx.started) return;
            string word = _currentString;
            if (WaveSpawner.Instance.HasEnemy(word))
            {
                Transform enemyPos = WaveSpawner.Instance.GetEnemyTransform(word);
                var position = transform.position;
                Quaternion quaternion =
                    Quaternion.FromToRotation(Vector2.up, (Vector2) (enemyPos.position - position));
                GameObject bullet = Instantiate(bulletPrefab, position, quaternion);
                bullet.SetActive(true);

                _currentString = "";
                typingDisplay.text = _currentString;
                _animator.SetTrigger(attackTriggerID);
                if (fireSound != null) fireSound.Play();
            }
            else
            {
                WarningTypeWrong();
            }
        }

        void WarningTypeWrong()
        {
            Debug.Log("Type Wrong!");
            _currentString = "";
            typingDisplay.text = _currentString;
            CameraShake.Instance.Shake(0.5f);
        }
    }
}