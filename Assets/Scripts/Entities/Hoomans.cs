using System;
using System.Collections;
using System.Collections.Generic;

using ClokysGoodMorning.Controllers;

using UnityEngine;

namespace ClokysGoodMorning.Entities {

    public class Hoomans : MonoBehaviour {
        
        [Range(5f, 30f)] [SerializeField] private float _snoozeDuartion = 10f;
        [Range(0.5f, 5f)] [SerializeField] private float _wakeyTime = 1f;
        [Range(0.5f, 2f)] [SerializeField] private float _destroyTimeout = 1f;

        private Animator[] _hoomans;
        private SpecialInputController _controller;
        private Coroutine _ringHandel;
        private Coroutine _snoozeHandel;
        private float _currCountdownValue;
        
        private static readonly int Wake = Animator.StringToHash("Wake");

        public void HoomansAwake() {
            Destroy(gameObject, _destroyTimeout);
        }

        public void Snooze() {
            if (_currCountdownValue > float.Epsilon) return;
            Debug.Log("SNOOOZE!!!");
            _snoozeHandel = StartCoroutine(Countdown(_snoozeDuartion));
        }

        private void Start() {
            _hoomans = GetComponentsInChildren<Animator>();
        }

        // private void OnDestroy() {
        //     _controller.AlarmPress -= RingHandler;
        // }

        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag("Player")) return;
            _controller = other.GetComponent<SpecialInputController>();
            _controller.AlarmPress += RingHandler;
        }

        private void OnTriggerExit(Collider other) {
            if (!other.CompareTag("Player")) return;

            _controller.AlarmPress -= RingHandler;
        }

        private void RingHandler(bool state) {
            if (state) {
                if (_currCountdownValue <= float.Epsilon)
                    _ringHandel = StartCoroutine(Ring());
                else {
                    Debug.Log($"Snooze : {_currCountdownValue}");
                }
            } else if (_ringHandel != null) {
                StopCoroutine(_ringHandel);
            }
        }

        private IEnumerator Ring() {
            var time = 0f;

            while (time <= _wakeyTime) {
                time += Time.deltaTime;

                yield return null;
            }
            
            _controller.AlarmPress -= RingHandler;
            
            foreach (var animator in _hoomans) {
                animator.SetBool(Wake, true);
            }
        }
        
        private IEnumerator Countdown(float countdownValue)
        {
            _currCountdownValue = countdownValue;
            while (_currCountdownValue > 0)
            {
                yield return new WaitForSeconds(1.0f);
                _currCountdownValue--;
            }
        }

    }

}