using System;
using _01Script.Game;
using _01Script.Networking;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _01Script.Player
{
    public class Winner : NetworkBehaviour
    {
        public static Action<Winner> OnCheck;
        private void OnEnable()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }

        private void OnSceneChanged(Scene oldScene, Scene newScene)
        {
            OnCheck?.Invoke(this);
        }
        public void Lose()
        {
            GameManager.Winners.Remove(this);
            
            if (GameManager.Winners.Count <= 1)
            {
                NetworkManager.Singleton.SceneManager.LoadScene(SceneNames.EndScene, LoadSceneMode.Single);
            }
        }
    }
}