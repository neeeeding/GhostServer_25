using System;
using _01Script.Game;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace _01Script.Player
{
    public class PlayerMovement : NetworkBehaviour
    {
        [Header("Setting")]
        [SerializeField] private float moveSpeed = 4f; //속도
        
        [Header("Need")]
        [SerializeField] private PlayerInputSO inputSo;

        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            RandomPosition.OnPos += RandomPos;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            RandomPosition.OnPos -= RandomPos;
        }

        private void RandomPos(RandomPosition obj)
        {
            obj.SetPosition(gameObject);
        }

        private void FixedUpdate()
        {
            if(!IsOwner) return;
            Movement();
        }

        private void Movement()
        {
            rb.linearVelocity =inputSo.MovementKey * moveSpeed;
        }
    }
}