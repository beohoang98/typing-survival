using System;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyMoving : MonoBehaviour
    {
        private float speed = 0.8f;
        private Rigidbody2D rigid;
        
        private void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            rigid.velocity = speed * (PlayerController.instance.gameObject.transform.position - gameObject.transform.position).normalized;
        }
    }
}