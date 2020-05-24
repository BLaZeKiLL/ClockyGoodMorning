using UnityEngine;

namespace ClokysGoodMorning.UI.Menu {

    public class PauseMenu : MonoBehaviour {

        [SerializeField] private GameObject _root;

        public void Show() {
            _root.SetActive(true);
        }

        public void Hide() {
            _root.SetActive(false);
        }

        public void Resume() {
            Time.timeScale = 1;
            Hide();
        }

    }

}