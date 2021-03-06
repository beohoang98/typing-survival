﻿using Game.Scripts.Config;
using Game.Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game.Scripts.Enemy
{
    public class EnemyWord : MonoBehaviour
    {
        private string _word;

        [FormerlySerializedAs("_textDisplay")] [SerializeField]
        private TextMeshProUGUI textDisplay;

        private readonly int _points = 100;
        private WaveSpawner _manager;

        public UnityEvent killedEvent;

        private void Start()
        {
            Canvas canvas = GetComponentInChildren<Canvas>();
            canvas.worldCamera = Camera.current;
            _manager = FindObjectOfType<WaveSpawner>();
        }

        public void SetWord(string w)
        {
            _word = w;
            textDisplay.SetText(_word);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameTag.Bullet))
            {
                GotKilled();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log(LayerMask.LayerToName(other.collider.gameObject.layer), this);
        }

        private void GotKilled()
        {
            _manager.KillEnemy(_word);
            GameController.Instance.IncreaseScore(_points);
            killedEvent.Invoke();
            Destroy(gameObject);
        }
    }
}