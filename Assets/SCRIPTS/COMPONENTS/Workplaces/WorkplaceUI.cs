using UnityEngine.UI;
using UnityEngine;

namespace GOM.Components.Workplaces {
    public class WorkplaceUI : MonoBehaviour {
        [SerializeField] private Image ProgressContainer;
        [SerializeField] private Image ProgressBar;

        public void HandleProgressBar(float fillAmount) {
            ProgressContainer.gameObject.SetActive(true);
            ProgressBar.fillAmount = fillAmount;
        }

        public void RestartProgressBar() {
            ProgressBar.fillAmount = 0;
            ProgressContainer.gameObject.SetActive(false);
        }
    }
}
