using System;
using UnityEngine;

namespace GOM.Components.Core {
    public enum GameState {
        Menu,
        Paused,
        Playing
    }

    public class GameManager : MonoBehaviour {
        #region Singleton

        public static GameManager Instance;

        private void Awake() {
            if (Instance != null) return;
            
            Instance = this;
        }

        #endregion

        #region Private Fields

        private GameState _state; // Current state of the game

        #endregion

        #region Unity Events

        private void Start() {
            _state = GameState.Menu;

            Screen.SetResolution(Screen.width, Screen.height, !PlayerPrefs.HasKey("FullScreen") || PlayerPrefs.GetInt("FullScreen") != 0);
        }

        #endregion

        #region Getters & Setters

        /// <summary>
        /// Sets the current state of the game
        /// </summary>
        /// <param name="state">The new state of the game</param>
        public void SetGameState(GameState state) {
            _state = state;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the game is currently paused
        /// </summary>
        /// <returns>True if the game is paused or is on the menu. False otherwise</returns>
        public bool GamePaused() {
            return _state == GameState.Paused || _state == GameState.Menu;
        }

        #endregion
    }

}
