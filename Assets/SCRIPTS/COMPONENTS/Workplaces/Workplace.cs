using UnityEngine;
using GOM.Components.Flowers;
using GOM.Classes.Bees;

namespace GOM.Components.Workplaces {
    public abstract class Workplace : MonoBehaviour {
        
        [SerializeField] protected int HoneyProduction;
        [SerializeField] protected Sprite NewHoneySprite;
        protected Bee WorkingBee;
        protected FlowerComponent CurrentFlower;

        private bool _withFlower;

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
        }

        public void Update() {
            if (!_withFlower) return;
            
            Work();
        }
        public void Work() {
            CurrentFlower.AddProcessTimeElapsed(HoneyProduction * Time.deltaTime);
        }

        public abstract void TransformPolen();
    }
}

