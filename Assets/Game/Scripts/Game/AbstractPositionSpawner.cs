using UnityEngine;

namespace Game.Scripts.Game
{
    public abstract class AbstractPositionSpawner : MonoBehaviour
    {
        public virtual Vector2 GetRandomPosition()
        {
            return Vector2.zero;
        }
    }
}