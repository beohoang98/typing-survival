using Config;
using Game;
using UnityEngine;
using UnityEngine.InputSystem;
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
        [SerializeField] private InputActionAsset inputActionAsset;
        [SerializeField] private GameObject cursorHover;

        private UserSetting _setting;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _setting = UserSetting.Load();
            settingUI.SetActive(false);
            playButton.onClick.AddListener(Play);
            settingButton.onClick.AddListener(OpenSetting);
            // creditButton.onClick.AddListener();
            quitButton.onClick.AddListener(Quit);
            displayDropdown.onValueChanged.AddListener(HandleResoChange);
            fullscreenToggle.onValueChanged.AddListener(HandleFullscreenChecked);
            backButton.onClick.AddListener(BackMainMenu);
            gameObject.SetActive(true);

            InputAction escAction = inputActionAsset.FindAction("ESC");
            escAction.started += OpenMenu;
            escAction.Enable();
        }

        private void OpenMenu(InputAction.CallbackContext context)
        {
            Debug.Log("ESC");
            if (SceneManager.GetActiveScene().name.Equals("Menu")) return;
            if (gameObject.activeSelf)
            {
                Time.timeScale = 1;
                gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                gameObject.SetActive(true);
            }
        }

        void Play()
        {
            int level = _setting.Level;
            string levelSceneName = $"Scenes/Level{level}";
            int buildIndex = SceneUtility.GetBuildIndexByScenePath(levelSceneName);
            SceneHolder.SceneIndex = buildIndex;
            gameObject.SetActive(false);
            SceneManager.LoadScene("Scenes/Loading");
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