using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOM
{
    public class StartAnimation : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            LeanTween.moveY(gameObject, -1000, 0).setOnComplete(OnComplete);
        }

        private void OnComplete()
        {
            LeanTween.moveY(gameObject, 540, 1).setEase(LeanTweenType.easeOutElastic);
        }
    }
}
