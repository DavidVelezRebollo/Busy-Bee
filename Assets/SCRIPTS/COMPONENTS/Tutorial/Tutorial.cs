using GOM.Components.Bees;
using GOM.Components.Player;
using GOM.Components.Core;
using UnityEngine;

namespace GOM.Components.Tutorial {
    public class Tutorial : MonoBehaviour {
        [SerializeField] BeeComponent FirstBee;
        [SerializeField] GameObject Panel01;
        private GameManager _gameManager; 
        private PlayerManager _player;

        private bool _aux;

        private void Start() {
            _gameManager = GameManager.Instance;
            _gameManager.SetGameState(GameState.Tutorial);
            _player = PlayerManager.Instance;
            FirstBee.BlockBee(true);
        }

        private void Update() {
            if(_player.GetMissNumber() == 0 || _aux) return;

            Panel01.SetActive(true);
            _gameManager.SetGameState(GameState.Tutorial);
            _player.ResetMisses();
            _aux = true;
        }

        public void OnResumeGameButton() {
            _gameManager.SetGameState(GameState.Playing);
        }
    }
}
