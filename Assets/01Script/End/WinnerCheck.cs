using System;
using _01Script.Networking;
using _01Script.Player;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace _01Script.End
{
    public class WinnerCheck : NetworkBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        
        private Winner _winner;
        
        public static Action OnDie;

        private void OnEnable()
        {
            Winner.OnCheck += Check;
            
            if(text == null) return;
            if (NetworkManager.Singleton.ConnectedClients.Count <2)
            {
                text.text = "혼자서 하셨군요.";
            }
        }

        private void OnDisable()
        {
            Winner.OnCheck -= Check;
        }

        // private void Start()
        // {
        //     DontDestroyOnLoad(gameObject);
        // }

        public void MoreGame()
        {
            More();
        }

        private async void More()
        {
            if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsClient)
            {
                NetworkManager.Singleton.Shutdown();
            }

            HostSingleton.Instance.GameManager.ResetAll();
            
            ApplicationController app = FindObjectOfType<ApplicationController>();
            app.ReLaunchInMode();
            
            bool result = await HostSingleton.Instance.GameManager.StartHostAsync();
            if (result)
            {
                HostSingleton.Instance.GameManager.ChangeNetworkScene(SceneNames.WaitScene);
            }
        }

        public void Main()
        {
            if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsClient)
            {
                NetworkManager.Singleton.Shutdown();
            }

            HostSingleton.Instance.GameManager.ResetAll();
            ApplicationController app = FindObjectOfType<ApplicationController>();
            app.ReLaunchInMode();
            HostSingleton.Instance.GameManager.ChangeNetworkScene(SceneNames.MenuScene);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
        
        private void Check(Winner winner)
        {
            if(text == null) return;
            
            _winner = winner;
            
            if (!winner.IsOwner)
            {
                text.text = "졌습니다.";
            }
            else
            {
                text.text = "이겼습니다.";
            }
            if(!IsServer) return;
            OnDie?.Invoke();
        }
    }
}