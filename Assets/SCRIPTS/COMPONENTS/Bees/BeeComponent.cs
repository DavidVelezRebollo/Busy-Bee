using GOM.Classes.Bees;
using GOM.Components.Workplaces;
using GOM.Components.Sounds;
using GOM.Shared;
using UnityEngine;

namespace GOM.Components.Bees {
    public class BeeComponent : MonoBehaviour {
        [SerializeField] private Bee BeeType;

        private WorkplaceManager _workplaceManager;
        private SpriteRenderer _renderer;
        private Camera _mainCamera;
        private Vector3 _initialPosition;
        private Vector3 _lastStationPosition;
        private int _lastWorkplace;
        private bool _isMoving;
        private bool _isWorking;

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
            _workplaceManager.SetBee(this, _lastWorkplace);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!_isMoving || !collision.CompareTag("BeeStation")) return;
            if (collision.GetComponentInParent<Workplace>().HaveBee()) return;

            _lastStationPosition = collision.transform.position;
            _lastWorkplace = collision.GetComponentInParent<Workplace>().GetWorkplaceIndex();
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (!_isMoving || !collision.CompareTag("BeeStation")) return;
            _lastStationPosition = Vector3.zero;
            _lastWorkplace = -1;
        }

        public void SetWorkingState(bool isWorking) { _isWorking = isWorking; }

        public float GetWorkSpeed() { return BeeType.WorkSpeed; }

        public float GetEffectiveWorkSpeed() { return BeeType.EffectiveWorkSpeed; }

        public WorkplaceType GetEffectiveWorkplace() { return BeeType.EffectiveWorkplace; }
    }
}