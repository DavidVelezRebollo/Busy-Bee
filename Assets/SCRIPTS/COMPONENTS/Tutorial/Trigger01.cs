using GOM.Components.Core;
using UnityEngine;

namespace GOM.Components.Tutorial {
    public class Trigger01 : MonoBehaviour {
        [SerializeField] private GameObject PanelToActivate;

        private void OnTriggerEnter2D(Collider2D collision) {
            PanelToActivate.SetActive(true);
            GameManager.Instance.SetGameState(GameState.Tutorial);
        }
    }
}
