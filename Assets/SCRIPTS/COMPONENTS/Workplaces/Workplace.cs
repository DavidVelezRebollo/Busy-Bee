using UnityEngine;

namespace GOM.Components.Workplaces {
    public abstract class Workplace : MonoBehaviour {
        protected void OnTriggerEnter2D(Collider2D collision) {
            WorkHoney();
        }

        protected abstract void WorkHoney();
    }
}
