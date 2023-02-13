using GOM.Components.Flowers;
using GOM.Components.Bees;
using GOM.Components.Player;
using GOM.Components.Core;
using GOM.Shared;
using UnityEngine;

namespace GOM.Components.Workplaces {
    public class Workplace : MonoBehaviour {
        
        [SerializeField] private BeeComponent WorkingBee;
        [SerializeField] private int NewHoneySprite;
        [SerializeField] private WorkplaceType Type;

        private PlayerManager _player;
        private int _index;
        private WorkplaceUI _ui;
        private FlowerComponent _currentFlower;

        private bool _withFlower;

        private void Start() {
            _ui = GetComponent<WorkplaceUI>();
            _player = PlayerManager.Instance;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!collision.gameObject.CompareTag("Honey")) return;

            if (WorkingBee == null) {
                _player.AddMiss();
                _withFlower = false;
                Destroy(collision.gameObject);
                return;
            }

            _currentFlower = collision.GetComponent<FlowerComponent>();
            _currentFlower.OnFlowerProccess += transformPolen;
            _withFlower = true;
            WorkingBee.SetWorkingState(true);
            
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (!collision.gameObject.CompareTag("Honey") || WorkingBee == null) return;

            _withFlower = false;
            _currentFlower.OnFlowerProccess -= transformPolen;
            _currentFlower = null;
            _ui.RestartProgressBar();
            WorkingBee.SetWorkingState(false);
        }

        public void Update() {
            if (!_withFlower || GameManager.Instance.GameStop()) return;
            
            work();
        }

        public bool HaveBee() { return WorkingBee != null; }

        public void SetWorkingBee(BeeComponent bee) { WorkingBee = bee; }

        public BeeComponent GetWorkingBee() { return WorkingBee; }

        public int GetWorkplaceIndex() { return _index; }

        public void SetWorkplaceIndex(int index) { _index = index; }

        private void work() {
            bool isEffective = false;
            int i = 0;

            while (!isEffective && i < WorkingBee.GetEffectiveWorkplaces().Length) {
                if (WorkingBee.GetEffectiveWorkplaces()[i] == Type) {
                    isEffective = true;
                }

                i++;
            }

            float workSpeed = isEffective ? WorkingBee.GetEffectiveWorkSpeed() : WorkingBee.GetWorkSpeed();

            _currentFlower.AddProcessTimeElapsed(workSpeed * Time.deltaTime);
            _ui.HandleProgressBar(_currentFlower.GetProccessPercentage());
        }

        private void transformPolen() {
            _currentFlower.ChangeSprite(NewHoneySprite);
        }
    }
}

