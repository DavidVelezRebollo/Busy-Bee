using UnityEngine;

namespace GOM.Components.Workplaces {
    public class Bottling : Workplace
    {
        public override void TransformPolen()
        {
            Debug.Log("Bottled");
        }
    }
}
