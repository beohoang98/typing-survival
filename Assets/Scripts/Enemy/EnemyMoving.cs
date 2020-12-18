using Player;
using UnityEngine;

namespace Enemy
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
                             (PlayerController.Instance.gameObject.transform.position - gameObject.transform.position)
                             .normalized;
        }
    }
}