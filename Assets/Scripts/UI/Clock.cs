using System;

using ClokysGoodMorning.Managers;

using CodeBlaze.Systems;

using UnityEngine;
using UnityEngine.UI;

namespace ClokysGoodMorning.UI {

    public class Clock : MonoBehaviour {

        [SerializeField] private GameManager _gameManager;
        
        [SerializeField] private Transform _hourHand;
        [SerializeField] private Transform _minHand;
        [SerializeField] private Image _FillImage;

        [SerializeField] private int _startHour;

        private void Start() {
            _hourHand.rotation = Quaternion.Euler(0f, 0f, _startHour * 30);
            _FillImage.fillAmount = (float)_gameManager.LevelDuration / 60 / 12;
            TickSystem.OnTick += OnTick;
        }

        private void OnDestroy() {
            TickSystem.OnTick -= OnTick;
        }

        private void OnTick(object sender, TickSystem.TickArgs args) {
            _minHand.Rotate(0f, 0f, -4f);
            _hourHand.Rotate(0f, 0f, -0.5f);
        }

    }

}