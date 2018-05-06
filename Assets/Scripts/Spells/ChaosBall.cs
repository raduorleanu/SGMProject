using System.Linq;
using PLayerScripts;
using UnityEngine;
using UnityEngine.AI;

namespace Spells {
    public class ChaosBall : Spell {
        private Rigidbody _rb;
        private Vector3 _f;
        private GameObject _player;
        private GameObject _hitEnemy;

        private bool _acting;
        private Vector3 _enemyPosition;
        private bool _shieldDown;
        private float _pullTime = 1f;

        // Use this for initialization
        private void Start() {
            _player = CastOrigin;
            _rb = GetComponent<Rigidbody>();
            _f = _player.transform.forward;
            transform.position = Origin;  // new Vector3(Origin.x, Origin.y + 2, Origin.z);
            _pullTime = SpellLevel;
            Destroy(gameObject, 5);
        }

        // Update is called once per frame
        private void Update() {
            //transform.position = Vector3.MoveTowards(transform.position, _f * 5, Speed * Time.deltaTime);
            //_rb.AddForce(_f * Speed);
            transform.Rotate(Vector3.back);
//            
//            if (transform.position == TargetPosition) {
//                PullAllIn();
//                Invoke("DestroyAfter", LifeTime);
//            }

            if (_acting) {
                if (!_shieldDown) {
                    Destroy(GetComponent<SphereCollider>());
                    _shieldDown = true;
                }
                //transform.position = Vector3.MoveTowards(transform.position, _enemyPosition, 20 * Time.deltaTime);
                //transform.parent = _hitEnemy.transform;
                transform.position = Vector3.MoveTowards(transform.position, _hitEnemy.transform.position, 24 * Time.deltaTime);
                if (transform.position == _hitEnemy.transform.position) {
                    PullAllIn();
                }

            }
            else {
                _rb.AddForce(_f * Speed * 10);
            }
        }
        
        
        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.CompareTag("Enemy")) {
                _rb.velocity = Vector3.zero;
                _hitEnemy = other.gameObject;
                _enemyPosition = other.gameObject.transform.position;
                //Invoke("PullAllIn", 2);
                _acting = true;
                Invoke("DestroyAfter", _pullTime);
            }
            else {
                //Invoke("DestroyAfter", 1);
            }
        }

        private void PullAllIn() {
            _hitEnemy.GetComponent<NavMeshAgent>().enabled = false;
            var enemies = GameObject.FindGameObjectsWithTag("Enemy").
                Where(e => Vector3.Distance(_enemyPosition, e.transform.position) < 20);
            foreach (var enemy in enemies) {
                enemy.transform.position =
                    Vector3.MoveTowards(enemy.transform.position, transform.position, Speed * 10 * Time.deltaTime);
            }
        }

        private void DestroyAfter() {
            if (!_hitEnemy.name.Contains("Boss")) {
                Destroy(_hitEnemy);
            }
            Destroy(gameObject);
        }
    }
}