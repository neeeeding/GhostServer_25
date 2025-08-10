using System;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _01Script.Game
{
    public class RandomPosition : NetworkBehaviour
    {
        public static Action<RandomPosition> OnPos;
        
        [Header("Need")]
        [SerializeField] private GameObject map;

        private Vector2 _mapCenter;
        private Vector2 _mapSize;

        private void Awake()
        {
            SpriteRenderer sprite = map.GetComponent<SpriteRenderer>();
            _mapCenter = map.transform.position;
            _mapSize = sprite.bounds.size/2;
            
            OnPos?.Invoke(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            
            Gizmos.DrawWireCube(_mapCenter, _mapSize*2);
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
    }
}
