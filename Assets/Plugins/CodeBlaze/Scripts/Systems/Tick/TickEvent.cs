﻿using System;

using UnityEngine;

namespace CodeBlaze.Systems {

    public class TickEvent {

        public enum Type {

            MICRO,
            NORMAL

        }
        
        public bool IsDone { get; private set; }
        
        private int _eventTick;
        private Type _type;
        private Action<int> _onComplete;
        private Action<int> _onTick;

        private int _tick;
        private int _startTick;

        public TickEvent(int eventTick, Action<int> onComplete = default, Action<int> onTick = default, Type type = Type.NORMAL) {
            _eventTick = eventTick;
            _onComplete = onComplete;
            _onTick = onTick;
            _type = type;

            _startTick = TickSystem.GetTick();
            
            switch (_type) {
                case Type.MICRO:
                    TickSystem.OnMicroTick += OnTick;
                    break;
                case Type.NORMAL:
                    TickSystem.OnTick += OnTick;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_type), _type, "Tick Type Not Valid");
            }
        }

        public int GetTick() => _tick;

        private void OnTick(object sender, TickSystem.TickArgs args) {
            _tick++;

            if (args.Tick - _startTick < _eventTick) {
                _onTick?.Invoke(_tick);
                return;
            }

            IsDone = true;
            _onComplete?.Invoke(_tick);
            Destroy();
        }
        
        public void Destroy() {
            switch (_type) {
                case Type.MICRO:
                    TickSystem.OnMicroTick -= OnTick;
                    break;
                case Type.NORMAL:
                    TickSystem.OnTick -= OnTick;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_type), _type, "Tick Type Not Valid");
            }
        }

    }

}