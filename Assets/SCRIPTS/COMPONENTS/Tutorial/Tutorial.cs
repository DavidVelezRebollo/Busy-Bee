using GOM.Components.Bees;
using GOM.Components.Player;
using GOM.Components.Core;
using GOM.Components.Workplaces;
using System.Collections;
using UnityEngine;

namespace GOM.Components.Tutorial {
    public class Tutorial : MonoBehaviour {
        [SerializeField] private BeeComponent FirstBee;
        [SerializeField] private GameObject Panel01;
        [SerializeField] private Workplace TutorialWorkplace;
        [SerializeField] private GameObject Button01;
        private GameManager _gameManager; 
        private PlayerManager _player;

        private bool _aux;
        private bool _aux2 = false;

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

            if(_player.GetMissNumber() == 0 || _aux) return;

            Panel01.SetActive(true);
            _gameManager.SetGameState(GameState.Tutorial);
            _player.ResetMisses();
            _aux = true;
        }

        public void OnResumeGameButton() {
            _gameManager.SetGameState(GameState.Playing);
        }

        public void ToggleAux() {
            _aux2 = !_aux2;
        }

        private IEnumerator WaitAction() {
             {
                yield return null;
            }

            
        }
    }
}
