using _01Script.Game;
using _01Script.Player;
using Unity.Netcode;
using UnityEngine;

namespace _01Script.Entity
{
    public class Item : NetworkBehaviour
    {
        [Header("Setting")]
        [SerializeField] private ItemType item;

        private ItemSpawner _spawner;

        public void SetSpawner(ItemSpawner pa)
        {
            _spawner = pa;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out HoldItem hold))
            {
                if (IsServer)
                {
                    Hold(hold); 
                    var netObj = GetComponent<NetworkObject>();
                    if (netObj != null)
                        netObj.Despawn();
                    else
                        Destroy(gameObject);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }

        private void Hold(HoldItem hold)
        {
            hold.Set(item, this);
        }
    }

    public enum ItemType
    {
        None,
        Weapon,
        Shield
    }
}