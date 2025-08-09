﻿using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace _01Script.Networking
{
    public class ClientSingleton : MonoBehaviour
    {
        private static ClientSingleton _instance;

        public static ClientSingleton Instance
        {
            get
            {
                if(_instance != null) return _instance;

                _instance = FindFirstObjectByType<ClientSingleton>();
                
                return _instance;
            }
        }
        
        public ClientGameManager GameManager { get; private set; }

        public async Task<bool> CreateClientAsync()
        {
            GameManager = new ClientGameManager();

            bool result = await GameManager.InitManagerAsync();

            if (result)
            {
                NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnectCallback;
            }

            return result;
        }

        private void HandleClientConnectCallback(ulong clientID)
        {
            Debug.Log($"{clientID} is disconnected");  
        }
    }
}