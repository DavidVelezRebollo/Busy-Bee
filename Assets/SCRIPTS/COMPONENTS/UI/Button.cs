using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GOM.Components.Buttons
{
    public class Button : MonoBehaviour, IPointerEnterHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            LeanTween.scale(gameObject, new Vector3(1.1f, 1.1f, 1.1f), 0.1f).setEase(LeanTweenType.easeOutQuad).setOnComplete(OnRelease);
        }

        public void OnPress()
        {
            LeanTween.scale(gameObject, new Vector3(0.9f, 0.9f, 0.9f), 0.1f).setEase(LeanTweenType.easeOutQuad).setOnComplete(OnRelease);
        }

        public void OnRelease()
        {
            LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.1f).setEase(LeanTweenType.easeOutQuad);
        }
    }
}
