using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    [DisallowMultipleComponent]
    public class LoadingController : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        private void Start()
        {
            slider.value = 0f;
            slider.maxValue = 1f;
            StartCoroutine(Load());
        }

        IEnumerator Load()
        {
            int sceneIdx = SceneHolder.SceneIndex;
            var operation = SceneManager.LoadSceneAsync(sceneIdx);

            while (!operation.isDone)
            {
                slider.value = operation.progress;
                yield return null;
            }
        }
    }
}