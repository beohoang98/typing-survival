using Game.Scripts.Config;
using UnityEngine;

namespace Game.Scripts.LevelWord
{
    public class Qwerty5Spawner : WordSpawner
    {
        [SerializeField] private DataFile dataFile;

        public override void Load()
        {
            dataFile.LoadSync(null);
        }

        public override string GetWord()
        {
            return dataFile.GetRandomQwertyWord();
        }
    }
}