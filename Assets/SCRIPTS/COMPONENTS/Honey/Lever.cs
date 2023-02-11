using UnityEngine;

namespace GOM.Components.Honey {
    public class Lever : MonoBehaviour {
        [Tooltip("First path which the lever will enable or disable")]
        [SerializeField] private GameObject[] FirstPath;
        [Tooltip("Second path which the lever will enable or disable")]
        [SerializeField] private GameObject[] SecondPath;

        private void OnMouseDown() {
            foreach (GameObject go in FirstPath)
                go.SetActive(!go.activeSelf);

            foreach (GameObject go in SecondPath)
                go.SetActive(!go.activeSelf);
        }

    }
}
