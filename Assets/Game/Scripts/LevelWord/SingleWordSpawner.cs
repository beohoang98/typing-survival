using Random = UnityEngine.Random;

namespace Game.Scripts.LevelWord
{
    public class SingleWordSpawner : WordSpawner
    {
        public override string GetWord()
        {
            string newWord = char.ConvertFromUtf32('a' + Random.Range(0, 26));
            return newWord;
        }
    }
}