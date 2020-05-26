using System;
using System.Collections;
using System.Collections.Generic;

using ClokysGoodMorning.Controllers;
using ClokysGoodMorning.Managers;
using ClokysGoodMorning.UI;

using CodeBlaze.Systems;

using UnityEngine;

namespace ClokysGoodMorning.Entities {

    public class Hoomans : MonoBehaviour {
        
        [Range(5, 30)] [SerializeField] private int _snoozeSecs = 10;
        [Range(1, 5)] [SerializeField] private int _wakeUpSecs = 1;
        [Range(0.5f, 2f)] [SerializeField] private float _destroyTimeout = 1f;
        [SerializeField] private List<GameObject> handy;

        private GameManager _gameManager;
        private UIController _ui;

        private Animator[] _hoomans;
        private SpecialInputController _controller;
        
        private TickEvent _ringHandel;
        private TickEvent _snoozeHandel;
        
        private static readonly int Wake = Animator.StringToHash("Wake");

        public void HoomansAwake() {
            handy.ForEach(Destroy);
            Destroy(gameObject, _destroyTimeout);
        }

        public void Snooze() {
            if (_snoozeHandel == null || _snoozeHandel.IsDone)
                _snoozeHandel = new TickEvent(TickUtils.SecToTicks(_snoozeSecs));
        }

        private void Start() {
            _gameManager = FindObjectOfType<GameManager>();
            _ui = FindObjectOfType<UIController>();
            _hoomans = GetComponentsInChildren<Animator>();
        }

        private void OnDestroy() {
            _ringHandel?.Destroy();
            _gameManager.AwakeHooman();
        }

        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag("Player")) return;
            _controller = other.GetComponent<SpecialInputController>();
            _ui.ToggleAlarmHint(true);
            _controller.AlarmPress += RingHandler;
        }

        private void OnTriggerExit(Collider other) {    
            if (!other.CompareTag("Player")) return;
            _ui.ToggleAlarmHint(false);
            _controller.AlarmPress -= RingHandler;
        }

        private void RingHandler(bool state) {
            if (state) {
                _ui.ToggleAlarmHint(false);
                if (_snoozeHandel == null || _snoozeHandel.IsDone) {
                    _ringHandel = new TickEvent(TickUtils.SecToTicks(_wakeUpSecs), tick => {
                        _controller.AlarmPress -= RingHandler;
                        foreach (var animator in _hoomans) {
                            animator.SetBool(Wake, true);
                        }
                    });
                } else {
                    Debug.Log($"Snooze : {_snoozeHandel.GetTick()} Ticks");
                }
            } else {
                _ui.ToggleAlarmHint(true);
                _ringHandel?.Destroy();
            }
        }

    }

}