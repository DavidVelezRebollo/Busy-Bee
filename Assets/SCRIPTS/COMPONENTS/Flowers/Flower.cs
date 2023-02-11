using GOM.Components.Honey;
using UnityEngine;
using System.Collections;
using System;

namespace GOM.Components.Flowers {
    public class Flower : MonoBehaviour {
        [SerializeField] private float Speed; // Speed which the flower will move

        private HoneyWaypoint _nextWaypoint; // Waypoint that the flower will follow
        private int _currentWaypoint = 0;
        private bool _waiting = false;

        private void Start() {
            _nextWaypoint = HoneyPath.GetWaypoint(_currentWaypoint);
        }

        private void Update() {
            if (_waiting) return;

            transform.position = Vector3.MoveTowards(transform.position, _nextWaypoint.transform.position, Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _nextWaypoint.transform.position) <= 0.01) {
                if(_nextWaypoint.NeedsStop()) 
                    StartCoroutine(wait(2f));
                else getNextWaypoint();
            }
        }

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

        private IEnumerator wait(float time) {
            _waiting = true;
            yield return new WaitForSeconds(time);
            getNextWaypoint();
            _waiting = false;
        }
    }
}
