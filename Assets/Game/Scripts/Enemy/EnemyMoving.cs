﻿using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class EnemyMoving : MonoBehaviour
    {
        [SerializeField] private float speed = 0.8f;
        [SerializeField] private Rigidbody2D rigid;

        private void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            rigid.velocity = speed *
                             (PlayerController.instance.gameObject.transform.position - gameObject.transform.position)
                             .normalized;
        }
    }
}