using System;

using CodeBlaze.Systems;

using UnityEngine;

namespace ClokysGoodMorning.Managers {

    public class GameManager : MonoBehaviour {

        private void Awake() {
            TickSystem.Create();
        }

    }

}