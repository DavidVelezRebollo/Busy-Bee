using GOM.Components.Bees;
using GOM.Components.Workplaces;
using GOM.Components.Core;
using UnityEngine;
using UnityEngine.UI;

namespace GOM.Components.Menu
{
    public class BeeShop : MonoBehaviour
    {
        [SerializeField] private BeeComponent[] Bees;
        private int _beesSelected = 3;
        private WorkplaceManager _workplaceManager;
        private GameManager _gameManager;
        [SerializeField] private Button[] Buttons;
        [SerializeField] private Sprite[] ButtonSprites;

        private void Start()
        {
            _workplaceManager = WorkplaceManager.Instance;
            _gameManager = GameManager.Instance;

            for(int i=0; i<Bees.Length; i++)
            {
                Buttons[i].image.sprite = _workplaceManager.HasBee(i) ? ButtonSprites[1] : ButtonSprites[0];
            }
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
                Buttons[index].image.sprite = ButtonSprites[0];
            }
            else
            {
                if (_beesSelected >= 3) return;
                _beesSelected++;
                Bees[index].gameObject.SetActive(true);
                _workplaceManager.AddBee(Bees[index]);
                Debug.Log("Abeja" + index + "activada");
                Buttons[index].image.sprite = ButtonSprites[1];
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
