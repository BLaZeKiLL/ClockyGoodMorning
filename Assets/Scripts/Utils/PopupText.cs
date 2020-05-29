using System;

using TMPro;

using UnityEngine;

namespace ClokysGoodMorning.Utils {

    public class PopupText : MonoBehaviour {

        private TextMeshPro _text;
        private float _disappearTime = 1f;
        private float _disappearSpeed = 3f;
        private float _speed = 2f;
        private Color _color;
        
        public static PopupText Create(Vector3 position) {
            var go = Instantiate(GameAssets.Current.SnoozeText, position, Quaternion.Euler(70f, 0f, 0f));
            var comp = go.GetComponent<PopupText>();
            comp.Setup();
            return comp;
        }

        private void Awake() {
            _text = GetComponent<TextMeshPro>();
        }

        private void Setup() {
            _color = _text.color;
        }

        private void Update() {
            transform.position += new Vector3(0, _speed, 0f) * Time.deltaTime;
            _disappearTime -= Time.deltaTime;

            if (!(_disappearTime < 0)) return;

            _color.a -= _disappearSpeed * Time.deltaTime;
            _text.color = _color;
            
            if (_color.a < 0f) Destroy(gameObject);
        }

    }

}