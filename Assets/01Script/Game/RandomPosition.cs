using System;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _01Script.Game
{
    public class RandomPosition : NetworkBehaviour
    {
        [Header("Need")]
        [SerializeField] private GameObject map;

        private Vector2 _mapCenter;
        private Vector2 _mapSize;

        private void Awake()
        {
            _mapCenter = map.transform.position;
            _mapSize = map.transform.localScale/2;
        }

        public void SetPosition(GameObject obj)
        {   
            float x = Random.Range(-_mapSize.x, _mapSize.x);
            x += _mapCenter.x;
            float y = Random.Range(-_mapSize.y, _mapSize.y);
            y += _mapCenter.y;
            Vector2 pos = new Vector2(x, y);
            
            obj.transform.localPosition = pos;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                SetPosition(other.gameObject);
            }
        }
    }
}
