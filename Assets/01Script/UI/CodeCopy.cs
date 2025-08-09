using System;
using _01Script.Networking;
using TMPro;
using UnityEngine;

namespace _01Script.UI
{
    public class CodeCopy : MonoBehaviour
    {
        [Header("Need")]
        [SerializeField] private TextMeshProUGUI code;
        private void Awake()
        {
            code.text = "참여 코드 : "+
                HostSingleton.Instance.GameManager.JoinCode;
        }

        public void CopyBtn()
        {
            GUIUtility.systemCopyBuffer = HostSingleton.Instance.GameManager.JoinCode;
        }
    }
}
