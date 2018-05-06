using PLayerScripts;
using UnityEngine;
using UnityEngine.Analytics;
using Utillity;

namespace Spells {
    public class Fireball : Spell {
        private GameObject _player;
        private Rigidbody _rb;

        private GameObject _target;

        private bool _movingTowardEnemy = false;
        private bool _splitting = false;
        private bool _shooting = false;

        private void Start() {
            //PlayerExperience.LevelUpEvent += LevelUp;
            _player = CastOrigin;
            // cast position is + 2 on the Y axis
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + 0.5f,
                _player.transform.position.z);
            _rb = GetComponent<Rigidbody>();
            _movingTowardEnemy = true;
            Invoke("Die", 5);
        }

        private void Update() {
            if (_movingTowardEnemy) {
                _rb.AddForce(_player.transform.forward * 15);
            }
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Enemy")) {
                _rb.velocity = Vector3.zero;
                _target = other.gameObject;
                var all = ProximityProvider.FindEnemiesAround(_target, 1.5f);
                foreach (var o in all) {
                    o.GetComponent<Enemy>().SubstarctDamageFromLife_(Damage * 1f, gameObject.GetHashCode());
                }

                _movingTowardEnemy = false;
            }
        }

        // ToDo: perform pooling in order to have game object deactivate and then respawned
        private void Die() {
            Destroy(gameObject);
        }

    }
}