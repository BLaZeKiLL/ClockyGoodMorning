﻿using System;

using ClokysGoodMorning.Managers;

using CodeBlaze.Controllers;

using UnityEngine;

namespace ClokysGoodMorning.Controllers {

    [RequireComponent(typeof(TopDownCharacterController))]
    public class SpecialInputController : MonoBehaviour {

        [SerializeField] private GameManager _gameManager;
        [SerializeField] private AudioSource _AlarmSound;
        
        public delegate void AlarmPressHandler(bool state);
        public event AlarmPressHandler AlarmPress;

        private TopDownCharacterController _controller;

        private void Start() {
            _controller = GetComponent<TopDownCharacterController>();
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                AlarmPress?.Invoke(true);
                _AlarmSound.Play();
                _controller.CanMove = false;
            }

            if (Input.GetKeyUp(KeyCode.Space)) {
                AlarmPress?.Invoke(false);
                _AlarmSound.Stop();
                _controller.CanMove = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape)) {
                _gameManager.GamePause();
            }
        }

    }

}