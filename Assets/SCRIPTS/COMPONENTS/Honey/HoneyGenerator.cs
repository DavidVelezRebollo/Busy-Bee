using GOM.Classes.UI;
using UnityEngine;

namespace GOM.Components.Honey {
    public class HoneyGenerator : MonoBehaviour {

        [Tooltip("Prefabs of the honey that will be generated")]
        [SerializeField] private GameObject[] FlowerPrefabs;

        private Timer _timer; // Timer that counts the time between flowers
        private bool _firstGeneration = true; // Checks if is the first time generating a flower

        private void Start() {
            _timer = new Timer(0, 3);
        }

        private void Update() {
            _timer.UpdateTimer(false);

            if (_timer.GetMinuteCount() > 0 || _timer.GetSecondCount() > 0) return;

            if (_firstGeneration) {
                _timer.SetInitialSeconds(30);
                _firstGeneration = false;
            }

            _timer.RestartTimer();
            Instantiate(FlowerPrefabs[0], transform.position, Quaternion.identity);
        }
    }
}
