using UnityEngine;

namespace GOM.Components.Honey {
    public class HoneyWaypoint : MonoBehaviour {
        [SerializeField] private bool NeedStop;
        //[SerializeField] private Workplace Workplace;

        public bool NeedsStop() { return NeedStop; }
    }
}
