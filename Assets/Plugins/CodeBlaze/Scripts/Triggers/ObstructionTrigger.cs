using System;
using System.Collections.Generic;

using UnityEngine;

namespace ClokysGoodMorning.Plugins.CodeBlaze.Scripts.Triggers {

    public class ObstructionTrigger : MonoBehaviour {

        [SerializeField] private List<GameObject> _activeTargets;
        [SerializeField] private List<GameObject> _hideTargets;
        
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _activeTargets.ForEach(target => target.SetActive(true));
                _hideTargets.ForEach(target => target.SetActive(false));
            }
        }

    }

}