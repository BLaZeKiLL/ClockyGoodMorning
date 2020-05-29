using System;

using ClokysGoodMorning.Controllers;
using ClokysGoodMorning.Entities;

using UnityEngine;

namespace ClokysGoodMorning.Components {

    public class Snooze : MonoBehaviour {

        [SerializeField] private Hoomans _hoomans;

        private HandyStateController _stateController;

        private void Start() {
            _stateController = GetComponentInParent<HandyStateController>();
        }

        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag("Player")) return;
            _hoomans.Snooze(other.transform.position + Vector3.up);
            _stateController.SetStateWander();
        }

    }

}