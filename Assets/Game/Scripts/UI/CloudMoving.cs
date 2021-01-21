using UnityEngine;
using Random = System.Random;

namespace Game.Scripts.UI
{
    [DisallowMultipleComponent]
    public class CloudMoving : MonoBehaviour
    {
        private static readonly Random _random = new Random();
        private float _delay;
        private Vector3 _originalPos;

        private void Start()
        {
            _delay = _random.Next(0, 100) / 25f; // 4s
            _originalPos = transform.localPosition;
        }

        private void Update()
        {
            float _speed = _originalPos.z + 1;
            transform.localPosition = (Vector2) _originalPos +
                                      Mathf.Cos((Time.time + _delay) * _speed / 5f) * 100f * Vector2.right;
        }
    }
}