using _01Script.Game;
using _01Script.Player;
using Unity.Netcode;
using UnityEngine;
namespace _01Script.Entity
{
    public class Enemy :NetworkBehaviour
    {
        private EnemySpawner _spawner;

        public void SetSpawner(EnemySpawner pa)
        {
            _spawner = pa;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") &&
                other.TryGetComponent(out UseItem use) && other.gameObject != this.gameObject)
            {
                use.Shielded();
            }
        }
    }
}