using System;
using System.Diagnostics;
using System.Threading.Tasks;
using _01Script.Player;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _01Script.Entity
{
    public class EnemyMovement : NetworkBehaviour
    {
        [Header("Setting")]
        [SerializeField] private float moveSpeed = 4f; //속도
        
        private Rigidbody2D rb;
        private Vector3 MovementKey;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            SetPos(3);
        }

        private void FixedUpdate()
        {
            if(!IsOwner) return;
            Movement();
        }

        private async Task SetPos(int ms)
        {
            while (true)
            {
                await Task.Delay(ms *1000);
                int r = Random.Range(0, 4);
                MovementKey = r switch
                {
                    0 => Vector3.forward,
                    1 => Vector3.left,
                    2 => Vector3.back,
                    3 => Vector3.right,
                    _ => Vector2.zero
                };
            }
        }

        private void Movement()
        {
            rb.linearVelocity = MovementKey * moveSpeed;
        }
    }
}