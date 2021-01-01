using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace Game.Scripts.Config
{
    [CreateAssetMenu(fileName = "data.config.asset", menuName = "Game/Data/Create Config", order = 0)]
    public class DataFile : ScriptableObject
    {
        private static readonly List<char> QwertyChars = new List<char>() {'q', 'w', 'e', 'r', 't', 'y', 'u', 'i'};
        [SerializeField] private TextAsset file;

        private readonly Dictionary<int, List<string>> _mapOfWordByLength;
        private readonly Dictionary<char, List<string>> _mapOfWordByFirstLetter;
        private readonly List<string> _qwerty5Words;

        private Random _random;

        public DataFile()
        {
            _mapOfWordByLength = new Dictionary<int, List<string>>();
            _mapOfWordByFirstLetter = new Dictionary<char, List<string>>();
            _qwerty5Words = new List<string>();
        }

        public void LoadSync(OnLoadedCallback onLoadedCallback)
        {
            _random = new Random(DateTime.Now.Millisecond);
            string[] lines = Regex.Split(file.text, "[\r\n]+");
            foreach (string word in lines)
            {
                AddCountWordLength(word);
                AddGetFirstLetter(word);
                AddQwerty5Word(word);
            }

            onLoadedCallback?.Invoke();
        }

        public async Task LoadAsync()
        {
            await Task.Run(() => LoadSync(null));
        }


        private void AddCountWordLength(string word)
        {
            int len = word.Length;
            if (!_mapOfWordByLength.ContainsKey(len))
            {
                _mapOfWordByLength[len] = new List<string>();
            }

            _mapOfWordByLength[len].Add(word);
        }

        private void AddGetFirstLetter(string word)
        {
            char firstLetter = word.ToLower()[0];
            if (!_mapOfWordByFirstLetter.ContainsKey(firstLetter))
            {
                _mapOfWordByFirstLetter[firstLetter] = new List<string>();
            }

            _mapOfWordByFirstLetter[firstLetter].Add(word);
        }

        private void AddQwerty5Word(string word)
        {
            char firstLetter = word.ToLower()[0];
            if (!QwertyChars.Contains(firstLetter) || word.Length < 3 || word.Length > 5) return;
            _qwerty5Words.Add(word);
        }

        public string GetRandomWordByLength(int len)
        {
            var list = _mapOfWordByLength[len];
            if (list == null) return "";
            int listLen = list.Count;
            int idx = _random.Next(0, listLen);
            return list[idx];
        }

        public string GetRandomQwertyWord()
        {
            int idx = _random.Next(0, _qwerty5Words.Count);
            return _qwerty5Words[idx];
        }

        public delegate void OnLoadedCallback();
    }
}