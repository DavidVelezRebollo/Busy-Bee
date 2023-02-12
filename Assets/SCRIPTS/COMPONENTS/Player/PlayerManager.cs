using GOM.Components.Core;
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

        private int[] _honeyCount;
        private int _completedHives = 0;

        private int _missNumber = 0;

        private void Start() {
            _honeyCount = new int[] { 0, 0 };
        }

        #region Getters

        public int GetHoneyCount (int index) { return _honeyCount[index]; }

        public int GetCompletedHives() { return _completedHives; }

        public int GetMissNumber() { return _missNumber; }

        #endregion

        public void AddMiss() { 
            _missNumber++;

            if (_missNumber < MaxMisses) return;

            // TODO - Handle lose
            GameManager.Instance.SetGameState(GameState.Lose);
        }

    }
}
