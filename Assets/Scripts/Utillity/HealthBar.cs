using Interfaces;
using UnityEngine;

namespace Utillity
{
    public class HealthBar : MonoBehaviour, IHealthBar
    {
        public GameObject HealtBar;

        private float _initialHealthBarLength;
        private Vector3 _cameraPosition;

//        private void OnEnable() {
//            EventManager.StartListening("OnDamageTaken", ShrinkBar);
//        }


        private void Start()
        {
            _initialHealthBarLength = HealtBar.transform.localScale.x;
            _cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
            //Debug.Log("Initial health bar width: " + _initialHealthBarWidth);
            //_healtBar.transform.localScale = new Vector3(10, 0.5f, 0.5f).normalized;
        }

        private void Update()
        {
            // toDo: cut if it eats to much CPU
            HealtBar.transform.LookAt(_cameraPosition);
        }

        public void ShrinkBar(float damage, float life) {
            //Debug.Log("damage: " + damage + " life: " + life);

            // todo : fix this :D
            var percentage = damage * _initialHealthBarLength / life;
            HealtBar.transform.localScale = new Vector3(HealtBar.transform.localScale.x - percentage,
                HealtBar.transform.localScale.y, HealtBar.transform.localScale.z);
        }

        private void OnDamageTaken(int damage)
        {
            Debug.Log(damage);
        }
    }
}