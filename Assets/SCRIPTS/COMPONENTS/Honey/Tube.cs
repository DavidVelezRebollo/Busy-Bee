using GOM.Components.Flowers;
using UnityEngine.UI;
using UnityEngine;

namespace GOM.Components.Honey {
    public class Tube : MonoBehaviour {
        [SerializeField] float FillSpeed;
        [SerializeField] Image Honey;
        [SerializeField] bool IsHorizontal;

        private bool _isFilling;

        private void Update() {
            if (!_isFilling) {
                Honey.fillAmount -= 0.05f * FillSpeed * Time.deltaTime;
                return;
            }

            Honey.fillAmount += 0.05f * FillSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!collision.CompareTag("Honey")) return;

            Honey.fillAmount = 0f;
            Honey.fillOrigin = IsHorizontal ? (int)Image.OriginHorizontal.Right : (int)Image.OriginVertical.Bottom;
            collision.gameObject.SetActive(false);

            _isFilling = true;
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (!collision.CompareTag("Honey")) return;

            collision.gameObject.SetActive(true);
            Honey.fillOrigin = IsHorizontal ? (int)Image.OriginHorizontal.Left : (int)Image.OriginVertical.Top;
            _isFilling = false;
        }
    }
}
