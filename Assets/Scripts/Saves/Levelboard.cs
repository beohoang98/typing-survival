using System;
using System.Collections.Generic;

namespace Saves
{
    [Serializable]
    public class Levelboard
    {
        public static readonly string Endpoint = Environment.GetEnvironmentVariable("BACKEND_URL") + "/level-board";
        
        public int Level { get; set; }
        public List<UserScore> Scores { get; }

        public static Dictionary<int, Levelboard> Load()
        {
            Dictionary<int, Levelboard> map = new Dictionary<int, Levelboard>();
            return map;
        }
    }
}