using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager instance;
        [SerializeField] private GameObject enemyPrefab;
        private float _radius = 5f;

        public readonly Dictionary<string, EnemyWord> enemies = new Dictionary<string, EnemyWord>();
        [SerializeField] private float interval = 2f;
        private float timeCount = 0f;

        private void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Update()
        {
            timeCount += Time.deltaTime;
            if (timeCount >= interval)
            {
                timeCount = 0;
                float randomDeg = Random.Range(0, 360);
                Vector2 pos = Quaternion.Euler(0, 0, randomDeg) * (Vector2.up * _radius);
                GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.Euler(0, 0, 0));
                EnemyWord enemyWord = enemy.GetComponent<EnemyWord>();
                string word;
                do {
                    word = ((char)('a' + Random.Range(0, 26))).ToString().ToUpper();
                } while (enemies.ContainsKey(word));
                
                enemyWord.SetWord(word);
                enemies.Add(word, enemyWord);
                Debug.Log($"Generate enemy {word}");
                
                enemy.SetActive(true);
            }
        }

        public void Spawn(string word)
        {
            
        }

        public void Kill(string word)
        {
            if (enemies.ContainsKey(word))
            {
                enemies[word].GotKilled();
                enemies.Remove(word);
            }
        }

        public bool Has(string word)
        {
            return enemies.ContainsKey(word);
        }
    }
}