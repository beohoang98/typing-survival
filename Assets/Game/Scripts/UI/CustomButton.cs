using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    [DisallowMultipleComponent]
    public class CustomButton : MonoBehaviour, IPointerEnterHandler, ISelectHandler
    {
        [SerializeField] private Image cursor;

        private void HandleSelect(BaseEventData data)
        {
            cursor.transform.DOMove(transform.position, 0.2f)
                .SetEase(Ease.OutCubic);
            cursor.GetComponent<AudioSource>().Play();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        public void OnSelect(BaseEventData eventData)
        {
            HandleSelect(eventData);
        }
    }
}