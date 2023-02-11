using UnityEngine;

namespace GOM.Components.Workplaces {
    public abstract class Workplace : MonoBehaviour {
        protected string Name;
        protected string Description;
        protected int HoneyProduction;

        protected Sprite Sprite;

        public abstract void Work();
    }
}
