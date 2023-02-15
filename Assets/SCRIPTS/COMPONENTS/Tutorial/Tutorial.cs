using GOM.Components.Bees;
using GOM.Components.Player;
using GOM.Components.Core;
using GOM.Components.Workplaces;
using GOM.Components.Menu;
using GOM.Components.Sounds;
using GOM.Shared;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GOM.Components.Tutorial {
    public class Tutorial : MonoBehaviour {
        [SerializeField] private BeeComponent FirstBee;
        [SerializeField] private BeeShop Shop;
        [SerializeField] private GameObject Panel01;
        [SerializeField] private Workplace TutorialWorkplace;
        [SerializeField] private GameObject Button01;
        [SerializeField] private GameObject ShopTutorialPanel01;
        [SerializeField] private GameObject ShopTutorialPanel02;
        [SerializeField] private GameObject FinalPanel;
        [SerializeField] private GameObject MissPanel;
        private GameManager _gameManager; 
        private PlayerManager _player;

        private bool _aux = false;
        private bool _aux2 = false;
        private bool _aux3 = false;
        private bool _aux4 = false;
        private bool _aux5 = false;
        private bool _firstActivation = false;

        private void Start() {
            _gameManager = GameManager.Instance;
            _gameManager.SetGameState(GameState.Tutorial);
            _player = PlayerManager.Instance;
            FirstBee.BlockBee(true);
        }

        private void Update() {

            if(TutorialWorkplace.HaveBee() && _aux2 == true) {
                Button01.SetActive(true);
                _aux2 = false;
            }

            if(_aux3 && _firstActivation) {
                ShopTutorialPanel01.SetActive(true);
                _aux3 = false;
            }

            if (Shop.ActiveBees() == 3 && _aux4) {
                ShopTutorialPanel01.SetActive(false);
                ShopTutorialPanel02.SetActive(true);
                Shop.OpenAndClose();
                _aux4 = false;
            }

            if(_player.GetCompletedHives() == 1) {
                FinalPanel.SetActive(true);
                _gameManager.SetGameState(GameState.Tutorial);
            }

            if(_player.GetMissNumber() >= 2) {
                MissPanel.SetActive(true);
                _gameManager.SetGameState(GameState.Tutorial);
                _player.ResetMisses();
            }

            if((_player.GetMissNumber() == 0 && !_aux5) || _aux) return;

            Panel01.SetActive(true);
            _gameManager.SetGameState(GameState.Tutorial);
            _player.ResetMisses();
            _aux = true;
            _aux5 = true;
        }

        public void OnResumeGameButton() {
            _gameManager.SetGameState(GameState.Playing);
        }

        public void OnExitButton() {
            StartCoroutine(LoadSceneAsync((int)SceneIndexes.MENU));
        }

        public void PlayButton() {
            SoundManager.Instance.Play("Button");
        }

        public void ToggleAux02() {
            _aux2 = !_aux2;
        }

        public void ToggleAux03() {
            _aux3 = !_aux3;
        }

        public void ToggleAux04() {
            _aux4 = !_aux4;
        }

        public void ToggleAux05() {
            _aux5 = !_aux5;
        }

        public void ToggleFirstActivation() {
            _firstActivation = true;
        }

        private static IEnumerator LoadSceneAsync(int index) {
            yield return null;

            AsyncOperation async = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
            while (!async.isDone)
                yield return null;
        }
    }
}
