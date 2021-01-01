using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Config
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Game/Config/Level Config", order = 0)]
    public class LevelConfig : ScriptableObject, ISerializationCallbackReceiver
    {
        private readonly Dictionary<uint, string> _scenes = new Dictionary<uint, string>();
        [Header("Dont Edit")] public List<LevelSceneIndex> scenesSaved = new List<LevelSceneIndex>();

        [Serializable]
        public class LevelSceneIndex
        {
            public uint level;
            public string sceneName;
        }

        public void OnBeforeSerialize()
        {
            scenesSaved.Clear();
            foreach (var el in _scenes)
            {
                scenesSaved.Add(new LevelSceneIndex()
                {
                    sceneName = el.Value,
                    level = el.Key
                });
            }
        }

        public void OnAfterDeserialize()
        {
            _scenes.Clear();
            foreach (var levelSceneIndex in scenesSaved)
            {
                _scenes.Add(levelSceneIndex.level, levelSceneIndex.sceneName);
            }
        }

#if UNITY_EDITOR
        [Header("Edit Below")] public LevelScene[] levels;

        [Serializable]
        public class LevelScene
        {
            public uint level;
            public SceneAsset scene;
        }

        private void OnValidate()
        {
            foreach (var level in levels)
            {
                if (_scenes.ContainsKey(level.level))
                {
                    _scenes[level.level] = level.scene.name;
                    continue;
                }

                _scenes.Add(level.level, level.scene.name);
            }
        }
#endif

        public int GetSceneIndexOfLevel(uint level)
        {
            if (_scenes.ContainsKey(level))
                return SceneUtility.GetBuildIndexByScenePath(_scenes[level]);
            return -1;
        }
    }
}