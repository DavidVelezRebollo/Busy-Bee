using GOM.Shared;
using GOM.Components.Sounds;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

namespace GOM.Components.Core {
    public enum GameState {
        Menu,
        Paused,
        Lose,
        Won,
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

        #region Serialized Fields

        [Tooltip("In case we want to start the game from certain scene")]
        [SerializeField] private bool DebugMode;

        #endregion

        #region Private Fields

        private GameState _state; // Current state of the game

        #endregion

        #region Unity Events

        private void Start() {
            SetGameState(!DebugMode ? GameState.Menu : GameState.Playing);
            if (!DebugMode) {
                StartCoroutine(LoadSceneAsync((int) SceneIndexes.MENU));
            }

            Screen.SetResolution(Screen.width, Screen.height, !PlayerPrefs.HasKey("FullScreen") || PlayerPrefs.GetInt("FullScreen") != 0);
        }

        private void Update() {
            if(!Input.GetKeyDown(KeyCode.Escape)) return;
            if (_state != GameState.Playing && _state != GameState.Paused) return;

            _state = _state == GameState.Playing ? GameState.Paused : GameState.Playing;
        }

        #endregion

        #region Getters & Setters

        /// <summary>
        /// Sets the current state of the game
        /// </summary>
        /// <param name="state">The new state of the game</param>
        public void SetGameState(GameState state) {
            _state = state;
            HandleMusic();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the game is currently paused
        /// </summary>
        /// <returns>True if the game is paused or is on the menu. False otherwise</returns>
        public bool GameStop() {
            return _state != GameState.Playing;
        }

        public bool GamePaused() {
            return _state == GameState.Paused;
        }

        public bool GameLost() {
            return _state == GameState.Lose;
        }

        public bool GameWon() {
            return _state == GameState.Won;
        }

        #endregion

        #region Auxiliar Methods

        private static IEnumerator LoadSceneAsync(int index) {
            yield return null;

            AsyncOperation async = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
            while (!async.isDone)
                yield return null;
        }

        private void HandleMusic() {
            SoundManager.Instance.Play(_state == GameState.Menu ? "MusicMenu" : "MainTheme");
            SoundManager.Instance.Stop(_state == GameState.Menu ? "MainTheme" : "MusicMenu");
        }

        #endregion
    }

}
