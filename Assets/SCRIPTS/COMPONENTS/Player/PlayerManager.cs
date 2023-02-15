using GOM.Components.Core;
using GOM.Components.Sounds;
using UnityEngine;

namespace GOM.Components.Player
{
    public class PlayerManager : MonoBehaviour {
        #region Singleton

        public static PlayerManager Instance;

        private void Awake() {
            if (Instance != null) return;

            Instance = this;
        }

        #endregion

        [SerializeField] private int MaxMisses;
        [SerializeField] private int HivesToComplete;

        private int[] _honeyCount;
        private int _completedHives = 0;

        private int _missNumber = 0;

        private void Start() {
            _honeyCount = new int[] { 0, 0 };
        }

        private void Update() {
            if (_completedHives != HivesToComplete) return;

            GameManager.Instance.SetGameState(GameState.Won);
        }

        #region Getters & Setters

        public void AddCompleteHive() { _completedHives++; }

        public int GetHoneyCount (int index) { return _honeyCount[index]; }

        public int GetCompletedHives() { return _completedHives; }

        public int GetMissNumber() { return _missNumber; }

        public void ResetMisses() { _missNumber = 0; }

        #endregion

        public void AddMiss() { 
            _missNumber++;
            SoundManager.Instance.Play("Error");

            if (_missNumber < MaxMisses) return;

            // TODO - Handle lose
            GameManager.Instance.SetGameState(GameState.Lose);
        }

    }
}
