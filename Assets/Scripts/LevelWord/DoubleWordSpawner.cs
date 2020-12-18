﻿using System.Collections;
using Config;
using UnityEngine;

namespace LevelWord
{
    public class DoubleWordSpawner : WordSpawner
    {
        [SerializeField] private DataFile dataFile;

        public override void Load()
        {
            dataFile.LoadSync(null);
        }

        public override string GetWord()
        {
            return dataFile.GetRandomWordByLength(2);
        }
    }
}