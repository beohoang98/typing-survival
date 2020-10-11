using System;
using System.IO;
using UnityEngine;

namespace Config
{
    [Serializable]
    public class UserSetting
    {
        private static string _path = "user_config.json";

        private int level = 1;
        Resolution _res = Display.of["1280x720"];
        bool _fullscreen = true;
        
        public void Apply()
        {
            Screen.SetResolution(_res.width, _res.height, _fullscreen, _res.refreshRate);
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
            get => _fullscreen;
            set
            {
                _fullscreen = value;
                Apply();
                Save();
            }
        }

        public int Level
        {
            get => level;
            set
            {
                level = value;
                Apply();
                Save();
            }
        }
        
        public void Save()
        {
            string json = JsonUtility.ToJson(this);
            StreamWriter writer = new StreamWriter(_path);
            writer.WriteLine(json);
            writer.Close();
        }
        
        public static UserSetting Load()
        {
            UserSetting setting = Read();
            setting.Apply();
            return setting;
        }

        static UserSetting Read()
        {
            if (!File.Exists(_path))
            {
                var setting = new UserSetting();
                setting.Save();
                return setting;
            }

            try
            {
                StreamReader reader = new StreamReader(_path);
                string json = reader.ReadToEnd();
                UserSetting setting = JsonUtility.FromJson<UserSetting>(json);
                return setting;
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
                return new UserSetting();
            }
        }
    }
}