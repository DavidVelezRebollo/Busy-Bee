using UnityEngine;

namespace GOM.Components.Honey {
    public class FlowerLever : MonoBehaviour {
        private bool _isLavander = true;

        private void OnMouseDown() {
            _isLavander = !_isLavander;

            transform.localRotation = Quaternion.Euler(0f, 0f, _isLavander ? -10f : 10f);
        }

        public bool OnLavander() { return _isLavander; }
    }
}
