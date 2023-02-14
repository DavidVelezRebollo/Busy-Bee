using UnityEngine;

namespace GOM.Components.Honey {
    public class Lever : MonoBehaviour {
        [Tooltip("First path which the lever will enable or disable")]
        [SerializeField] private GameObject FirstPath;
        [Tooltip("Second path which the lever will enable or disable")]
        [SerializeField] private GameObject SecondPath;

        private void OnMouseDown() {
            if (FirstPath.GetComponent<HoneyWaypoint>().FlowerComing() ||
                SecondPath.GetComponent<HoneyWaypoint>().FlowerComing()) return;

            FirstPath.SetActive(!FirstPath.activeInHierarchy);
            SecondPath.SetActive(!SecondPath.activeInHierarchy);

            transform.localPosition = new Vector3(FirstPath.activeInHierarchy ? -0.35f : 0.35f, 0f, 0f);
        }

    }
}
