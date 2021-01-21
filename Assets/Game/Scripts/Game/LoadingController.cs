using System.Collections;
using Game.Scripts.Config;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scripts.Game
{
    [DisallowMultipleComponent]
    public class LoadingController : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Image splashScreen;
        [SerializeField] private SplashScreenConfig splashScreenConfig;

        private void Start()
        {
            slider.value = 0f;
            slider.maxValue = 1f;
            StartCoroutine(Load());
        }

        private IEnumerator Load()
        {
            int sceneIdx = SceneHolder.sceneIndex;
            Sprite sceneImage = splashScreenConfig.GetSpriteOf(sceneIdx);
            if (sceneImage != null)
                splashScreen.sprite = sceneImage;

            var operation = SceneManager.LoadSceneAsync(sceneIdx);

            while (!operation.isDone)
            {
                slider.value = operation.progress;
                yield return null;
            }
        }
    }
}