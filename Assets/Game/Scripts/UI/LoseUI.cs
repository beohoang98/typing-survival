using Game.Scripts.Game;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class LoseUI : MonoBehaviour
    {
        public static LoseUI instance;
        [SerializeField] private GameObject wrapper;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
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