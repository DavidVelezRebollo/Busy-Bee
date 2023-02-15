using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOM.Components.Menu
{
    public class Credits : MonoBehaviour
    {
        [SerializeField] private float LoweringSpeed;
        [SerializeField] private float InitY;
        [SerializeField] private float FinalY;
        private RectTransform _creditsTransform;

        private void Start()
        {
            _creditsTransform = GetComponent<RectTransform>();
            ResetPosition();
        }

        private void Update()
        {
            if(_creditsTransform.anchoredPosition.y<FinalY) _creditsTransform.anchoredPosition += Vector2.up * LoweringSpeed * Time.deltaTime;
        }

        public void ResetPosition()
        {
            if(_creditsTransform!=null) _creditsTransform.anchoredPosition = new Vector2(_creditsTransform.anchoredPosition.x, InitY);
        }
    }
}
