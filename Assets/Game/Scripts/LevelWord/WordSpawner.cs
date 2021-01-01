using UnityEngine;

namespace Game.Scripts.LevelWord
{
    public abstract class WordSpawner : MonoBehaviour
    {
        public virtual void Load()
        {
        }

        public virtual string GetWord()
        {
            return "A";
        }
    }
}