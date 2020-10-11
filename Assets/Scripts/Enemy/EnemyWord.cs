using System;
using Game;
using TMPro;
using UnityEngine;

namespace Enemy
{
    public class EnemyWord : MonoBehaviour
    {
        private string _word;
        [SerializeField]
        private TextMeshProUGUI _textDisplay;

        private readonly int _points = 100;
        private WaveSpawner _manager;
        
        private void Start()
        {
            Canvas canvas = GetComponentInChildren<Canvas>();
            canvas.worldCamera = Camera.current;
            _manager = FindObjectOfType<WaveSpawner>();
        }

        private void Update()
        {
           
        }

        public void SetWord(string w)
        {
            _word = w;
            _textDisplay.SetText(_word);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("bullet"))
            {
                GotKilled();
            }
        }

        public void GotKilled()
        {
            _manager.KillEnemy(_word);
            GameController.Instance.IncreaseScore(_points);
            Destroy(gameObject);
        }
    }
}