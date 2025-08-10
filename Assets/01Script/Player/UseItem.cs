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
            if (other.CompareTag("Player") &&
                other.TryGetComponent(out UseItem use) && other.gameObject != this.gameObject)
            {
                if (_holdItem.weapon.Value != 0)
                {
                    use.Shielded();
                    
                    if(!IsServer) return;
                    _holdItem.Set(ItemType.Weapon);
                }
            }
        }

        public bool Shielded() //방패 가지고 있는지
        {
            if (_holdItem.shield.Value != 0)
            {
                if(!IsServer) return true;
                _holdItem.Set(ItemType.Shield);
                return true;
            }
            if (IsServer)
            {
                GetComponent<Winner>().Lose();
                
                var netObj = GetComponent<NetworkObject>();
                if (netObj != null)
                    netObj.Despawn();
                else
                    Destroy(gameObject);
            }
            else
            {
                //gameObject.SetActive(false);
            }
            return false;
        }
    }
}
