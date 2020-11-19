using Enemy;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        private static GameController _instance;
        public static GameController Instance => _instance;

        private int _score;
        public int GetScore() => _score;

        [SerializeField] private TextMeshProUGUI scoreDisplay;
        [SerializeField] private WinUI winUI;
        [SerializeField] private LoseUI loseUI;
        public InputActionAsset playerInputActionAsset;

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
            playerInputActionAsset.Enable();
            foreach (var action in playerInputActionAsset)
            {
                Debug.Log(action.name);
                action.Enable();
            }
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
            winUI.ScoreText.text = _score.ToString("#,###");
            winUI.Show();
        }

        public void Lose()
        {
            playerInputActionAsset.Disable();
            WaveSpawner.Instance.Stop();
            loseUI.Show();
        }

        public void Retry()
        {
            playerInputActionAsset.Disable();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}