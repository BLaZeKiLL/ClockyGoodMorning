using System;

using UnityEngine;

namespace ClokysGoodMorning.Controllers {

    public class UIAudioController : MonoBehaviour {

        private AudioSource _source;

        private void Start() {
            _source = GetComponent<AudioSource>();
        }

        public void ButtonClick() {
            _source.Play();
        }

    }

}