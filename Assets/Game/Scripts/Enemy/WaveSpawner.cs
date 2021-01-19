using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Game;
using Game.Scripts.LevelWord;
using Game.Scripts.UI;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class WaveSpawner : MonoBehaviour
    {
        private static WaveSpawner _instance;
        public static WaveSpawner Instance => _instance;

        [Serializable]
        public class Wave
        {
            [Header("Setting")] public float delay = 1f;
            public float totalCount = 5f;
            public AbstractPositionSpawner positionSpawner;

            [Tooltip("Effect after spawned"), CanBeNull]
            public GameObject particleSystemEffect;

            [Tooltip("Wait time between enemies, also mean 'Delay'")]
            public float rate = 1f;

            [Header("Object")] public WordSpawner wordSpawner;
            public GameObject enemyPrefab;

            [HideInInspector] public int killedCount = 0;
        }

        [Header("Data")] [SerializeField] private Wave[] waves;
        [SerializeField] private EnemyWaveUI enemyWaveUI;

        private enum WaveState
        {
            Delay,
            Spawning,
            End,
        }

        private WaveState _state = WaveState.Delay;
        // public WaveState State => _state;
        // private Wave _currentWave;

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
                // _currentWave = wave;
                yield return DoSpawn(wave);
            }

            _state = WaveState.End;
        }

        IEnumerator DoSpawn(Wave wave)
        {
            int spawnedCount = 0;
            wave.killedCount = 0;
            while (spawnedCount < wave.totalCount)
            {
                if (_isStop)
                {
                    yield break;
                }

                Vector2 pos = wave.positionSpawner.GetRandomPosition();

                GameObject enemy = Instantiate(wave.enemyPrefab, pos, Quaternion.identity);
                EnemyWord enemyWord = enemy.GetComponent<EnemyWord>();
                string word;
                do
                {
                    word = wave.wordSpawner.GetWord().ToUpper();
                } while (_enemies.ContainsKey(word));

                enemyWord.SetWord(word);
                enemyWord.killedEvent.AddListener((() => ++wave.killedCount));
                _enemies.Add(word, enemyWord);

                Debug.Log($"Generate enemy {word}");
                enemy.SetActive(true);
                if (wave.particleSystemEffect != null)
                {
                    Instantiate(wave.particleSystemEffect, pos, Quaternion.identity);
                }

                yield return new WaitForSeconds(wave.rate);
                ++spawnedCount;
            }

            StartCoroutine(WaitToClearEnemyToIncreaseCheckpoint(wave));
        }

        private IEnumerator WaitToClearEnemyToIncreaseCheckpoint(Wave wave)
        {
            while (wave.killedCount < wave.totalCount)
            {
                yield return null;
            }

            enemyWaveUI.IncreaseCheckpoint();
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