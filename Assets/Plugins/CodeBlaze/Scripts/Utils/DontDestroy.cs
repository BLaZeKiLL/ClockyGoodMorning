using System;

using UnityEngine;

namespace CodeBlaze.Utils {

    public class DontDestroy : MonoBehaviour {

        private static DontDestroy instance;
        
        private void Awake() {
            if (instance != null) Destroy(gameObject);
            else {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

    }

}