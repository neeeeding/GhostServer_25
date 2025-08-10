using System;
using _01Script.Networking;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace _01Script.UI
{
    public class CodeCopy : MonoBehaviour
    {
        [Header("Need")]
        [SerializeField] private TextMeshProUGUI code; //코드 작성
        [SerializeField] private TextMeshProUGUI people; //인원

        private string _joinCode;
        
        private void Awake()
        {
            _joinCode= HostSingleton.Instance.GameManager.JoinCode;
            code.text = "참여 코드 : "+ _joinCode;
        }

        private void Update()
        {
            people.text = NetworkManager.Singleton.ConnectedClients.Count + "/4";
        }

        public void CopyBtn()
        {
            GUIUtility.systemCopyBuffer = _joinCode;
        }
    }
}
