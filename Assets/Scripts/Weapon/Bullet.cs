using System;
using UnityEngine;

namespace Weapon
{
    public class Bullet : MonoBehaviour
    {
        private readonly float _speed = 10f;
        private Rigidbody2D _rigid;
        [SerializeField] private GameObject partialSys;

        private void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _rigid.velocity = (Vector2)transform.up * _speed;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("enemy"))
            {
                Explosion();
            }
        }

        void Explosion()
        {
            var emit = partialSys.GetComponent<ParticleSystem>();
            emit.transform.parent = null;
            var mainModule = emit.main;
            mainModule.loop = false;

            // temp
            Destroy(gameObject);
        }
    }
}