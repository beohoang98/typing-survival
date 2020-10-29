using System;
using Enemy;
using Game;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance;
    
        private string _currentString = "";
        [SerializeField] private TMP_InputField typingDisplay;
        [SerializeField] private GameObject bulletPrefab;
        private Animator _animator;
         
        // Start is called before the first frame update
        void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
    
            if (typingDisplay)
            { 
                typingDisplay.text = "";
            }

            _animator = GetComponent<Animator>();
        }
    
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
    
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot(_currentString);
            } 
            
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Debug.Log("Backspace");
                if (_currentString.Length > 0)
                {
                    _currentString = _currentString.Remove(_currentString.Length - 1, 1);
                    typingDisplay.text = _currentString;
                }
            }
        }
    
        private void OnGUI()
        {
            if (Event.current.isKey)
            {
                HandleTyping(Event.current.character);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("enemy"))
            {
                GameController.Instance.Lose();
            }
        }

        void HandleTyping(char c)
        {
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
            {
                _currentString += c.ToString().ToUpper();
                typingDisplay.text = _currentString;
            }
        }

        void Shoot(string word)
        {
            if (WaveSpawner.Instance.HasEnemy(word))
            {
                Transform enemyPos = WaveSpawner.Instance.GetEnemyTransform(word);
                Quaternion quaternion = Quaternion.FromToRotation(Vector2.up, (Vector2)(enemyPos.position - transform.position));
                GameObject bullet = Instantiate(bulletPrefab, transform.position, quaternion);
                bullet.SetActive(true);
                
                _currentString = "";
                typingDisplay.text = _currentString;
                _animator.SetTrigger("attack");
            }
            else
            {
                WarningTypeWrong();
            }
        }

        void WarningTypeWrong()
        {
            Debug.Log("Type Wrong!");
        }
    }
}
