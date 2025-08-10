using System;
using _01Script.Entity;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

namespace _01Script.Player
{
    public class HoldItem : NetworkBehaviour
    {
        [Header("Need")]
        [SerializeField] private GameObject weaponObj;
        [SerializeField] private GameObject shieldObj;
        
        public NetworkVariable<ulong> weapon = new NetworkVariable<ulong>(0);
        public NetworkVariable<ulong> shield = new NetworkVariable<ulong>(0);
        
        private NetworkVariable<bool> _weaponIsActive = new NetworkVariable<bool>(true, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
        private NetworkVariable<bool> _shieldIsActive = new NetworkVariable<bool>(true, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);


        private void Awake()
        {
            Set(ItemType.Weapon);
            Set(ItemType.Shield);
            
            _weaponIsActive.OnValueChanged += (oldVal, b) =>
            {
                weaponObj.SetActive(b);
            };
            _shieldIsActive.OnValueChanged += (oldVal, b) =>
            {
                shieldObj.SetActive(b);
            };
        }

        public void Set(ItemType type,Item item = null)
        {
            if(!IsServer) return;
            switch (type)
            {
                case ItemType.Weapon:
                    _weaponIsActive.Value = item == null; 
                    _weaponIsActive.Value = item != null; 
                    weapon.Value = item?.NetworkObjectId??0;
                    break;
                case ItemType.Shield:
                    _shieldIsActive.Value = item == null;
                    _shieldIsActive.Value = item != null;
                    shield.Value = item?.NetworkObjectId??0;
                    break;
            }
        }
    }
}
