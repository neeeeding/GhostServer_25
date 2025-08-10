using System;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _01Script.Networking
{
    public class HostGameManager
    {
        private Allocation _relayAllocation;
        private const int MaxCount = 4;
        private string _joinCode;

        public string JoinCode
        {
            get { return _joinCode; }
            set { _joinCode = value; }
        }

        public async void ResetAll()
        {
            _relayAllocation = await RelayService.Instance.CreateAllocationAsync(MaxCount);
            _joinCode = await RelayService.Instance.GetJoinCodeAsync(_relayAllocation.AllocationId);

        }

        public async Task<bool> StartHostAsync()
        {
            try
            {
                _relayAllocation = await RelayService.Instance.CreateAllocationAsync(MaxCount);
                _joinCode = await RelayService.Instance.GetJoinCodeAsync(_relayAllocation.AllocationId);

                UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
                transport.SetRelayServerData(_relayAllocation.ToRelayServerData("dtls"));
                
                return NetworkManager.Singleton.StartHost();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void ChangeNetworkScene(string sceneName)
            => NetworkManager.Singleton.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}