using System;

namespace Game.Scripts.Saves
{
    [Serializable]
    public class LevelScore
    {
        public uint level = 1;
        public uint score = 0;
        public uint waves = 0;
    }
}