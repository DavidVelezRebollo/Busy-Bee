using GOM.Classes.Bees;
using UnityEngine;
using UnityEngine.UI;

namespace GOM.Components.Bees {
    public class BeeDisplay : MonoBehaviour {
        public Bee beeType;

        public Text nameText;
        public Text descriptionText;

        public Image beeSprite;

        public Text workSpeedText;
        public Text effectiveWorkSpeedText;

        void Start() {
            nameText.text = beeType.Name;
            descriptionText.text = beeType.Description;
            beeSprite.sprite = beeType.BeeSprite;

            workSpeedText.text = beeType.WorkSpeed.ToString();
            effectiveWorkSpeedText.text = beeType.EffectiveWorkSpeed.ToString();
        }
    }
}