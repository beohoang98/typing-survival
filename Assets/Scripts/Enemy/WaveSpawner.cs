﻿using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using LevelWord;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class WaveSpawner : MonoBehaviour
    {
        private static WaveSpawner _instance;
        public static WaveSpawner Instance => _instance;

        [Serializable]
        public class Wave
        {
            public float delay = 1f;
            public float spawnTime = 5f;
            public float rate = 1f;
            public WordSpawner wordSpawner;
            public GameObject enemyPrefab;
        }

        [SerializeField]
        private Wave[] waves;

        [SerializeField]
        private float radius = 10f;

        public enum WaveState
        {
            DELAY,
            SPAWNING,
            END,
        }
        private WaveState _state = WaveState.DELAY;
        public WaveState State => _state;
        private Wave _currentWave;

        private bool _isStop = false;

        private Dictionary<string, EnemyWord> _enemies = new Dictionary<string, EnemyWord>();

        private void Start()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            if (waves.Length > 0)
            {
                foreach (Wave wave in waves)
                {
                    wave.wordSpawner.Load();
                }
            }

            StartCoroutine(DoSpawnLoop());
        }

        private void Update()
        {
            if (_state == WaveState.END && _enemies.Count == 0)
            {
                GameController.Instance.Win();
            }
        }

        
        IEnumerator DoSpawnLoop()
        {
            foreach (var wave in waves)
            {
                _state = WaveState.DELAY;
                yield return new WaitForSeconds(wave.delay);
                Debug.Log("Start new wave...");
                _state = WaveState.SPAWNING;
                _currentWave = wave;
                yield return DoSpawn(wave);
            }

            _state = WaveState.END;
        }

        IEnumerator DoSpawn(Wave wave)
        {
            float total = 0f;
            while (total < wave.spawnTime)
            {
                if (_isStop)
                {
                    yield break;
                }
                
                float randomDeg = Random.Range(0, 360);
                Vector2 pos = Quaternion.Euler(0, 0, randomDeg) * (Vector2.up * radius);
                GameObject enemy = Instantiate(wave.enemyPrefab, pos, Quaternion.Euler(0, 0, 0));
                EnemyWord enemyWord = enemy.GetComponent<EnemyWord>();
                string word;
                do
                {
                    word = wave.wordSpawner.GetWord().ToUpper();
                } while (_enemies.ContainsKey(word));

                enemyWord.SetWord(word);
                _enemies.Add(word, enemyWord);

                Debug.Log($"Generate enemy {word}");
                enemy.SetActive(true);
                
                yield return new WaitForSeconds(wave.rate);
                total += wave.rate;
            }
        }
        
        public bool HasEnemy(string word)
        {
            return _enemies.ContainsKey(word);
        }

        public void KillEnemy(string word)
        {
            _enemies.Remove(word);
        }

        public Transform GetEnemyTransform(string word)
        {
            return _enemies[word].gameObject.transform;
        }

        public void Stop()
        {
            _isStop = true;
        }
    }
}