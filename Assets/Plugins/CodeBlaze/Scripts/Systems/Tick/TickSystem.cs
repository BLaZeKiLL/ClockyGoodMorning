using System;

using UnityEngine;

namespace CodeBlaze.Systems {

    public static class TickSystem {

        public class TickArgs : EventArgs {

            public int Tick { get; set; }

        }
        
        public static event EventHandler<TickArgs> OnMicroTick;
        public static event EventHandler<TickArgs> OnTick;
        
        public const float TICK_TIMER = .2f;

        private static int _tick;

        private static GameObject _tickSystemObject;
        
        public static void Create() {
            if (_tickSystemObject != null) return;

            _tickSystemObject = new GameObject("TickSystem");
            _tickSystemObject.AddComponent<TickSystemMono>();
        }

        public static int GetTick() => _tick;

        private class TickSystemMono : MonoBehaviour {
            
            private float _time;

            private void Awake() {
                _tick = 0;
            }

            private void Update() {
                _time += Time.deltaTime;

                if (!(_time >= TICK_TIMER)) return;

                _time -= TICK_TIMER;
                _tick++;
            
                OnMicroTick?.Invoke(this, new TickArgs { Tick = _tick});
            
                if (_tick % 5 == 0) OnTick?.Invoke(this, new TickArgs { Tick = _tick});
            }

        }

    }

}