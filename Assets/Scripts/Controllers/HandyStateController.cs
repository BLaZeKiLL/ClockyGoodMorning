using System;

using UnityEngine;

namespace ClokysGoodMorning.Controllers {

    public class HandyStateController : MonoBehaviour {

        public delegate void PlayerInRangeHandler(Vector3 selfPosition, Vector3 playerPosition);

        public event PlayerInRangeHandler PlayerInRange;
        
        private Animator _animator;
        
        private static readonly int Wander = Animator.StringToHash("Wander");
        private static readonly int Chase = Animator.StringToHash("Chase");

        private void Start() {
            _animator = GetComponentInChildren<Animator>();
            _animator.SetBool(Wander, true);
        }

        private void OnTriggerEnter(Collider other) {
            if(!other.CompareTag("Player")) return;
            _animator.SetBool(Chase, true);
            _animator.SetBool(Wander, false);
        }

        private void OnTriggerStay(Collider other) {
            if(!other.CompareTag("Player")) return;
            PlayerInRange?.Invoke(transform.position, other.transform.position);
        }

        private void OnTriggerExit(Collider other) {
            if(!other.CompareTag("Player")) return;
            _animator.SetBool(Chase, false);
            _animator.SetBool(Wander, true);
        }

    }

}