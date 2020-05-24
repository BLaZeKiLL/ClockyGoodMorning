using CodeBlaze.Systems;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClokysGoodMorning.UI.Menu {

    public class WinMenu : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI _timeTaken;
        [SerializeField] private GameObject _root;

        public void Show(int ticks) {
            _root.SetActive(true);
            _timeTaken.text = $"Time Taken : {TickUtils.TicksToSec(ticks)}";
        }

        public void Hide() {
            _root.SetActive(false);
        }

        public void NextLevel() {
            Time.timeScale = 1f;
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        }

    }

}