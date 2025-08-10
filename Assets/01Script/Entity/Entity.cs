using System;
using _01Script.End;
using _01Script.Player;
using Unity.Netcode;
using UnityEngine;

namespace _01Script.Entity
{
    public class Entity :  NetworkBehaviour
    {
        private void OnEnable()
        {
            WinnerCheck.OnDie += Die;
        }

        private void OnDisable()
        {
            WinnerCheck.OnDie -= Die;
        }

        private void Die()
        {
            if (IsServer)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}