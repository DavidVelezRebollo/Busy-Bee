using GOM.Components.Honey;
using GOM.Classes.Flowers;
using GOM.Components.Core;
using GOM.Components.Player;
using GOM.Components.Sounds;
using GOM.Shared;
using UnityEngine;
using System.Collections;
using System;

namespace GOM.Components.Flowers {
    public class FlowerComponent : MonoBehaviour {
        #region Serialized Fields

        [SerializeField] private Flower FlowerType;
        [SerializeField] private float Speed; // Speed which the flower will move


        public Action OnFlowerProccess;
        public Action<bool> OnFlowerFinal;

        #endregion

        #region Private Fields

        private SpriteRenderer _spriteRenderer;
        private HoneyWaypoint _nextWaypoint; // Waypoint that the flower will follow
        private SoundManager _soundManager;
        private HoneyTypes _finalType;
        private BottleType _currentBottle = BottleType.None;
        private float _processTimeElapsed = 0; //Time that has been processed (seconds)
        private bool _waiting = false;

        #endregion

        #region Unity Events

        private void Start() {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _soundManager = SoundManager.Instance;

            _nextWaypoint = GameObject.FindGameObjectWithTag("FirstWaypoint").GetComponent<HoneyWaypoint>();
        }

        private void Update() {
            if (_waiting || GameManager.Instance.GameStop()) return;

            _processTimeElapsed = 0;
            transform.position = Vector3.MoveTowards(transform.position, _nextWaypoint.transform.position, Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _nextWaypoint.transform.position) > 0.01) return;
            
            if (_nextWaypoint.IsWorkplace()) StartCoroutine(wait());
            else getNextWaypoint();
        }

        #endregion

        #region Getters & Setters

        public void AddProcessTimeElapsed(float amount) { _processTimeElapsed += amount; }

        public float GetProccessPercentage() { return _processTimeElapsed / FlowerType.ProcessTime; }

        public void ChangeSprite(int spriteIndex) { _spriteRenderer.sprite = FlowerType.Sprites[spriteIndex]; }

        public void SetFinalType(HoneyTypes finalType) { _finalType = finalType; }

        public HoneyTypes GetFinalType() { return _finalType; }

        public float GetProcessTime() { return FlowerType.ProcessTime; }

        public FlowerType GetFlowerType() { return FlowerType.Type; }

        public BottleType GetCurrentBottle() { return _currentBottle; }

        public void ChangeCurrentBottle(BottleType type) { _currentBottle = type; }

        public void ChangeSpriteOrder(int order) { _spriteRenderer.sortingOrder = order; }

        #endregion

        #region Auxiliar Methods

        private void getNextWaypoint() {
            if(_nextWaypoint.GetNextWaypoint() != null) {
                _nextWaypoint = _nextWaypoint.GetNextWaypoint();
                return;
            }

            if ((_currentBottle == BottleType.Big && (_finalType is HoneyTypes.SweetBig or HoneyTypes.SourBig))
                    || (_currentBottle == BottleType.Small && (_finalType is HoneyTypes.SweetSmall or HoneyTypes.SourSmall)))
                recollect();
            else
                Miss();
        }

        private void recollect() {
            OnFlowerFinal?.Invoke(false);
            HoneyGenerator.SubstractFlower();
            Destroy(gameObject);
        }

        public void Miss() {
            OnFlowerFinal?.Invoke(true);
            PlayerManager.Instance.AddMiss();
            HoneyGenerator.SubstractFlower();
            Destroy(gameObject);
        }

        private IEnumerator wait() {
            _waiting = true;
            yield return new WaitWhile(() => {
                if (!_soundManager.IsPlaying("Machine")) _soundManager.Play("Machine");
                return _processTimeElapsed < FlowerType.ProcessTime; 
            });

            getNextWaypoint();

            _soundManager.Stop("Machine");
            _soundManager.Play("Complete");

            OnFlowerProccess?.Invoke();
            _waiting = false;
        }

        #endregion
    }
}
