using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WinUI : MonoBehaviour
    {
        public static WinUI Instance;
        [SerializeField] private GameObject wrapper;
        [SerializeField] private Text scoreText;
        [SerializeField] private Image background;

        public Text ScoreText
        {
            get => scoreText;
            set => scoreText = value;
        }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            wrapper.SetActive(false);
        }

        public void Show()
        {
            wrapper.SetActive(true);
        }

        public void Hide()
        {
            wrapper.SetActive(false);
        }

        public void OnNextClicked()
        {
            GameController.Instance.NextLevel();
        }

        public void OnRetryClicked()
        {
            GameController.Instance.Retry();
        }
    }
}