using System;

using UnityEngine;

namespace ClokysGoodMorning.Utils {

    public class GameAssets : MonoBehaviour {

        public GameObject SnoozeText;
        
        public static GameAssets Current { get; private set; }

        private void Start() {
            Current = this;
        }

    }

}