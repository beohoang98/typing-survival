using DG.Tweening;
using Game.Scripts.Config;
using Game.Scripts.Game;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Display = Game.Scripts.Config.Display;

namespace Game.Scripts.UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private LevelConfig levelConfig;
        [SerializeField] private InputActionAsset inputActionAsset;
        [SerializeField] private GameObject wrapper;

        [Header("Main Menu")] [SerializeField] private GameObject mainMenuUI;
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingButton;
        [SerializeField] private Button creditButton;
        [SerializeField] private Button quitButton;

        [Header("Setting Menu")] [SerializeField]
        private GameObject settingUI;

        [SerializeField] private Dropdown displayDropdown;
        [SerializeField] private Toggle fullscreenToggle;
        [SerializeField] private Button settingBackButton;

        [Header("Play Menu")] [SerializeField] private GameObject playMenuUI;
        [SerializeField] private Button storyModeButton;
        [SerializeField] private Button survivalModeButton;
        [SerializeField] private Button playBackButton;


        private UserSetting _setting;
        private InputAction _escAction;

        #region Unity Functions

        private void Awake()
        {
            // DontDestroyOnLoad(gameObject);

            _setting = UserSetting.Load();
            settingUI.SetActive(false);
            playMenuUI.SetActive(false);
            playMenuUI.transform.localScale = Vector3.zero;

            playButton.onClick.AddListener(Play);
            settingButton.onClick.AddListener(OpenSetting);
            // creditButton.onClick.AddListener();
            // quitButton.onClick.AddListener(Quit);
            displayDropdown.onValueChanged.AddListener(HandleResoChange);
            fullscreenToggle.onValueChanged.AddListener(HandleFullscreenChecked);
            settingBackButton.onClick.AddListener(BackMainMenu);

            storyModeButton.onClick.AddListener(StoryModePressed);
            survivalModeButton.onClick.AddListener(SurvivalModePressed);
            playBackButton.onClick.AddListener(PlayMenuBackPressed);

            _escAction = inputActionAsset.FindAction("ESC");
            _escAction.Enable();
        }

        private void Start()
        {
            displayDropdown.value = displayDropdown.options.FindIndex(data => data.text == _setting.Res.ToString());
            fullscreenToggle.isOn = _setting.Fullscreen;

            bool isMenuScene = SceneManager.GetActiveScene().name.Equals("Menu");

            Text quitText = quitButton.GetComponentInChildren<Text>();
            quitText.text = (isMenuScene ? "Quit" : "Back to menu");

            playButton.gameObject.SetActive(isMenuScene);
            wrapper.SetActive(isMenuScene);
            if (isMenuScene)
            {
                EventSystem.current.SetSelectedGameObject(playButton.gameObject);
                quitButton.onClick.RemoveAllListeners();
                quitButton.onClick.AddListener(Quit);
            }
            else
            {
                quitButton.onClick.RemoveAllListeners();
                quitButton.onClick.AddListener(BackToMenu);
            }
        }

        private void OnEnable()
        {
            _escAction.started += OpenMenu;
        }

        private void OnDisable()
        {
            _escAction.started -= OpenMenu;
        }

        #endregion

        #region Public Methods

        public void OpenMenu(InputAction.CallbackContext context)
        {
            Debug.Log("ESC");
            if (!context.started) return;
            if (SceneManager.GetActiveScene().name.Equals("Menu")) return;
            if (wrapper.activeSelf)
            {
                Time.timeScale = 1;
                wrapper.transform.DOScale(Vector3.zero, 0.2f)
                    .OnComplete(() => wrapper.SetActive(false));
                EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                Time.timeScale = 0;
                wrapper.SetActive(true);
                wrapper.transform.DOScale(Vector3.one, 0.2f);
                EventSystem.current.SetSelectedGameObject(playButton.gameObject);
            }
        }

        private async void Play()
        {
            await mainMenuUI.transform.DOScale(Vector3.zero, 0.2f).AsyncWaitForCompletion();
            mainMenuUI.SetActive(false);
            playMenuUI.SetActive(true);
            await playMenuUI.transform.DOScale(Vector3.one, 0.2f).AsyncWaitForCompletion();
            EventSystem.current.SetSelectedGameObject(storyModeButton.gameObject);
        }

        private void BackToMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        void OpenSetting()
        {
            settingUI.transform.localScale = Vector3.zero;
            settingUI.SetActive(true);
            settingUI.transform.DOScale(new Vector3(1, 1, 1), 0.2f)
                .SetEase(Ease.OutBounce);
            EventSystem.current.SetSelectedGameObject(settingBackButton.gameObject);
        }

        void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
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
            settingUI.transform.DOScale(Vector3.zero, 0.2f)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => settingUI.SetActive(false));
        }

        private void StoryModePressed()
        {
            PlayMenuBackPressed();
            wrapper.SetActive(false);
            wrapper.transform.localScale = Vector3.zero;
            SceneManager.LoadScene("LevelChooser", LoadSceneMode.Single);
        }

        private void SurvivalModePressed()
        {
            PlayMenuBackPressed();
            int sceneIndex = SceneUtility.GetBuildIndexByScenePath("Survival");
            SceneHolder.sceneIndex = sceneIndex;
            wrapper.SetActive(false);
            wrapper.transform.localScale = Vector3.zero;
            SceneManager.LoadScene("Scenes/Loading");
        }

        private async void PlayMenuBackPressed()
        {
            await playMenuUI.transform.DOScale(Vector3.zero, 0.2f).AsyncWaitForCompletion();
            playMenuUI.SetActive(false);
            mainMenuUI.SetActive(true);
            await mainMenuUI.transform.DOScale(Vector3.one, 0.2f).AsyncWaitForCompletion();
            EventSystem.current.SetSelectedGameObject(playButton.gameObject);
        }

        #endregion
    }
}