using Game.Scripts.Player;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    [DisallowMultipleComponent,
     RequireComponent(typeof(Rigidbody2D)),
     RequireComponent(typeof(SpriteRenderer))]
    public class EnemyFlying : MonoBehaviour
    {
        [SerializeField] private float speed = 0.5f;
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _renderer;
        private float _flyDownTime;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _renderer = GetComponent<SpriteRenderer>();

            Vector2 dir = PlayerController.instance.transform.position - transform.position;
            _rigidbody2D.velocity = speed * dir.normalized;
            _flyDownTime = 0f;
        }

        private void Update()
        {
            _flyDownTime += Time.deltaTime;
            _renderer.flipX = _rigidbody2D.velocity.x > 0;
        }

        [UsedImplicitly]
        public void OnFlyUp()
        {
            Debug.Log("Fly up", this);
            Vector2 dir = PlayerController.instance.transform.position - transform.position;
            _rigidbody2D.velocity = speed * dir.normalized -
                                    (_flyDownTime * _rigidbody2D.gravityScale * Physics2D.gravity / 2f);
            _flyDownTime = 0f;
        }
    }
}