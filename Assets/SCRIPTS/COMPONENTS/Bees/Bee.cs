using GOM.Components.Workplaces;
using UnityEngine;

namespace GOM.Components.Bees {
    public abstract class Bee : MonoBehaviour {
        private Workplace _currentWorkplace;

        protected abstract void Work();
    }
}
