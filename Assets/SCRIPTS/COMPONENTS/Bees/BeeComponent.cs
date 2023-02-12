using GOM.Classes.Bees;
using UnityEngine;
using UnityEngine.UI;

namespace GOM.Components.Bees {
    public class BeeComponent : MonoBehaviour {
        public Bee BeeType;

        private Camera _mainCamera;

        private void Start() {
            _mainCamera = Camera.main;   
        }

        private void OnMouseDrag() {
            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            transform.position = new Vector3(mousePosition.x, mousePosition.y);
        }

        private void OnMouseUp() {
            if (!Physics2D.Raycast(transform.position, Vector3.down, 1f)) return;

            Debug.Log("Nose");
        }
    }
}