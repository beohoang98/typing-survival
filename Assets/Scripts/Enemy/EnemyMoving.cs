using System;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyMoving : MonoBehaviour
    {
        private float speed = 0.8f;
        [SerializeField] private Rigidbody2D rigid;
        [SerializeField] private PlayerController playerController;
        
        private void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
            playerController = FindObjectOfType<PlayerController>();
        }

        private void Update()
        {
            rigid.velocity = speed * (PlayerController.Instance.gameObject.transform.position - gameObject.transform.position).normalized;
        }
    }
}