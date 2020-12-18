using System.Collections;
using UnityEngine;

namespace UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    [ExecuteInEditMode]
    public class CameraShake : MonoBehaviour
    {
        public static CameraShake Instance { get; private set; }
        private Camera _camera;
        [SerializeField] private float shakeDistance = 1f;

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            _camera = GetComponent<Camera>();
        }

        public void Shake(float duration)
        {
            StartCoroutine(ShakeAwait(duration));
        }

        IEnumerator ShakeAwait(float duration)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float x = Random.Range(-1f, 1f) * shakeDistance;
                _camera.transform.localPosition = Vector3.right * x;
                yield return null;
            }

            _camera.transform.localPosition = Vector3.zero;
        }
    }
}