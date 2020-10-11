using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WinUI : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Text scoreText;
        [SerializeField] private Image background;
        
        public Text ScoreText
        {
            get => scoreText;
            set => scoreText = value;
        }

        public Canvas Canvas => canvas;

        private void Start()
        {
            canvas.worldCamera = Camera.current;
            canvas.gameObject.SetActive(false);
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