using System;
using System.Collections;
using System.Collections.Generic;

using ClokysGoodMorning.Controllers;
using ClokysGoodMorning.Managers;
using ClokysGoodMorning.UI;

using CodeBlaze.Systems;

using UnityEngine;
using UnityEngine.UI;

namespace ClokysGoodMorning.Entities {

    public class Hoomans : MonoBehaviour {
        
        [Range(5, 30)] [SerializeField] private int _snoozeSecs = 10;
        [Range(1, 5)] [SerializeField] private int _wakeUpSecs = 1;
        [Range(0.5f, 2f)] [SerializeField] private float _destroyTimeout = 1f;

        [SerializeField] private Scrollbar _snoozeBar;
        [SerializeField] private List<GameObject> handy;

        private GameManager _gameManager;
        private UIController _ui;

        private Animator[] _hoomans;
        private SpecialInputController _controller;
        
        private TickEvent _ringHandel;
        private TickEvent _snoozeHandel;

        private bool _playerInRange;
        
        private static readonly int Wake = Animator.StringToHash("Wake");

        public void HoomansAwake() {
            handy.ForEach(Destroy);
            _gameManager.AwakeHooman();
            Destroy(gameObject, _destroyTimeout);
        }

        public void Snooze() {
            if (_snoozeHandel != null && !_snoozeHandel.IsDone) return;
            _ui.ToggleAlarmHint(false);
            _snoozeBar.transform.parent.parent.gameObject.SetActive(true);
            _snoozeHandel = new TickEvent(
                TickUtils.SecToTicks(_snoozeSecs),
                tick => {
                    _snoozeBar.transform.parent.parent.gameObject.SetActive(false);
                    if (_playerInRange) _ui.ToggleAlarmHint(true);
                },
                tick => _snoozeBar.size = (float) tick / (float) _snoozeSecs);
        }

        private void Start() {
            _gameManager = FindObjectOfType<GameManager>();
            _ui = FindObjectOfType<UIController>();
            _hoomans = GetComponentsInChildren<Animator>();
        }

        private void OnDestroy() {
            _ringHandel?.Destroy();
        }

        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag("Player")) return;
            _controller = other.GetComponent<SpecialInputController>();
            if (_snoozeHandel == null || _snoozeHandel.IsDone) _ui.ToggleAlarmHint(true);
            _playerInRange = true;
            _controller.AlarmPress += RingHandler;
        }

        private void OnTriggerExit(Collider other) {    
            if (!other.CompareTag("Player")) return;
            _ui.ToggleAlarmHint(false);
            _ui.ToggleAlarmBar(false);
            _playerInRange = false;
            _controller.AlarmPress -= RingHandler;
        }

        private void RingHandler(bool state) {
            if (state) {
                _ui.ToggleAlarmHint(false);
                _ui.ToggleAlarmBar(true);
                if (_snoozeHandel == null || _snoozeHandel.IsDone) {
                    _ringHandel = new TickEvent(
                        TickUtils.SecToTicks(_wakeUpSecs),
                        tick => {
                            _controller.AlarmPress -= RingHandler;
                            _ui.ToggleAlarmBar(false);
                            foreach (var animator in _hoomans) {
                                animator.SetBool(Wake, true);
                            }
                        }, 
                        tick => {
                            _ui.UpdateAlarmBar((float)tick / (_wakeUpSecs * 5));
                        }, 
                        TickEvent.Type.MICRO);
                } else {
                    Debug.Log($"Snooze : {_snoozeHandel.GetTick()} Ticks");
                }
            } else {
                _ui.ToggleAlarmHint(true);
                _ui.ToggleAlarmBar(false);
                _ringHandel?.Destroy();
            }
        }

    }

}