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

        private Item _weapon;
        private Item _shield;

        private void Start()
        {
            if (!IsServer) return;

            _weapon = Instantiate(weaponPrefabs).GetComponent<Item>();
            _shield = Instantiate(shieldPrefabs).GetComponent<Item>();
            
            _weapon.SetSpawner(this);
            _shield.SetSpawner(this);
            
            _weapon.SetVisible(true);
            _shield.SetVisible(true);

            Spawn(ItemType.Weapon);
            Spawn(ItemType.Shield);

            _weapon.NetworkObject.Spawn();
            _shield.NetworkObject.Spawn();

        }

        public void Spawn(ItemType item)
        {
            switch (item)
            {
                case ItemType.Weapon:
                    _weapon.gameObject.SetActive(true);
                    if(IsServer)
                        map.SetPosition(_weapon.gameObject);
                    break;
                case ItemType.Shield:
                    _shield.gameObject.SetActive(true);
                    if(IsServer)
                        map.SetPosition(_shield.gameObject);
                    break;
            }
        }
    }
}