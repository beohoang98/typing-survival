using UnityEngine;

namespace Game.Scripts.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public class LayerIndexUpdateRuntime : MonoBehaviour
    {
        [SerializeField] private float baseMaxIndex;
        private SpriteRenderer _renderer;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _renderer.sortingOrder = (int) (baseMaxIndex - transform.position.y);
        }
    }
}