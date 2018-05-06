using System.Collections.Generic;
using System.Linq;
using PLayerScripts;
using UnityEngine;

namespace Spells {
    public class ChainLightning : Spell {
        
        private int _maxNumberOfTargets;

        private int _currentTargetIndex = 0;

        private GameObject _player;
        private Rigidbody _rb;
        private Vector3 _f;
        private bool _chaining;
        private Vector3 _closestEnemy;
        private List<GameObject> _alreadyBouncedTo;
        
        
        public float MoveSpeed = 15.0f;
 
        public float Frequency = 20.0f;  // Speed of sine movement
        public float Magnitude = 0.5f;   // Size of sine movement
        private Vector3 _axis;
 
        private Vector3 _pos;


        private void Start() {
            _maxNumberOfTargets = SpellLevel;
            transform.position = Origin;
            _player = CastOrigin;
            //_player = GameObject.FindWithTag("Player");
            _rb = GetComponent<Rigidbody>();
            _f = _player.transform.forward;
            _alreadyBouncedTo = new List<GameObject>();
            Invoke("DestroyAfter", 3);
            
            _pos = transform.position;
            _axis = transform.right;
            Damage += 2 * SpellLevel;

        }

        private void Update() {

            if (_chaining) {
                transform.position = Vector3.MoveTowards(transform.position, _closestEnemy, Speed * Time.deltaTime);
            }
            else {
                //_rb.AddForce(_f * Speed);
                //transform.Translate(_f * Time.deltaTime * 10);
                //transform.RotateAround(CastOrigin.transform.position, transform.position, Angle);
                
                _pos += _f * Time.deltaTime * MoveSpeed;
                transform.position = _pos + _axis * Mathf.Sin (Time.time * Frequency) * Magnitude;
            }
        }

        private void OnCollisionEnter(Collision other) {
            if (!other.gameObject.CompareTag("Enemy")) return;
            _rb.velocity = Vector3.zero;


            var allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (_alreadyBouncedTo.Count > allEnemies.Length - 3) {
                _alreadyBouncedTo.Clear();
            }
            
            var enemy = allEnemies
                .Where(i => !i.gameObject.Equals(other.gameObject)
                            && i.gameObject.CompareTag("Enemy")
                            && !_alreadyBouncedTo.Contains(i.gameObject))
                .OrderBy(i => Vector3.Distance(transform.position, i.gameObject.transform.position))
                .First();

            _closestEnemy = enemy.transform.position;

            _alreadyBouncedTo.Add(enemy);

            _chaining = true;
        }

        private void DestroyAfter() {
            Destroy(gameObject);
        }
    }
}