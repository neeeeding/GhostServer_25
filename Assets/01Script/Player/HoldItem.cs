using System;
using _01Script.Entity;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

namespace _01Script.Player
{
    public class HoldItem : NetworkBehaviour
    {
        public NetworkVariable<ulong> weapon = new NetworkVariable<ulong>(0);
        public NetworkVariable<ulong> shield = new NetworkVariable<ulong>(0);

        public Item weaponItem; 
        public Item shieldItem; 

        public void Set(ItemType type,Item item = null)
        {
            switch (type)
            {
                case ItemType.Weapon:
                    weapon.Value = item?.NetworkObjectId??0;
                    weaponItem = item;
                    break;
                case ItemType.Shield:
                    shield.Value = item?.NetworkObjectId??0;
                    shieldItem = item;
                    break;
            }
        }
    }
}
