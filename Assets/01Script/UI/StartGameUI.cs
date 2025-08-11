using System;
using _01Script.Networking;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _01Script.UI
{
    public class StartGameUI : NetworkBehaviour
    {
        [Header("Need")]
        [SerializeField] private TextMeshProUGUI startUI;

        private bool _isEnter;
        
        private void Awake()
        {
            startUI.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && other.TryGetComponent(out NetworkBehaviour netObj))
            {
                if (netObj.IsOwner) 
                {
                    startUI.text = netObj.IsServer ? "F를 눌러 시작하기" : "방장만 시작할 수 있습니다.";
                    startUI.gameObject.SetActive(true);
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
                    startUI.gameObject.SetActive(false);
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
                    NetworkManager.Singleton.SceneManager.LoadScene(SceneNames.GameScene, LoadSceneMode.Single);
                }
            }
        }
    }
}
