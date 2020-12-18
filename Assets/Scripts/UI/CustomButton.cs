using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [DisallowMultipleComponent]
    public class CustomButton : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private Image cursor;
        [SerializeField] private AudioClip hoverSound;

        public void OnPointerEnter(PointerEventData eventData)
        {
            cursor.transform.position = transform.position;
            AudioSource.PlayClipAtPoint(hoverSound, Camera.main.transform.position);
        }
    }
}