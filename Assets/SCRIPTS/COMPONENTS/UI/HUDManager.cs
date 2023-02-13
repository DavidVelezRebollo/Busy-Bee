using TMPro;
using GOM.Components.Core;
using GOM.Components.Player;
using GOM.Components.Honey;
using GOM.Classes.UI;
using GOM.Shared;
using UnityEngine.UI;
using UnityEngine;

namespace GOM.Components.UI {
    public class HUDManager : MonoBehaviour {
        [Header("Panels")]
        [SerializeField] GameObject PauseMenu;
        [SerializeField] GameObject EndGameScreen;
        [Space(2)]
        [Header("End Game Screen")]
        [SerializeField] TextMeshProUGUI EndText;
        [SerializeField] TextMeshProUGUI TimeText;
        [SerializeField] TextMeshProUGUI[] HoneyTexts;
        [SerializeField] TextMeshProUGUI CompletedHiveText;
        [SerializeField] Image BeeImage;
        [SerializeField] Sprite[] BeeSprites;
        [Space(10)] [Header("Game UI Elements")] 
        [SerializeField] private Sprite[] HoneyIcons;
        [SerializeField] private Image NextHoney;

        private GameManager _gameManager;
        private PlayerManager _player;
        private Timer _gameTimer;

        private void Start() {
            _gameManager = GameManager.Instance;
            _player = PlayerManager.Instance;
            _gameTimer = new Timer(0, 0);
            HoneyGenerator.OnFlowerGeneration += ShowNextFlower;
        }

        private void Update() {
            PauseMenu.SetActive(_gameManager.GamePaused());

            if (_gameManager.GamePaused()) return;

            if (_gameManager.GameLost()) {
                EndText.text = "Jornada\nCompletada";
                BeeImage.sprite = BeeSprites[0];
                HandleEndGame();
                return;
            } else if (_gameManager.GameWon()) {
                EndText.text = "Te han expulsado\ndel panal";
                BeeImage.sprite = BeeSprites[1];
                HandleEndGame();
                return;
            }

            _gameTimer.UpdateTimer(true);
        }

        private void HandleEndGame() {
            EndGameScreen.SetActive(true);

            TimeText.text = $"{_gameTimer.GetMinuteCount():00}:{_gameTimer.GetSecondCount():00}";
            for (int i = 0; i < HoneyTexts.Length; i++) {
                HoneyTexts[i].text = _player.GetHoneyCount(i).ToString();
            }
            CompletedHiveText.text = _player.GetCompletedHives().ToString();
        }

        private void ShowNextFlower(int index) {
            bool big = Random.Range(0f, 1f) < 0.5;

            if (index == 0) 
                NextHoney.sprite = big ? HoneyIcons[(int)HoneyTypes.SweetBig] : HoneyIcons[(int)HoneyTypes.SweetSmall];
            else
                NextHoney.sprite = big ? HoneyIcons[(int)HoneyTypes.SourBig] : HoneyIcons[(int)HoneyTypes.SourSmall];
            
        }
    }
}
