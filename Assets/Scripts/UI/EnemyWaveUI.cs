using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [DisallowMultipleComponent]
    public class EnemyWaveUI : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        private int _numberOfWave = 3;
        [SerializeField] private Image waveIconPrefab;

        private readonly List<Image> _waveIcons = new List<Image>();
        private int _currentProgress;

        private void Start()
        {
            if (slider == null) slider = GetComponentInChildren<Slider>();
            ReDrawIcons();
            _currentProgress = 0;
            slider.value = 0;
        }

        private void ReDrawIcons()
        {
            if (waveIconPrefab != null)
            {
                _waveIcons.Clear();
                slider.maxValue = _numberOfWave;

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
                float margin = width / _numberOfWave;
                for (int idx = 0; idx < _numberOfWave; ++idx)
                {
                    GameObject newIcon = Instantiate(waveIconPrefab.gameObject, transform);
                    newIcon.SetActive(true);
                    Vector2 sliderLocalPos = slider.transform.localPosition;
                    newIcon.transform.localPosition =
                        new Vector3(sliderLocalPos.x - width / 2 + margin * (idx + 1) - iconSize / 2,
                            sliderLocalPos.y, 0);
                    Image newIconImage = newIcon.GetComponent<Image>();
                    newIconImage.color = Color.gray;
                    _waveIcons.Add(newIconImage);
                }
            }
        }

        public void SetNumberOfWave(int inNumberOfWave)
        {
            _numberOfWave = inNumberOfWave;
            ReDrawIcons();
        }

        public int GetCurrentProgress() => _currentProgress;

        public void SetCurrentProgress(int value)
        {
            _currentProgress = value;
            slider.value = value;
            _waveIcons[value - 1].color = Color.yellow;
        }

        public void IncreaseCheckpoint()
        {
            SetCurrentProgress(_currentProgress + 1);
        }

        private void Reset()
        {
            Start();
        }
    }
}