using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Config
{
    public class SplashScreenConfig : MonoBehaviour
    {
        public List<LevelSplashScreen> splashScreens;
        private readonly Dictionary<int, Sprite> _mapSprite = new Dictionary<int, Sprite>();

        [Serializable]
        public class LevelSplashScreen
        {
            public int sceneIndex;
            public Sprite image;
        }

        private void Start()
        {
            _mapSprite.Clear();
            foreach (LevelSplashScreen levelSplashScreen in splashScreens)
            {
                _mapSprite.Add(levelSplashScreen.sceneIndex, levelSplashScreen.image);
            }
        }

        public Sprite GetSpriteOf(int sceneIndex)
        {
            if (_mapSprite.ContainsKey(sceneIndex))
                return _mapSprite[sceneIndex];
            return null;
        }
    }
}