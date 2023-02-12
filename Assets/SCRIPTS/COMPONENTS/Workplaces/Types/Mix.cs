using UnityEngine;

namespace GOM.Components.Workplaces {
    public class Mix : Workplace
    {
        public override void TransformPolen()
        {
            Debug.Log("Mixed");
            CurrentFlower.ChangeSprite(NewHoneySprite);
        }
    }
}
