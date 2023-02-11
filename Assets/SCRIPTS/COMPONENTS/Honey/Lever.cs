using UnityEngine;

namespace GOM.Components.Honey {
    public class Lever : MonoBehaviour {
        [Tooltip("First path which the lever will enable or disable")]
        [SerializeField] private GameObject[] FirstPath;
        [Tooltip("Second path which the lever will enable or disable")]
        [SerializeField] private GameObject[] SecondPath;

        private void OnMouseDown() {
            int i = 0;
            bool found = false;

            while(i < FirstPath.Length && !found) {
                if (FirstPath[i].GetComponent<HoneyWaypoint>().FlowerComing())
                    found = true;
                i++;
            }

            if (found) return;
            i = 0;

            while (i < SecondPath.Length && !found) {
                if (SecondPath[i].GetComponent<HoneyWaypoint>().FlowerComing())
                    found = true;
                i++;
            }

            if (found) return;

            foreach (GameObject go in FirstPath)
                go.SetActive(!go.activeSelf);

            foreach (GameObject go in SecondPath)
                go.SetActive(!go.activeSelf);
        }

    }
}
