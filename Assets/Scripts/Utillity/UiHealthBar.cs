using Interfaces;
using UnityEngine;

namespace Utillity {
    public class UiHealthBar : MonoBehaviour, IHealthBar {
        
        public GameObject HealtBar;

        private float _initialHealthBarLength;

        private void Start() {
            _initialHealthBarLength = HealtBar.transform.localScale.y;
        }

        public void ShrinkBar(float damage, float life) {
            
            //Debug.Log("Incoming damage: " + damage + " incoming life: " + life);
            
            var percentage = damage * 100 / life;
            
            //Debug.Log("Percentage: " + percentage);
            
            var barRepresentationOfDamageTaken = percentage * _initialHealthBarLength / 100;
            
            //Debug.Log("Bar percentage: " + barRepresentationOfDamageTaken);
            
            HealtBar.transform.localScale = new Vector3(HealtBar.transform.localScale.x, 
                HealtBar.transform.localScale.y - barRepresentationOfDamageTaken, HealtBar.transform.localScale.z);
        }
        
        public void GrowBar(float damage, float life) {
            
            //Debug.Log("Incoming damage: " + damage + " incoming life: " + life);
            
            var percentage = damage * 100 / life;
            
            //Debug.Log("Percentage: " + percentage);
            
            var barRepresentationOfDamageTaken = percentage * _initialHealthBarLength / 100;
            
            //Debug.Log("Bar percentage: " + barRepresentationOfDamageTaken);
            
            HealtBar.transform.localScale = new Vector3(HealtBar.transform.localScale.x, 
                HealtBar.transform.localScale.y + barRepresentationOfDamageTaken, HealtBar.transform.localScale.z);
        }

        public void ResetBar() {
            HealtBar.transform.localScale = new Vector3(HealtBar.transform.localScale.x, 
                _initialHealthBarLength, HealtBar.transform.localScale.z);
        }
    }
}