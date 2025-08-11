using System;
using System.Threading;
using System.Threading.Tasks;
using _01Script.Entity;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

namespace _01Script.Game
{
    public class EnemySpawner : NetworkBehaviour
    {
        [Header("Setting")]
        [SerializeField] private int delay = 3;
        [Header("Need")]
        [SerializeField] private GameObject enemyPrefabs;
        [SerializeField] private RandomPosition map;
        
        private CancellationTokenSource cts;
        private void Start()
        {
            if (!IsServer) return;
            cts = new CancellationTokenSource();
            StartSpawn(cts.Token);
        }

        private async void StartSpawn(CancellationToken token)
        {
            try
            {
                while (true)
                {
                    token.ThrowIfCancellationRequested();

                    GameObject enemy = Instantiate(enemyPrefabs);
                    enemy.GetComponent<Enemy>().SetSpawner(this);

                    Spawn(enemy);
                    enemy.GetComponent<Enemy>().NetworkObject.Spawn();

                    await Task.Delay(delay * 1000, token);
                }
            }
            catch (OperationCanceledException) //씬 전환
            {
            }
        }

        private void OnDestroy()
        {
            cts?.Cancel();
        }

        private void Spawn(GameObject item)
        {
            item.gameObject.SetActive(true);
            if (IsServer)
                map.SetPosition(item.gameObject);
        }
    }
}