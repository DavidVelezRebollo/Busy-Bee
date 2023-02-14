using GOM.Components.Workplaces;
using UnityEngine;

namespace GOM.Components.Honey {
    public class HoneyWaypoint : MonoBehaviour {
        [SerializeField] private Workplace Workplace;
        [SerializeField] private HoneyWaypoint NextWaypoint;

        private bool _flowerComing;

        public bool IsWorkplace() { return Workplace != null; }
        public bool FlowerComing() { return _flowerComing; }
        public HoneyWaypoint GetNextWaypoint() { return NextWaypoint; }
        public void SetNextWaypoint(HoneyWaypoint waypoint) { NextWaypoint = waypoint; }
        public void SetFlowerComing(bool isComing) { _flowerComing = isComing; }
    }
}
