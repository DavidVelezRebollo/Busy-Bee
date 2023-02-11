using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOM.Components.Menu
{
    public class Credits : MonoBehaviour
    {
        private float _loweringSpeed = 200f;
        private RectTransform _creditsTransform;

        private void Start()
        {
            _creditsTransform = GetComponent<RectTransform>();
            ResetPosition();
        }

        private void Update()
        {
            if(_creditsTransform.anchoredPosition.y<2430) _creditsTransform.anchoredPosition += Vector2.up * _loweringSpeed * Time.deltaTime;
        }

        public void ResetPosition()
        {
            if(_creditsTransform!=null) _creditsTransform.anchoredPosition = new Vector2(_creditsTransform.anchoredPosition.x, -880);
        }
    }
}
