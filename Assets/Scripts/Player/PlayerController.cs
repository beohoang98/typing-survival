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
    
        private string currentString = "";
        [SerializeField] private TMP_InputField typingDisplay;
        [SerializeField] private GameObject _bulletPrefab;
         
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
        }
    
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
    
            }
            
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Shoot(currentString);
            } 
            
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Debug.Log("Backspace");
                if (currentString.Length > 0)
                {
                    currentString = currentString.Remove(currentString.Length - 1, 1);
                    typingDisplay.text = currentString;
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
                currentString += c.ToString().ToUpper();
                typingDisplay.text = currentString;
            }
        }

        void Shoot(string word)
        {
            if (WaveSpawner.Instance.HasEnemy(word))
            {
                Transform enemyPos = WaveSpawner.Instance.GetEnemyTransform(word);
                Quaternion quaternion = Quaternion.FromToRotation(Vector2.up, (Vector2)(enemyPos.position - transform.position));
                GameObject bullet = Instantiate(_bulletPrefab, transform.position, quaternion);
                bullet.SetActive(true);
                
                currentString = "";
                typingDisplay.text = currentString;
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
