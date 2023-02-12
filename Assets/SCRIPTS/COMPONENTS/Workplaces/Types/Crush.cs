using UnityEngine;

namespace GOM.Components.Workplaces {
    public class Crush : Workplace
    {
        public override void TransformPolen()
        {
            Debug.Log("Crushed");
            CurrentFlower.ChangeSprite(NewHoneySprite);
        }
    }
}
