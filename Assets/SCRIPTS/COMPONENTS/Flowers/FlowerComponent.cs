using GOM.Components.Honey;
using GOM.Classes.Flowers;
using GOM.Components.Core;
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

        #endregion

        #region Private Fields

        private SpriteRenderer _renderer;
        private HoneyWaypoint _nextWaypoint; // Waypoint that the flower will follow
        private HoneyTypes _finalType;
        private int _currentWaypoint = 0;
        private float _processTimeElapsed = 0; //Time that has been processed (seconds)
        private bool _waiting = false;

        #endregion

        #region Unity Events

        private void Start() {
            _renderer = GetComponentInChildren<SpriteRenderer>();
            _nextWaypoint = HoneyPath.GetWaypoint(_currentWaypoint);
            _renderer.sprite = FlowerType.Sprites[0];
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

        public void ChangeSprite(int spriteIndex) { _renderer.sprite = FlowerType.Sprites[spriteIndex]; }

        public void SetFinalType(HoneyTypes finalType) {
            _finalType = finalType;
        }

        #endregion

        #region Auxiliar Methods

        private void getNextWaypoint() {
            if(_currentWaypoint >= HoneyPath.WaypointCount()) {
                recollect();
                return;
            }

            _currentWaypoint += HoneyPath.WaypointEnabled(_currentWaypoint + 1) ? 1 : 4;

            _nextWaypoint = HoneyPath.GetWaypoint(_currentWaypoint);
        }

        private void recollect() {
            Destroy(gameObject);
        }

        private IEnumerator wait() {
            _waiting = true;
            yield return new WaitWhile(() => { return _processTimeElapsed < FlowerType.ProcessTime; });

            getNextWaypoint();
            OnFlowerProccess?.Invoke();
            _waiting = false;
        }

        #endregion
    }
}
