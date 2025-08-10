using System;
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
        [Header("Need")]
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected CircleCollider2D circleCollider;

        private ItemSpawner _spawner;
        
        public NetworkVariable<bool> isActive = new NetworkVariable<bool>();

        private void Awake()
        {
            SetVisible(true);
        }

        public void SetSpawner(ItemSpawner pa)
        {
            _spawner = pa;
        }

        public override void OnNetworkSpawn()
        {
            if (IsClient)
                SetVisible(isActive.Value);
        }

        public void SetVisible(bool isActive)
        {
            circleCollider.enabled = isActive;
            spriteRenderer.enabled = isActive;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out HoldItem hold))
            {
                if (IsServer)
                {
                    Hold(hold);   
                }
                SetVisible(false);
            }
        }

        private void Hold(HoldItem hold)
        {
            hold.Set(item, this);
        }

        public void ReSpawn()
        {
            SetVisible(true);
            if(!IsServer) return;
            _spawner.Spawn(item);
        }
    }

    public enum ItemType
    {
        None,
        Weapon,
        Shield
    }
}