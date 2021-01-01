using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Config
{
    public static class Display
    {
        public static class AvailableResolution
        {
            public const string _1080p = "1920x1080";
            public const string _720p = "1280x720";
            public const string _600p = "800x600";
        }

        public static readonly Dictionary<string, Resolution> of = new Dictionary<string, Resolution>()
        {
            {AvailableResolution._1080p, new Resolution() {width = 1920, height = 1080, refreshRate = 60}},
            {AvailableResolution._720p, new Resolution() {width = 1280, height = 720, refreshRate = 60}},
            {AvailableResolution._600p, new Resolution() {width = 800, height = 600, refreshRate = 60}},
        };
    }
}