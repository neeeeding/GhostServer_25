using System;
using System.Collections.Generic;
using _01Script.Player;
using Unity.Netcode;
using UnityEngine;

namespace _01Script.Game
{
    public class GameManager : NetworkBehaviour
    {
        public static List<Winner> Winners;
        private void Start()
        {
            Winners = new List<Winner>();
            if (!IsServer) return;
            PlayersCount();
        }
        private void PlayersCount()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject obj in objs)
            {
                Winner w = obj.GetComponent<Winner>();
                Winners.Remove(w);
                Winners.Add(w);
            }
            Winners.Remove(null);

        }
    }
}