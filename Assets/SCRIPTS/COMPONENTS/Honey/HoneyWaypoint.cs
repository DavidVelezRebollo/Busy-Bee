using GOM.Components.Workplaces;
using UnityEngine;

namespace GOM.Components.Honey {
    public class HoneyWaypoint : MonoBehaviour {
        [SerializeField] private Workplace Workplace;

        private bool _flowerComing;

        public bool IsWorkplace() { return Workplace != null; }
        public bool FlowerComing() { return _flowerComing; }
        public void SetFlowerComing(bool isComing) { _flowerComing = isComing; }
    }
}
