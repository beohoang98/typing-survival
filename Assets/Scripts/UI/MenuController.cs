using System;
using Config;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Display = Config.Display;

namespace UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingButton;
        [SerializeField] private Button creditButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private GameObject settingUI;
        [SerializeField] private Dropdown displayDropdown;
        [SerializeField] private Toggle fullscreenToggle;
        [SerializeField] private Button backButton;

        private UserSetting _setting;

        private void Start()
        {
            _setting = UserSetting.Load();
            settingUI.SetActive(false);
            playButton.onClick.AddListener(Play);
            settingButton.onClick.AddListener(OpenSetting);
            // creditButton.onClick.AddListener();
            quitButton.onClick.AddListener(Quit);
            displayDropdown.onValueChanged.AddListener(HandleResoChange);
            fullscreenToggle.onValueChanged.AddListener(HandleFullscreenChecked);
            backButton.onClick.AddListener(BackMainMenu);
        }

        void Play()
        {
            int level = _setting.Level;
            SceneManager.LoadScene(level);
        }

        void OpenSetting()
        {
            settingUI.SetActive(true);
        }

        void Quit()
        {
            Application.Quit();
        }

        void HandleResoChange(int idx)
        {
            string val = displayDropdown.options[idx].text.Replace(" ", "");
            _setting.Res = Display.of[val];
        }

        void HandleFullscreenChecked(bool isChecked)
        {
            _setting.Fullscreen = isChecked;
        }

        void BackMainMenu()
        {
            settingUI.SetActive(false);
        }
    }
}