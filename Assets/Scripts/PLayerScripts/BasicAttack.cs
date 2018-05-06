using System;
using Spells;
using UnityEngine;
using Utillity;

namespace PLayerScripts {
    public class BasicAttack : MonoBehaviour {

        public GameObject AttackPrefab;
        public float AttackInterval = 1;
        public float AttackDistance = 10f;

        private float _stamp;
        
        private GameObject _target;
        
        
        
        private void Start() {
            _stamp = Time.time;
            PlayerExperience.LevelUpEvent += LevelUp;
        }

        private void Update() {
            if (Time.time > _stamp) {
                _stamp += AttackInterval;
                _target = ProximityProvider.FindEnemyInRange(gameObject, AttackDistance);
                if (_target) {
                    Shoot();
                }
            }
        }

        private void Shoot() {
            var x = Instantiate(AttackPrefab);
            x.transform.position = transform.position;
            x.GetComponent<AttackProjectile>().Target = _target;
            x.GetComponent<AttackProjectile>().Origin = gameObject;
        }

        private void LevelUp(GameObject player) {
            AttackPrefab.GetComponent<AttackProjectile>().Damage += 2;
//            AttackInterval -= 0.2f;
        }
        
        private void OnDisable() {
            PlayerExperience.LevelUpEvent -= LevelUp;
        }

    }
}