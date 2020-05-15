using System.Collections;
using System.Collections.Generic;

using CodeBlaze.Controllers;

using UnityEngine;

namespace ClokysGoodMorning.Controllers {

    [RequireComponent(
        typeof(Animator),
        typeof(TopDownCharacterController),
        typeof(SpecialInputController))]
    public class ClockyAnimationController : MonoBehaviour {

        private Animator _animator;
        private TopDownCharacterController _controller;
        private SpecialInputController _specialController;
        
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Alarm = Animator.StringToHash("Alarm");

        private void Start() {
            _animator = GetComponent<Animator>();
            _controller = GetComponent<TopDownCharacterController>();
            _specialController = GetComponent<SpecialInputController>();
            
            _controller.MovementUpdate += ControllerOnMovementUpdate;
            _specialController.AlarmPress += SpecialControllerOnAlarmPress;
        }

        private void SpecialControllerOnAlarmPress(bool state) {
            _animator.SetBool(Alarm, state);
        }

        private void ControllerOnMovementUpdate(Vector3 direction, float speed) {
            _animator.SetFloat(Speed, direction.magnitude * speed);
        }

    }

}
