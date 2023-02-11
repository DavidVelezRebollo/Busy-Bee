using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

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

        #region Serialized Fields

        [Tooltip("In case we want to start the game from certain scene")]
        [SerializeField] private bool DebugMode;

        #endregion

        #region Private Fields

        private GameState _state; // Current state of the game

        #endregion

        #region Unity Events

        private void Start() {
            _state = GameState.Menu;
            if (!DebugMode)
                StartCoroutine(LoadSceneAsync(0));

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

        #region Auxiliar Methods

        private static IEnumerator LoadSceneAsync(int index) {
            yield return null;

            AsyncOperation async = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
            while (!async.isDone)
                yield return null;
        }

        #endregion
    }

}
