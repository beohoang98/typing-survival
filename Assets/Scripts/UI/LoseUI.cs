using System;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoseUI : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Button retryBtn;

        private void Start()
        {
            canvas.worldCamera = Camera.current;
            retryBtn.onClick.AddListener(OnRetryClicked);
            canvas.gameObject.SetActive(false);
        }

        void OnRetryClicked()
        {
            GameController.Instance.Retry();
        }

        public void Show()
        {
            canvas.gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            canvas.gameObject.SetActive(false);
        }
    }
}