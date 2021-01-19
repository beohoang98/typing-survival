using UnityEngine;

namespace Game.Scripts.Enemy
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ParticleSystem))]
    public class SpawnUndergroundEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem mParticleSystem;

        private void Start()
        {
            mParticleSystem.Play(true);
        }

        private void Reset()
        {
            mParticleSystem = GetComponent<ParticleSystem>();
        }

        private void OnParticleSystemStopped()
        {
            Destroy(gameObject);
        }
    }
}