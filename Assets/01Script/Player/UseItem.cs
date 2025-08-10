using System;
using _01Script.Entity;
using Unity.Netcode;
using UnityEngine;

namespace _01Script.Player
{
    public class UseItem : NetworkBehaviour
    {
        private HoldItem _holdItem;

        private void Awake()
        {
            _holdItem = GetComponent<HoldItem>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out UseItem use) && other.gameObject != this.gameObject)
            {
                if (_holdItem.weapon.Value != 0)
                {
                    use.Shielded();
                    
                    Item item = _holdItem.weaponItem;
                    if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(_holdItem.weapon.Value, out NetworkObject netObj))
                    {
                        item = netObj.GetComponent<Item>();
                    }
                    
                    item.ReSpawn();
                    if(!IsServer) return;
                    _holdItem.Set(ItemType.Weapon);
                }
            }
        }

        private bool Shielded() //방패 가지고 있는지
        {
            if (_holdItem.shield.Value != 0)
            {
                Item item = _holdItem.shieldItem;
                if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(_holdItem.shield.Value, out NetworkObject netObj))
                {
                    item = netObj.GetComponent<Item>();
                }
                
                item.ReSpawn();
                if(!IsServer) return true;
                _holdItem.Set(ItemType.Shield);
                return true;
            }
            gameObject.SetActive(false);
            return false;
        }
    }
}
