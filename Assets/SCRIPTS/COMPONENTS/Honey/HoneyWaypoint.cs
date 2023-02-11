using UnityEngine;

namespace GOM.Components.Honey {
    public class HoneyWaypoint : MonoBehaviour {
        [SerializeField] private bool NeedStop;
        //[SerializeField] private Workplace Workplace;

        private bool _flowerComing;

        public bool NeedsStop() { return NeedStop; }
        public bool FlowerComing() { return _flowerComing; }
        public void SetFlowerComing(bool isComing) { _flowerComing = isComing; }
    }
}
