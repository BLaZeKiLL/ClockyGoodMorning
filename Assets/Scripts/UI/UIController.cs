using ClokysGoodMorning.UI.Menu;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ClokysGoodMorning.UI {

    public class UIController : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI _HoomanCount;
        [SerializeField] private GameObject _AlarmHint;
        [SerializeField] private Scrollbar _AlarmBar;
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

        public void ToggleAlarmHint(bool state) {
            _AlarmHint.SetActive(state);
        }

        public void ToggleAlarmBar(bool state) {
            _AlarmBar.transform.parent.gameObject.SetActive(state);
        }

        public void UpdateAlarmBar(float size) {
            _AlarmBar.size = size;
        }

        public void MainMenu() {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }

    }

}