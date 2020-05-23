using System;

using CodeBlaze.Systems;

using UnityEngine;

namespace ClokysGoodMorning.Managers {

    public class GameManager : MonoBehaviour {

        [SerializeField] private int _levelDuration = 180;

        public int LevelDuration => _levelDuration;
        
        private void Awake() {
            TickSystem.Create();
        }

    }

}