using UnityEngine;

namespace GOM.Components.Honey {
    public class FlowerLever : MonoBehaviour {
        [SerializeField] private GameObject ParticlePrefab;
        private bool _isLavander = true;

        private void OnMouseDown() {
            _isLavander = !_isLavander;

            transform.localRotation = Quaternion.Euler(0f, 0f, _isLavander ? -10f : 10f);
            GameObject particles = Instantiate(ParticlePrefab, transform.position - new Vector3(0f, -0.6f), Quaternion.Euler(-90f, 0f, 0f));
            Destroy(particles, 1f);
        }

        public bool OnLavander() { return _isLavander; }
    }
}
