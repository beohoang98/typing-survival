using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public class EnemyMoving : MonoBehaviour
    {
        [SerializeField] private float speed = 0.8f;
        private Rigidbody2D _rigid;
        private SpriteRenderer _renderer;
        private Vector2 _direction;

        private void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _direction = (PlayerController.instance.gameObject.transform.position - transform.position).normalized;
            _rigid.velocity = (speed * _direction);
            _renderer.flipX = _direction.x < 0;
        }
    }
}