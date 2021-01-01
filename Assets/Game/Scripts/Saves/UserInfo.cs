using System;

namespace Game.Scripts.Saves
{
    [Serializable]
    public class UserInfo
    {
        public string FacebookId { get; set; }
        public string Username { get; set; }
        public string AvatarUrl { get; set; }
    }
}