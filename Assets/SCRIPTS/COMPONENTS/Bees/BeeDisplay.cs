using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GOM
{
    public class BeeDisplay : MonoBehaviour
    {
        public Bee beeType;

        public Text nameText;
        public Text descriptionText;

        public Image beeSprite;

        public Text workSpeedText;
        public Text effectiveWorkSpeedText;

        // Start is called before the first frame update
        void Start()
        {
            nameText.text = beeType.name;
            descriptionText.text = beeType.description;
            beeSprite.sprite = beeType.beeSprite;

            workSpeedText.text = beeType.workSpeed.ToString();
            effectiveWorkSpeedText.text = beeType.effectiveWorkSpeed.ToString();
        }
    }
}