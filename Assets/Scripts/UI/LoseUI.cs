using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoseUI : MonoBehaviour
    {
        public static LoseUI Instance;
        [SerializeField] private GameObject wrapper;
        [SerializeField] private Button retryBtn;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            retryBtn.onClick.AddListener(OnRetryClicked);
            wrapper.SetActive(false);
        }

        public void OnRetryClicked()
        {
            GameController.Instance.Retry();
        }

        public void Show()
        {
            wrapper.SetActive(true);
        }

        public void Hide()
        {
            wrapper.SetActive(false);
        }
    }
}