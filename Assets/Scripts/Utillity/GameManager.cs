using UnityEngine;

namespace Utillity {
    public class GameManager : MonoBehaviour {

        public static bool RandomGeneration = true;
        public GameObject RandomLevelManager;
        public GameObject NormalLevelManager;

        private void Awake() {
            if (RandomGeneration) {
                RandomLevelManager.SetActive(true);
                NormalLevelManager.SetActive(false);
            }
            else {
                RandomLevelManager.SetActive(false);
                NormalLevelManager.SetActive(true);
            }
        }
    }
}