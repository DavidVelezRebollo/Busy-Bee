using System;
using GOM.Classes.UI;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GOM.Components.Honey {
    public class HoneyGenerator : MonoBehaviour {

        [Tooltip("Prefabs of the honey that will be generated")]
        [SerializeField] private GameObject[] FlowerPrefabs;
        [SerializeField] private TextMeshProUGUI NextFlowerTime;

        public static Action<int> OnFlowerGeneration;

        private static Timer _timer; // Timer that counts the time between flowers
        private bool _firstGeneration = true; // Checks if is the first time generating a flower

        private void Start() {
            _timer = new Timer(0, 3);
        }

        private void Update() {
            _timer.UpdateTimer(false);
            NextFlowerTime.text = $"{_timer.GetSecondCount():00}";

            if (_timer.GetMinuteCount() > 0 || _timer.GetSecondCount() > 0) return;

            if (_firstGeneration) {
                _timer.SetInitialSeconds(30);
                _firstGeneration = false;
            }

            _timer.RestartTimer();
            generateFlower();
        }

        private void generateFlower() {
            int nextFlower = 0;

            nextFlower = Random.Range(0f, 1f) < 0.5 ? 0 : 1;
            
            Instantiate(FlowerPrefabs[nextFlower], transform.position, Quaternion.identity);
            OnFlowerGeneration?.Invoke(nextFlower);
        }
    }
}
