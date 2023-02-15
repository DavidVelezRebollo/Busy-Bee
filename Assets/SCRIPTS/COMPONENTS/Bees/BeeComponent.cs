using GOM.Classes.Bees;
using GOM.Components.Workplaces;
using GOM.Components.Sounds;
using GOM.Shared;
using UnityEngine;
using GOM.Components.Core;

namespace GOM.Components.Bees {
    public class BeeComponent : MonoBehaviour {
        [SerializeField] private Bee BeeType;

        private WorkplaceManager _workplaceManager;
        private Workplace _lastWorkplace;
        private SpriteRenderer _renderer;
        private Camera _mainCamera;
        private Vector3 _initialPosition;
        private Vector3 _lastStationPosition;
        private int _intiialSortingOrder;
        private int _lastWorkplaceIndex;
        private int _sortingOrder;
        private bool _block;
        private bool _isMoving;
        private bool _isWorking;
        private bool _flip;

        private void Start() {
            _renderer = GetComponentInChildren<SpriteRenderer>();

            _workplaceManager = WorkplaceManager.Instance;
            _mainCamera = Camera.main;
            _renderer.sprite = BeeType.BeeSprite;
            _initialPosition = transform.position;
            _intiialSortingOrder = _renderer.sortingOrder;
            _lastStationPosition = Vector3.zero;
        }


        private void OnMouseDown() {
            if (_isWorking || _block) return;

            if (SoundManager.Instance.IsPlaying(BeeType.SFX)) return;

            SoundManager.Instance.Play(BeeType.SFX);
        }

        private void OnMouseDrag() {
            if (GameManager.Instance.GameStop() && !GameManager.Instance.InTutorial()) return;
            if (_isWorking || _block) return;

            for(int i = 0; i < _workplaceManager.WorkplaceCount(); i++) {
                    _workplaceManager.GetWorkplace(i).ActivateHighLight(true);
                    _workplaceManager.GetWorkplace(i).SetHighlight(_workplaceManager.GetWorkplaceType(i) == BeeType.EffectiveWorkplace);
            }

            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            transform.position = new Vector3(mousePosition.x, mousePosition.y);
            _renderer.sortingOrder = 10;
            _sortingOrder = _lastWorkplace == null ? 10 : _lastWorkplace.GetBeePostSortingLayer();
            _flip = _lastWorkplace == null ? false : _lastWorkplace.Flip();
            _isMoving = true;
        }

        private void OnMouseUp() {
            _isMoving = false;

            for (int i = 0; i < _workplaceManager.WorkplaceCount(); i++)
            {
                _workplaceManager.GetWorkplace(i).ActivateHighLight(false);
            }

            if (_lastStationPosition == Vector3.zero) {
                transform.position = _initialPosition;
                _renderer.sortingOrder = _intiialSortingOrder;
                return;
            }

            transform.position = _lastStationPosition;
            _initialPosition = _lastStationPosition;

            _renderer.sortingOrder = _sortingOrder;
            _renderer.flipX = _flip;

            _workplaceManager.SetBee(this, _lastWorkplaceIndex);
            _lastWorkplace = _workplaceManager.GetWorkplace(_lastWorkplaceIndex);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!_isMoving || !collision.CompareTag("BeeStation")) return;
            if (collision.GetComponentInParent<Workplace>().HaveBee()) return;

            SetPosition(collision.transform);
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (!_isMoving || !collision.CompareTag("BeeStation")) return;
            _lastStationPosition = Vector3.zero;
            _lastWorkplaceIndex = -1;
            _lastWorkplace = null;
        }

        public void SetPosition(Transform pos)
        {
            _lastStationPosition = pos.transform.position;
            _lastWorkplace = pos.GetComponentInParent<Workplace>();
            _lastWorkplaceIndex = _lastWorkplace.GetWorkplaceIndex();
        }

        public void SetWorkingState(bool isWorking) { _isWorking = isWorking; }

        public float GetWorkSpeed() { return BeeType.WorkSpeed; }

        public float GetEffectiveWorkSpeed() { return BeeType.EffectiveWorkSpeed; }

        public WorkplaceType GetEffectiveWorkplace() { return BeeType.EffectiveWorkplace; }

        public bool IsWorking() { return _isWorking; }

        public void BlockBee(bool block) { _block = block; }
    }
}