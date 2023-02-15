using UnityEngine;

namespace GOM.Components.Honey {
    public class Lever : MonoBehaviour {
        [Tooltip("The Waypoint attached to the lever")]
        [SerializeField] private HoneyWaypoint LeverWaypoint;
        [Tooltip("First path which the lever will enable or disable")]
        [SerializeField] private HoneyWaypoint FirstPath;
        [Tooltip("Second path which the lever will enable or disable")]
        [SerializeField] private HoneyWaypoint SecondPath;
        [Tooltip("Particle Prefab")]
        [SerializeField] private GameObject ParticlePrefab;

        private bool _firstPath = true;

        private void OnMouseDown() {
            if (FirstPath.GetComponent<HoneyWaypoint>().FlowerComing() ||
                SecondPath.GetComponent<HoneyWaypoint>().FlowerComing()) return;

            _firstPath = !_firstPath;

            LeverWaypoint.SetNextWaypoint(_firstPath ? FirstPath : SecondPath);

            transform.localRotation = Quaternion.Euler(0f, 0f, _firstPath ? -25f : 25f);
            GameObject particles = Instantiate(ParticlePrefab, transform.position - new Vector3(0f, 0.6f), Quaternion.Euler(90f, 0f, 0f));
            Destroy(particles, 1f);
        }

    }
}
