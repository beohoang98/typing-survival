using Game.Scripts.Config;
using Game.Scripts.Enemy;
using Game.Scripts.Saves;
using Game.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Game
{
    [DisallowMultipleComponent]
    public class GameController : MonoBehaviour
    {
        private static GameController _instance;
        public static GameController Instance => _instance;

        private int _score;
        public int GetScore() => _score;

        [SerializeField] private TextMeshProUGUI scoreDisplay;
        [SerializeField] private InputActionAsset input;
        private InputActionMap _playerInputActionMap;
        [SerializeField] private LevelConfig levelConfig;

        private void Awake()
        {
            if (!_instance)
            {
                _instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }

            _playerInputActionMap = input.FindActionMap("Player");
        }

        private void Start()
        {
            _score = 0;
            _playerInputActionMap.Enable();
        }

        public void IncreaseScore(int amount)
        {
            _score += amount;
            UpdateScoreToUi();
        }

        void UpdateScoreToUi()
        {
            scoreDisplay.SetText(_score.ToString("#,###"));
        }

        public void Win()
        {
            uint level = UserScore.instance.playingLevel;
            if (!UserScore.instance.levelScores.ContainsKey(level))
            {
                UserScore.instance.levelScores[level] = new LevelScore() {level = level, score = 0};
            }

            uint maxScore = UserScore.instance.levelScores[level].score;
            if (_score > maxScore)
            {
                UserScore.instance.levelScores[level].score = (uint) _score;
                UserScore.instance.Save();
            }

            WinUI.instance.ScoreText.text = _score.ToString("#,###");
            WinUI.instance.Show();
        }

        public void Lose()
        {
            _playerInputActionMap.Disable();
            WaveSpawner.Instance.Stop();
            LoseUI.instance.Show();
        }

        public void Retry()
        {
            _playerInputActionMap.Disable();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void NextLevel()
        {
            uint currentLevel = UserScore.instance.playingLevel;
            uint nextLevel = currentLevel + 1;
            UserScore.instance.playingLevel = nextLevel;
            UserScore.instance.Save();

            SceneHolder.sceneIndex = levelConfig.GetSceneIndexOfLevel(nextLevel);
            SceneManager.LoadScene("Scenes/Loading");
        }
    }
}