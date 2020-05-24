using System;

using ClokysGoodMorning.UI;

using CodeBlaze.Systems;

using UnityEngine;

namespace ClokysGoodMorning.Managers {

    public class GameManager : MonoBehaviour {
        
        [SerializeField] private int _levelDuration = 180;
        [SerializeField] private int _hoomanCount = 0;
        [SerializeField] private UIController _ui;

        private TickEvent _gameOverEvent;

        public int LevelDuration => _levelDuration;

        public void AwakeHooman() {
            _hoomanCount--;
            _ui.SetHoomanCount(_hoomanCount);
            if (_hoomanCount == 0) GameWin();
        }
        
        private void Awake() {
            TickSystem.Create();
        }

        private void Start() {
            _gameOverEvent = new TickEvent(TickUtils.SecToTicks(_levelDuration), GameOver);
            _ui.SetHoomanCount(_hoomanCount);
        }

        private void OnDestroy() {
            _gameOverEvent.Destroy();
        }

        private void GameOver(int obj) {
            Time.timeScale = 0f;
            _ui.ShowGameOverMenu();
        }

        public void GamePause() {
            Time.timeScale = 0f;
            _ui.ShowPauseMenu();
        }

        private void GameWin() {
            Time.timeScale = 0f;
            _ui.ShowWinMenu(TickSystem.GetTick());
        }

    }

}