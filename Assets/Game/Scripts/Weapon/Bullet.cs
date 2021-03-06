﻿using System.Collections;
using Game.Scripts.Config;
using UnityEngine;

namespace Game.Scripts.Weapon
{
    public class Bullet : MonoBehaviour
    {
        private readonly float _speed = 10f;
        private Rigidbody2D _rigid;

        private void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
            // StartCoroutine(DestroyedAfter(2));
            Destroy(gameObject, 2f);
        }

        private void Update()
        {
            _rigid.velocity = (Vector2) transform.up * _speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameTag.Enemy)) Explosion();
        }

        private void Explosion()
        {
            // temp
            Destroy(gameObject);
        }

        private IEnumerator DestroyedAfter(float secs)
        {
            yield return new WaitForSecondsRealtime(secs);
            if (gameObject && gameObject.activeSelf) Destroy(gameObject);
            yield return null;
        }
    }
}