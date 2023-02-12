using UnityEngine;
using GOM.Components.Flowers;
using GOM.Components.Bees;

namespace GOM.Components.Workplaces {
    public abstract class Workplace : MonoBehaviour {
        
        [SerializeField] protected float HoneyProduction;
        [SerializeField] protected BeeComponent WorkingBee;
        [SerializeField] protected int NewHoneySprite;
        
        protected int Index;
        protected FlowerComponent CurrentFlower;
        protected WorkplaceUI UI;

        private bool _withFlower;

        private void Start() {
            UI = GetComponent<WorkplaceUI>();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!collision.gameObject.CompareTag("Honey")) return;

            CurrentFlower = collision.GetComponent<FlowerComponent>();
            CurrentFlower.OnFlowerProccess += TransformPolen;
            _withFlower = true;
            WorkingBee.SetWorkingState(true);
            
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (!collision.gameObject.CompareTag("Honey")) return;

            _withFlower = false;
            CurrentFlower.OnFlowerProccess -= TransformPolen;
            CurrentFlower = null;
            UI.RestartProgressBar();
            WorkingBee.SetWorkingState(false);
        }

        public void Update() {
            if (!_withFlower) return;
            
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

