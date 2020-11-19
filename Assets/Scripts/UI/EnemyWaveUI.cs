using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [DisallowMultipleComponent]
    public class EnemyWaveUI : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Canvas canvas;
        [SerializeField] private int numberOfWave = 3;
        [SerializeField] private Image waveIconPrefab;

        private List<Image> waveIcons = new List<Image>();
        private int currentProgress = 0;

        private void Start()
        {
            if (slider == null) slider = GetComponentInChildren<Slider>();
            if (canvas == null) canvas = GetComponentInChildren<Canvas>();
            ReDrawIcons();
            currentProgress = 0;
            slider.value = 0;
        }

        private void ReDrawIcons()
        {
            if (waveIconPrefab != null)
            {
                waveIcons.Clear();
                slider.maxValue = numberOfWave;

                waveIconPrefab.gameObject.SetActive(false);
                Image[] oldIcons = GetComponentsInChildren<Image>();
                foreach (var oldIcon in oldIcons)
                {
                    if (!oldIcon.gameObject.name.Equals("WaveIcon")) continue;
                    oldIcon.transform.parent = null;
                    Destroy(oldIcon.gameObject);
                }

                float width = slider.GetComponent<RectTransform>().rect.width;
                float iconSize = waveIconPrefab.GetComponent<RectTransform>().rect.width;
                float margin = width / numberOfWave;
                for (int idx = 0; idx < numberOfWave; ++idx)
                {
                    GameObject newIcon = Instantiate(waveIconPrefab.gameObject, canvas.transform);
                    newIcon.SetActive(true);
                    newIcon.transform.localPosition =
                        new Vector3(slider.transform.localPosition.x - width / 2 + margin * (idx + 1) - iconSize / 2,
                            slider.transform.localPosition.y, 0);
                    Image newIconImage = newIcon.GetComponent<Image>();
                    newIconImage.color = Color.gray;
                    waveIcons.Add(newIconImage);
                }
            }
        }

        public void SetNumberOfWave(int inNumberOfWave)
        {
            numberOfWave = inNumberOfWave;
            ReDrawIcons();
        }

        public int GetCurrentProgress() => currentProgress;

        public void SetCurrentProgress(int value)
        {
            currentProgress = value;
            slider.value = value;
            waveIcons[value - 1].color = Color.yellow;
        }

        public void IncreaseCheckpoint()
        {
            SetCurrentProgress(currentProgress + 1);
        }

        private void Reset()
        {
            Start();
        }
    }
}