using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOM.Components.Workplaces
{
    internal class Mix : Workplace
    {
        private string name;
        private string description;
        private int honeyProduction;

        private Sprite sprite;

        public override void Work()
        {
            Debug.Log("Mix");
        }
    }
}
