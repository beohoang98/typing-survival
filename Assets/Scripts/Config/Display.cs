using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    public class Display
    {
        public static readonly Dictionary<string, Resolution> of = new Dictionary<string, Resolution>()
        {
            {"1920x1080", new Resolution() { width = 1920, height = 1080, refreshRate = 60 } },
            {"1280x720", new Resolution() { width = 1280, height = 720, refreshRate = 60 } },
            {"800x600", new Resolution() { width = 800, height = 600, refreshRate = 60 } },
        };
    }
}