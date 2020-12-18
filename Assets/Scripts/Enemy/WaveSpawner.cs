using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using LevelWord;
using UI;
using UnityEngine;
using Random = System.Random;

namespace Enemy
{
    public class WaveSpawner : MonoBehaviour
    {
        private static WaveSpawner _instance;
        public static WaveSpawner Instance => _instance;

        [Serializable]
        public class Wave
        {
            [Header("Setting")] public float delay = 1f;
            public float spawnTime = 5f;

            [Tooltip("Wait time between enemies, also mean 'Delay'")]
            public float rate = 1f;

            [Header("Object")] public WordSpawner wordSpawner;
            public GameObject enemyPrefab;
        }

        [SerializeField] private Wave[] waves;
        [SerializeField] private EnemyWaveUI enemyWaveUI;

        [SerializeField] [Tooltip("Distance from player where enemy will spawn in")]
        private float radius = 10f;

        private Random _random;

        public enum WaveState
        {
            Delay,
            Spawning,
            End,
        }

        private WaveState _state = WaveState.Delay;
        public WaveState State => _state;
        private Wave _currentWave;

        private bool _isStop = false;

        private readonly Dictionary<string, EnemyWord> _enemies = new Dictionary<string, EnemyWord>();

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

            _random = new Random(DateTime.Now.Millisecond);
            enemyWaveUI = FindObjectOfType<EnemyWaveUI>();
            enemyWaveUI.SetNumberOfWave(waves.Length);
            StartCoroutine(DoSpawnLoop());
        }

        private void Update()
        {
            if (_state == WaveState.End && _enemies.Count == 0)
            {
                GameController.Instance.Win();
            }
        }


        IEnumerator DoSpawnLoop()
        {
            foreach (var wave in waves)
            {
                _state = WaveState.Delay;
                yield return new WaitForSeconds(wave.delay);
                Debug.Log("Start new wave...");
                _state = WaveState.Spawning;
                _currentWave = wave;
                yield return DoSpawn(wave);
                enemyWaveUI.IncreaseCheckpoint();
            }

            _state = WaveState.End;
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

                float randomDeg = _random.Next(0, 360);
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