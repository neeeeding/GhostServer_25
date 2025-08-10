using _01Script.Entity;
using Unity.Netcode;
using UnityEngine;

namespace _01Script.Game
{
    public class ItemSpawner : NetworkBehaviour
    {
        [Header("Need")]
        [SerializeField] private GameObject weaponPrefabs;
        [SerializeField] private GameObject shieldPrefabs;
        [SerializeField] private RandomPosition map;

        private int _maxCount;

        private void Start()
        {
            if (!IsServer) return;

            _maxCount = NetworkManager.Singleton.ConnectedClients.Count;
            
            StartSpawn();
        }

        private void StartSpawn()
        {
            for (int i = 0; i < _maxCount; i++)
            {
                
               GameObject weapon = Instantiate(weaponPrefabs);
               GameObject shield = Instantiate(shieldPrefabs);
            
                weapon.GetComponent<Item>().SetSpawner(this);
                shield.GetComponent<Item>().SetSpawner(this);

                Spawn(weapon);
                Spawn(shield);

                weapon.GetComponent<Item>().NetworkObject.Spawn();
                shield.GetComponent<Item>().NetworkObject.Spawn();
            }
        }

        private void Spawn(GameObject item)
        {
            item.gameObject.SetActive(true);
            if(IsServer)
                map.SetPosition(item.gameObject);
        }
    }
}