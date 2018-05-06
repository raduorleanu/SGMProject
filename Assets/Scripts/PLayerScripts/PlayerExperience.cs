using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

namespace PLayerScripts {
    public class PlayerExperience : MonoBehaviour {
        
        public int CurrentExperience;
        public int Level = 1;
        public GameObject LevelOnUi;
        
        private int _reqToLevelUp;
        private ParticleSystem _levelUParticleSystem;
        
        public delegate void OnLevelUp(GameObject player);
        public static event OnLevelUp LevelUpEvent;
        
        public delegate void OnExperienceGain(int xp, int reqToLevelUp, GameObject player);
        public static event OnExperienceGain OnExperienceGainEvent;
        
        private void Start() {
            Enemy.AddExperience += AddExperience;
            _levelUParticleSystem = GetComponentInChildren<ParticleSystem>();
            _reqToLevelUp = 100;
        }

        private void AddExperience(int experience, int playerCode) {
            if (gameObject.GetHashCode() != playerCode) return;
            CurrentExperience += experience;
            if (OnExperienceGainEvent != null) OnExperienceGainEvent(experience, _reqToLevelUp, gameObject);
            if (CurrentExperience >= _reqToLevelUp) {
                LevelUp();
            }
        }

        private void LevelUp() {
            Level++;
            LevelOnUi.GetComponent<Text>().text = Level.ToString();
            _levelUParticleSystem.Play();
            CurrentExperience = 0;
            _reqToLevelUp *= 2;
            if (LevelUpEvent != null) LevelUpEvent(gameObject);
        }

        private void OnDestroy() {
            Enemy.AddExperience -= AddExperience;
        }
    }
}