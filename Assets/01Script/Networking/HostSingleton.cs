using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace _01Script.Networking
{
    public class HostSingleton : MonoBehaviour
    {
        private static HostSingleton _instance;

        public static HostSingleton Instance
        {
            get
            {
                if(_instance != null) return _instance;

                _instance = FindFirstObjectByType<HostSingleton>();
                
                return _instance;
            }
        }
        
        public HostGameManager GameManager { get; private set; }

        public void CreateHost()
        {
            GameManager = new HostGameManager();
        }
    }
}