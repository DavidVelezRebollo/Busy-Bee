using GOM.Components.Flowers;
using GOM.Components.Bees;
using GOM.Components.Player;
using GOM.Components.Core;
using GOM.Components.Sounds;
using GOM.Components.Honey;
using GOM.Shared;
using UnityEngine;

namespace GOM.Components.Workplaces {
    public class Workplace : MonoBehaviour {
        
        [SerializeField] private GameObject HighLight;
        [SerializeField] private BeeComponent WorkingBee;
        [SerializeField] private FlowerLever Lever;
        [SerializeField] private Sprite EffectiveWorkplace;
        [SerializeField] private Sprite HighlightWorkplace;
        [SerializeField] private int BeePostSortingLayer;
        [SerializeField] private int InSortingLayer;
        [SerializeField] private int OutSortingLayer;
        [SerializeField] private int NewHoneySprite;
        [SerializeField] private bool FlipBee;
        [SerializeField] private WorkplaceType Type;
        [SerializeField] private GameObject BeeContainer;

        private PlayerManager _player;
        private SoundManager _soundManager;
        private int _index;
        private WorkplaceUI _ui;
        private FlowerComponent _currentFlower;
        private SpriteRenderer _highlightRenderer;

        private bool _withFlower;

        private void Start() {
            _ui = GetComponent<WorkplaceUI>();
            _player = PlayerManager.Instance;
            _soundManager = SoundManager.Instance;
            _highlightRenderer = HighLight.GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!collision.gameObject.CompareTag("Honey")) return;

            if (WorkingBee == null) {
                _withFlower = false;
                collision.GetComponent<FlowerComponent>().Miss();
                return;
            }

            _currentFlower = collision.GetComponent<FlowerComponent>();
            _currentFlower.OnFlowerProccess += transformPolen;
            _currentFlower.ChangeSpriteOrder(InSortingLayer);
            _withFlower = true;
            WorkingBee.SetWorkingState(true);
            
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (!collision.gameObject.CompareTag("Honey") || WorkingBee == null) return;

            _withFlower = false;

            if (_currentFlower != null) {
                _currentFlower.OnFlowerProccess -= transformPolen;
                _currentFlower.ChangeSpriteOrder(OutSortingLayer);
                _currentFlower = null;
            }

            _ui.RestartProgressBar();
            WorkingBee.SetWorkingState(false);
        }

        public void Update() {
            if (!_withFlower || GameManager.Instance.GameStop()) return;
            
            work();
        }

        public bool HaveBee() { return WorkingBee != null; }

        public void SetWorkingBee(BeeComponent bee) { 
            WorkingBee = bee;
            if (bee != null) { bee.SetPosition(BeeContainer.transform); }
        }

        public BeeComponent GetWorkingBee() { return WorkingBee; }

        public int GetWorkplaceIndex() { return _index; }

        public void SetWorkplaceIndex(int index) { _index = index; }

        public int GetBeePostSortingLayer() { return BeePostSortingLayer; }

        public WorkplaceType GetWorkplaceType() { return Type; }

        public bool Flip() { return FlipBee; }

        public void SetHighlight(bool isEffective)
        {
            if (isEffective)
            {
                _highlightRenderer.sprite = EffectiveWorkplace;
            }
            else
            {
                _highlightRenderer.sprite = HighlightWorkplace;
            }
        }

        public void ActivateHighLight(bool active)
        {
            HighLight.SetActive(active);
        }

        private void work() {
            bool isEffective = false;

            if (WorkingBee.GetEffectiveWorkplace() == Type) {
                isEffective = true;
            }

            float workSpeed = isEffective ? WorkingBee.GetEffectiveWorkSpeed() : WorkingBee.GetWorkSpeed();

            _currentFlower.AddProcessTimeElapsed(workSpeed * Time.deltaTime);
            _ui.HandleProgressBar(_currentFlower.GetProccessPercentage());
        }

        private void transformPolen() {
            if(Lever != null) {
                if (Lever.OnLavander() && _currentFlower.GetFlowerType() != FlowerType.Lavander ||
                    !Lever.OnLavander() && _currentFlower.GetFlowerType() != FlowerType.StrawberryTree) {
                    _currentFlower.Miss();
                    _withFlower = false;

                    return;
                }
            }

            GameObject flowerParticle;

            if (Type == WorkplaceType.Polen) {
                flowerParticle = Instantiate(_currentFlower.GetPolenParticles(), _currentFlower.transform.position, Quaternion.identity);
                Destroy(flowerParticle, 2f);
            }

            if (Type is WorkplaceType.BigBottiling or WorkplaceType.SmallBottling)
                _currentFlower.ChangeCurrentBottle(Type == WorkplaceType.BigBottiling ? BottleType.Big : BottleType.Small);

            if (Type == WorkplaceType.Cover) {
                _currentFlower.ChangeSprite(_currentFlower.GetCurrentBottle() == BottleType.Small ? NewHoneySprite + 1 : NewHoneySprite);
            } else {
                _currentFlower.ChangeSprite(NewHoneySprite);
            }
            _soundManager.Play("Complete");
        }
    }
}

