using Config;
using Enemy;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PolygonCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        private string _currentString = "";
        [SerializeField] private TMP_InputField typingDisplay;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private InputActionAsset inputActionAsset;
        private Animator _animator;
        private readonly int attackTriggerID = Animator.StringToHash("attack");

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            if (typingDisplay)
            {
                typingDisplay.text = "";
            }

            _animator = GetComponent<Animator>();
            InputAction deleteAction = inputActionAsset.FindAction("Delete");
            InputAction submitAction = inputActionAsset.FindAction("Submit");

            deleteAction.performed += HandleBackspace;
            submitAction.performed += Shoot;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameTag.Enemy))
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

        void HandleBackspace(InputAction.CallbackContext ctx)
        {
            if (_currentString.Length > 0)
            {
                _currentString = _currentString.Remove(_currentString.Length - 1, 1);
                typingDisplay.text = _currentString;
            }
        }

        void Shoot(InputAction.CallbackContext ctx)
        {
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
        }
    }
}