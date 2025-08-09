using System;
using _01Script.Networking;
using Unity.Netcode;
using UnityEngine;

namespace _01Script.UI
{
    public class StartGameUI : NetworkBehaviour
    {
        [Header("Need")]
        [SerializeField] private GameObject startUI;

        private bool _isEnter;
        
        private void Awake()
        {
            startUI.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && other.TryGetComponent(out NetworkBehaviour netObj))
            {
                if (netObj.IsOwner) 
                {
                    startUI.SetActive(true);
                    _isEnter = true;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player") && other.TryGetComponent(out NetworkBehaviour netObj) && startUI != null)
            {
                if (netObj.IsOwner) 
                {
                    startUI.SetActive(false);
                    _isEnter = false;
                }
            }
        }

        private void Update()
        {
            if (_isEnter)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    HostSingleton.Instance.GameManager.ChangeNetworkScene(SceneNames.GameScene);
                }
            }
        }
    }
}
