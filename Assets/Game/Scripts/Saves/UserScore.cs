using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.Saves
{
    [Serializable]
    public class UserScore : ISerializationCallbackReceiver
    {
        public static string savePath = $"{Application.persistentDataPath}/scores.dat";
        public static UserScore instance = LoadLocal();

        public uint playingLevel = 1;
        public UserInfo userInfo = null;
        public Dictionary<uint, LevelScore> levelScores = new Dictionary<uint, LevelScore>();
        public List<LevelScore> levelScoresMap = new List<LevelScore>();

        public static UserScore LoadLocal()
        {
            if (File.Exists(savePath))
            {
                StreamReader stream = new StreamReader(savePath);
                string data = stream.ReadToEnd();
                UserScore score = JsonUtility.FromJson<UserScore>(data);
                stream.Close();
                return score;
            }
            else
            {
                return new UserScore();
            }
        }

        public void Save()
        {
            string data = JsonUtility.ToJson(this);
            StreamWriter writer = new StreamWriter(savePath);
            writer.Write(data);
            writer.Close();
        }

        public uint GetLatestLevel()
        {
            if (levelScores.Count == 0) return 0;
            return levelScores.Keys.Max();
        }

        public bool IsLevelUnlocked(uint level)
        {
            return level <= GetLatestLevel() + 1;
        }

        public void OnBeforeSerialize()
        {
            levelScoresMap.Clear();
            foreach (var pair in levelScores)
            {
                levelScoresMap.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            levelScores.Clear();
            foreach (var score in levelScoresMap)
            {
                levelScores.Add(score.level, score);
            }
        }
    }
}