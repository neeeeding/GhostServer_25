using System;
using Unity.Netcode;
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