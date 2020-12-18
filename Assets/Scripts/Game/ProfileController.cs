using Facebook.Unity;
using UnityEngine;

namespace Game
{
    [DisallowMultipleComponent]
    public class ProfileController : MonoBehaviour
    {
        private void Awake()
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                FB.Init(HandleLoaded, OnHideUnity);
            }
        }

        private void HandleLoaded()
        {
            FB.ActivateApp();
        }

        public void Login()
        {
            string[] perms = {"profile", "basic"};
            FB.LogInWithReadPermissions(perms, HandleLoginResult);
        }

        private void HandleLoginResult(ILoginResult result)
        {
            if (result.Cancelled) return;
            Debug.Log(result.AccessToken.ToJson());
        }

        public void OnHideUnity(bool isGameShown)
        {
            Time.timeScale = isGameShown ? 1 : 0;
        }
    }
}