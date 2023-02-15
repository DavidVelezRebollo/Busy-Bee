using GOM.Components.Bees;
using GOM.Components.Workplaces;
using GOM.Components.Core;
using UnityEngine;

namespace GOM.Components.Menu
{
    public class BeeShop : MonoBehaviour
    {
        [SerializeField] private BeeComponent[] Bees;
        private int _beesSelected = 3;
        private WorkplaceManager _workplaceManager;
        private GameManager _gameManager;

        private void Start()
        {
            _workplaceManager = WorkplaceManager.Instance;
            _gameManager = GameManager.Instance;
        }

        public void OnBeeSelected(int index)
        {
            bool active = Bees[index].isActiveAndEnabled;

            if(active)
            {
                if (Bees[index].IsWorking()) return;
                _beesSelected--;
                Bees[index].gameObject.SetActive(false);
                _workplaceManager.DeleteBee(Bees[index]);
                Debug.Log("Abeja" + index + "desactivada");
            }
            else
            {
                if (_beesSelected >= 3) return;
                _beesSelected++;
                Bees[index].gameObject.SetActive(true);
                _workplaceManager.AddBee(Bees[index]);
                Debug.Log("Abeja" + index + "activada");
            }
        }

        public void ActiveBeeShop() {
            GameState state;

            state = _gameManager.InTutorial() ? GameState.Tutorial : GameState.Paused;

            GameManager.Instance.SetGameState(GameManager.Instance.GamePaused() ? GameState.Playing : state);
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }
    }
}
