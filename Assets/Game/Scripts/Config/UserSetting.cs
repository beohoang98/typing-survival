using System;
using UnityEngine;

namespace Game.Scripts.Config
{
    [Serializable]
    public class UserSetting
    {
        private int _level = 1;
        private Resolution _res = Display.of[Display.AvailableResolution._1080p];
        private bool _isFullscreen = true;

        public void Apply()
        {
            Screen.SetResolution(_res.width, _res.height, _isFullscreen, _res.refreshRate);
        }

        public Resolution Res
        {
            get => _res;
            set
            {
                _res = value;
                Apply();
                Save();
            }
        }

        public bool Fullscreen
        {
            get => _isFullscreen;
            set
            {
                _isFullscreen = value;
                Apply();
                Save();
            }
        }

        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                Apply();
                Save();
            }
        }

        public void Save()
        {
            PlayerPrefs.SetInt("level", _level);
            PlayerPrefs.SetString("resolution", $"{_res.width}x{_res.height}");
            PlayerPrefs.SetInt("isFullscreen", _isFullscreen ? 1 : 0);
            PlayerPrefs.Save();
        }

        public static UserSetting Load()
        {
            UserSetting setting = Read();
            setting.Apply();
            return setting;
        }

        static UserSetting Read()
        {
            UserSetting setting = new UserSetting();
            setting._level = PlayerPrefs.GetInt("level", 1);
            setting._res = Display.of[PlayerPrefs.GetString("resolution", Display.AvailableResolution._720p)];
            setting._isFullscreen = PlayerPrefs.GetInt("isFullscreen", 0) == 1;
            return setting;
        }
    }
}