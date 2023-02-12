using GOM.Components.Flowers;
using GOM.Components.Bees;
using GOM.Components.Player;
using GOM.Components.Core;
using UnityEngine;

namespace GOM.Components.Workplaces {
    public abstract class Workplace : MonoBehaviour {
        
        [SerializeField] protected float HoneyProduction;
        [SerializeField] protected BeeComponent WorkingBee;
        [SerializeField] protected int NewHoneySprite;

        protected PlayerManager _player;
        protected int Index;
        protected FlowerComponent CurrentFlower;
        protected WorkplaceUI UI;

        private bool _withFlower;

        private void Start() {
            UI = GetComponent<WorkplaceUI>();
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

            CurrentFlower = collision.GetComponent<FlowerComponent>();
            CurrentFlower.OnFlowerProccess += TransformPolen;
            _withFlower = true;
            WorkingBee.SetWorkingState(true);
            
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (!collision.gameObject.CompareTag("Honey") || WorkingBee == null) return;

            _withFlower = false;
            CurrentFlower.OnFlowerProccess -= TransformPolen;
            CurrentFlower = null;
            UI.RestartProgressBar();
            WorkingBee.SetWorkingState(false);
        }

        public void Update() {
            if (!_withFlower || GameManager.Instance.GameStop()) return;
            
            Work();
        }

        public bool HaveBee() { return WorkingBee != null; }

        public void SetWorkingBee(BeeComponent bee) { WorkingBee = bee; }

        public BeeComponent GetWorkingBee() { return WorkingBee; }

        public int GetWorkplaceIndex() { return Index; }

        public void SetWorkplaceIndex(int index) { Index = index; }

        public void Work() {
            CurrentFlower.AddProcessTimeElapsed(HoneyProduction * Time.deltaTime);
            UI.HandleProgressBar(CurrentFlower.GetProccessPercentage());
        }

        public abstract void TransformPolen();
    }
}

