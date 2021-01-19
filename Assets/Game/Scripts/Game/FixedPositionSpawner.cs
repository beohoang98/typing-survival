using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Game.Scripts.Game
{
    [DisallowMultipleComponent]
    public class FixedPositionSpawner : AbstractPositionSpawner
    {
        private Random random;
        public List<Vector2> spawnPosition = new List<Vector2>();

        private void Awake()
        {
            random = new Random(DateTime.UtcNow.Millisecond);
        }
        
        public override Vector2 GetRandomPosition()
        {
            int index = random.Next(0, spawnPosition.Count);
            return spawnPosition[index];
        }
    }
}