using UnityEngine;

namespace GOM.Components.Honey {
    public class HoneyPath : MonoBehaviour {
        [Tooltip("Waypoints which the honey will follow")]
        private static HoneyWaypoint[] _pathWaypoints;

        private void Start() {
            _pathWaypoints = GetComponentsInChildren<HoneyWaypoint>(true);
        }

        public static HoneyWaypoint GetWaypoint(int index) { return _pathWaypoints[index]; }

        public static bool WaypointEnabled(int index) { return _pathWaypoints[index].gameObject.activeInHierarchy; }

        public static int WaypointCount() { return _pathWaypoints.Length - 1; }

    }
}
