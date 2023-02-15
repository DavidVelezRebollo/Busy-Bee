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
        [SerializeField] private GameObject EndGame;
        [SerializeField] private GameObject[] Fails;

        private int[] _honeyCount;
        private int _completedHives = 0;

        private int _missNumber = 0;

        private void Start() {
            _honeyCount = new int[] { 0, 0 };
        }

        private void Update() {
            if (_completedHives != HivesToComplete) return;

            GameManager.Instance.SetGameState(GameState.Won);
            EndGame.SetActive(true);
        }

        #region Getters & Setters

        public void AddCompleteHive() { _completedHives++; }

        public int GetHoneyCount (int index) { return _honeyCount[index]; }

        public int GetCompletedHives() { return _completedHives; }

        public int GetMissNumber() { return _missNumber; }

        public void ResetMisses() { 
            _missNumber = 0;

            for(int i = 0; i < Fails.Length; i++) {
                Fails[i].SetActive(false);
            }
        }

        #endregion

        public void AddMiss() {
            Fails[_missNumber].SetActive(true);
            _missNumber++;
            SoundManager.Instance.Play("Error");
            
            if (_missNumber < MaxMisses) return;

            // TODO - Handle lose
            EndGame.SetActive(true);
            GameManager.Instance.SetGameState(GameState.Lose);
        }

    }
}
