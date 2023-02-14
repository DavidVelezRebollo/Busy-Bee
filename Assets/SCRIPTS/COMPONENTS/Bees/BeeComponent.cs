using GOM.Classes.Bees;
using GOM.Components.Workplaces;
using GOM.Components.Sounds;
using GOM.Shared;
using UnityEngine;

namespace GOM.Components.Bees {
    public class BeeComponent : MonoBehaviour {
        [SerializeField] private Bee BeeType;

        private WorkplaceManager _workplaceManager;
        private Workplace _lastWorkplace;
        private SpriteRenderer _renderer;
        private Camera _mainCamera;
        private Vector3 _initialPosition;
        private Vector3 _lastStationPosition;
        private int _lastWorkplaceIndex;
        private int _sortingOrder;
        private bool _isMoving;
        private bool _isWorking;
        private bool _flip;

        private void Start() {
            _renderer = GetComponentInChildren<SpriteRenderer>();

            _workplaceManager = WorkplaceManager.Instance;
            _mainCamera = Camera.main;
            _renderer.sprite = BeeType.BeeSprite;
            _initialPosition = transform.position;
            _lastStationPosition = Vector3.zero;
        }


        private void OnMouseDown() {
            if (_isWorking) return;

            if (SoundManager.Instance.IsPlaying(BeeType.SFX)) return;

            SoundManager.Instance.Play(BeeType.SFX);
        }

        private void OnMouseDrag() {
            if (_isWorking) return;

            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            transform.position = new Vector3(mousePosition.x, mousePosition.y);
            _renderer.sortingOrder = 10;
            _sortingOrder = _lastWorkplace == null ? 10 : _lastWorkplace.GetBeePostSortingLayer();
            _flip = _lastWorkplace == null ? false : _lastWorkplace.Flip();
            _isMoving = true;
        }

        private void OnMouseUp() {
            _isMoving = false;

            if (_lastStationPosition == Vector3.zero) {
                transform.position = _initialPosition;
                return;
            }

            transform.position = _lastStationPosition;
            _initialPosition = _lastStationPosition;

            _renderer.sortingOrder = _sortingOrder;
            _renderer.flipX = _flip;

            _workplaceManager.SetBee(this, _lastWorkplaceIndex);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!_isMoving || !collision.CompareTag("BeeStation")) return;
            if (collision.GetComponentInParent<Workplace>().HaveBee()) return;

            _lastStationPosition = collision.transform.position;
            _lastWorkplace = collision.GetComponentInParent<Workplace>();
            _lastWorkplaceIndex = _lastWorkplace.GetWorkplaceIndex();
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (!_isMoving || !collision.CompareTag("BeeStation")) return;
            _lastStationPosition = Vector3.zero;
            _lastWorkplaceIndex = -1;
            _lastWorkplace = null;
        }

        public void SetWorkingState(bool isWorking) { _isWorking = isWorking; }

        public float GetWorkSpeed() { return BeeType.WorkSpeed; }

        public float GetEffectiveWorkSpeed() { return BeeType.EffectiveWorkSpeed; }

        public WorkplaceType GetEffectiveWorkplace() { return BeeType.EffectiveWorkplace; }

        public bool IsWorking() { return _isWorking; }
    }
}