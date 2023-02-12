using UnityEngine;
using GOM.Components.Flowers;
using GOM.Classes.Bees;

namespace GOM.Components.Workplaces {
    public abstract class Workplace : MonoBehaviour {
        
        [SerializeField] protected float HoneyProduction;
        [SerializeField] protected Sprite NewHoneySprite;

        protected Bee WorkingBee;
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
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (!collision.gameObject.CompareTag("Honey")) return;

            _withFlower = false;
            CurrentFlower.OnFlowerProccess -= TransformPolen;
            CurrentFlower = null;
            UI.RestartProgressBar();
        }

        public void Update() {
            if (!_withFlower) return;
            
            Work();
        }

        public void Work() {
            CurrentFlower.AddProcessTimeElapsed(HoneyProduction * Time.deltaTime);
            UI.HandleProgressBar(CurrentFlower.GetProccessPercentage());
        }

        public abstract void TransformPolen();
    }
}

