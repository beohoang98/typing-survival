using System.Linq;
using System.Text.RegularExpressions;
using Game.Scripts.Config;
using Game.Scripts.Game;
using Game.Scripts.Saves;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class LevelListUI : MonoBehaviour
    {
        [SerializeField] private LevelConfig levelConfig;
        [SerializeField] private Button[] levelButtons;

        private void Start()
        {
            foreach (Button button in levelButtons)
            {
                uint level = GetLevelFromButton(button);
                button.interactable = UserScore.instance.IsLevelUnlocked(level);
                if (button.interactable) button.onClick.AddListener(() => OnLevelChosen(level));
            }
        }

        private void Reset()
        {
            Button[] buttons = GetComponentsInChildren<Button>();
            levelButtons = buttons.Where(btn => btn.name.Contains("Level")).ToArray();
        }

        private void OnLevelChosen(uint level)
        {
            if (UserScore.instance.IsLevelUnlocked(level))
            {
                SceneHolder.sceneIndex = levelConfig.GetSceneIndexOfLevel(level);
                SceneManager.LoadScene("Loading");
            }
        }

        private static uint GetLevelFromButton(Button button)
        {
            string levelStr = Regex.Match(button.name, "^Level(\\d+)$").Groups[1].Value;
            uint level = uint.Parse(levelStr);
            return level;
        }
    }
}