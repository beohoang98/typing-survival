using System;
using Enemy;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        private static GameController _instance;
        public static GameController Instance => _instance;

        private int _score;
        public int GetScore() => _score;

        [SerializeField] private TextMeshProUGUI _scoreDisplay;
        [SerializeField] private WinUI winUI;
        [SerializeField] private LoseUI loseUI;

        private void Start()
        {
            if (!_instance)
            {
                _instance = this;
            }
            else
            {
                Destroy(this);
            }

            _score = 0;
        }

        private void Update()
        {
            
        }

        public void IncreaseScore(int amount)
        {
            _score += amount;
            UpdateScoreToUi();
        }

        void UpdateScoreToUi()
        {
            _scoreDisplay.SetText(_score.ToString("#,###"));
        }

        public void Win()
        {
            winUI.ScoreText.text = _score.ToString("#,###");
            winUI.Show();
        }

        public void Lose()
        {
            WaveSpawner.Instance.Stop();
            loseUI.Show();
        }

        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}