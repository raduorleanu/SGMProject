using UnityEngine;

namespace PLayerScripts {
    public class PlayerExperienceBar : MonoBehaviour {
        [SerializeField] private GameObject _lifeBar;

        private float _maxLen = 13f;

        private void Start() {
            PlayerExperience.OnExperienceGainEvent += ResizeBar;
            PlayerExperience.LevelUpEvent += LevelUp;
            _lifeBar.transform.localScale = new Vector3(0,
                _lifeBar.transform.localScale.y, _lifeBar.transform.localScale.z);
        }

        private void ResizeBar(int xp, int reqToLevelUp, GameObject player) {
            if (player.GetHashCode() != gameObject.GetHashCode()) return;
            var percentageOfRequiredCurrentXpIs = xp * 100.0f / reqToLevelUp;
            var lengthTheXpBarShouldIncrease = _maxLen * percentageOfRequiredCurrentXpIs / 100;

//            Debug.Log("Received " + xp + ", req to lvl up: " + reqToLevelUp +
//                      " percentage: " + percentageOfRequiredCurrentXpIs + " length of the bar: " + lengthTheXpBarShouldIncrease);
            
            _lifeBar.transform.localScale = new Vector3(_lifeBar.transform.localScale.x + lengthTheXpBarShouldIncrease,
                _lifeBar.transform.localScale.y, _lifeBar.transform.localScale.z);
        }

        private void LevelUp(GameObject player) {
            if (player.GetHashCode() != gameObject.GetHashCode()) return;
            _lifeBar.transform.localScale = new Vector3(0,
                _lifeBar.transform.localScale.y, _lifeBar.transform.localScale.z);
        }

        private void OnDestroy() {
            PlayerExperience.OnExperienceGainEvent -= ResizeBar;
            PlayerExperience.LevelUpEvent -= LevelUp;
        }
    }
}