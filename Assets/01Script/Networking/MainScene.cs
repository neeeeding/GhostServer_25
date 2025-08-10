using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _01Script.Networking
{
    public class MainScene : MonoBehaviour
    {
        [SerializeField] private Button hostBtn;
        [SerializeField] private Button joinBtn;
        [SerializeField] private TMP_InputField codeInput;

        private void Awake()
        {
            hostBtn.onClick.AddListener(HandleRelayHostClick);
            joinBtn.onClick.AddListener(HandleJoinClick);
        }
        
        private async void HandleRelayHostClick()
        {
            bool result = await HostSingleton.Instance.GameManager.StartHostAsync();
            if (result)
            {
                HostSingleton.Instance.GameManager.ChangeNetworkScene(SceneNames.WaitScene);
            }
            else
            {
                Debug.Log("Failed to host");
            }
        }
        
        private async void HandleJoinClick()
        {
            string joinCode = codeInput.text;
            if(string.IsNullOrEmpty(joinCode)) return;

            await ClientSingleton.Instance.GameManager.StartClientWithJoinCode(joinCode);
        }
    }

    public class SceneNames
    {
        public const string MainScene = "MainScene";
        public const string MenuScene = "MenuScene";
        public const string WaitScene = "WaitScene";
        public const string GameScene = "GameScene";
        public const string EndScene = "EndScene";
    }
}
