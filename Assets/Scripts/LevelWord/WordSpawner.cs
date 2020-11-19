using UnityEngine;

namespace LevelWord
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