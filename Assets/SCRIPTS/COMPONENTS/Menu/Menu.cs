using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GOM.Components.Sounds;
using GOM.Shared;
using GOM.Components.Core;

namespace GOM.Components.Menu {
    public class Menu : MonoBehaviour {
        [SerializeField] private GameObject LoadingScreen;
        [SerializeField] private Image LoadingBar;

        private List<AsyncOperation> _sceneLoading = new List<AsyncOperation>();
        private float _totalSceneProgress;

        #region Methods

        /// <summary>
        /// Function that runs when the start button is clicked.
        /// </summary>
        public void OnStartButton()
        {
            LoadingScreen.SetActive(true);
            _sceneLoading.Add(SceneManager.UnloadSceneAsync((int) SceneIndexes.MENU));
            _sceneLoading.Add(SceneManager.LoadSceneAsync((int) SceneIndexes.GAME, LoadSceneMode.Additive));

            StartCoroutine(loadSceneAsync());
            GameManager.Instance.SetGameState(GameState.Playing);
            //PlayButton();
        }

        public void OnTutorialButton() {
            LoadingScreen.SetActive(true);
            _sceneLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.MENU));
            _sceneLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.TUTORIAL, LoadSceneMode.Additive));

            StartCoroutine(loadSceneAsync());
            GameManager.Instance.SetGameState(GameState.Playing);
        }

        /// <summary>
        /// Function that runs when the exit button is clicked.
        /// </summary>
        public void OnExitButton()
        {
            Application.Quit();
            //PlayButton();
        }

        /// <summary>
        /// Function that plays the general sound of a button.
        /// </summary>
        public void PlayButton()
        {
            SoundManager.Instance.Play("Button");
        }

        private IEnumerator loadSceneAsync()
        {

            for (int i = 0; i < _sceneLoading.Count; i++) {
                while (!_sceneLoading[i].isDone) {
                    _totalSceneProgress = 0;

                    foreach(AsyncOperation operation in _sceneLoading) {
                        _totalSceneProgress += operation.progress;
                    }

                    _totalSceneProgress = (_totalSceneProgress / _sceneLoading.Count) * 100f;
                    LoadingBar.fillAmount = Mathf.RoundToInt(_totalSceneProgress);

                    yield return null;
                }
            }


            LoadingScreen.SetActive(false);
        }

        #endregion
    }
}
