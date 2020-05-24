using ClokysGoodMorning.UI.Menu;

using TMPro;

using UnityEngine;

namespace ClokysGoodMorning.UI {

    public class UIController : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI _HoomanCount;
        [SerializeField] private WinMenu _winMenu;
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private GameOverMenu _gameOverMenu;

        public void SetHoomanCount(int count) {
            _HoomanCount.text = $"\uf007 {count}";
        }

        public void ShowWinMenu(int ticks) {
            _winMenu.Show(ticks);
        }

        public void ShowPauseMenu() {
            _pauseMenu.Show();
        }

        public void ShowGameOverMenu() {
            _gameOverMenu.Show();
        }

    }

}