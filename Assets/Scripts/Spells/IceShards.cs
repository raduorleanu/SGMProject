using PLayerScripts;
using UnityEngine;
using Utillity;

namespace Spells {
    public class IceShards : Spell {

        private Vector3 _overHeadPosition;

        private bool _moovingOverHead = true;
        private bool _splitting;
        private bool _shooting;
        private GameObject _target;

        public int Count;

        private void Start() {
            //PlayerExperience.LevelUpEvent += LevelUp;
            transform.position = new Vector3(CastOrigin.transform.position.x, 
                CastOrigin.transform.position.y + 2, CastOrigin.transform.position.z);
            _overHeadPosition = new Vector3(CastOrigin.transform.position.x, 
                CastOrigin.transform.position.y + 7, CastOrigin.transform.position.z);
            Invoke("Die", 4);
            Invoke("Transit", Random.Range(0.3f, 1.2f));
            // AOE is based on size
            transform.localScale = new Vector3(0.1f * SpellLevel, 0.1f * SpellLevel, 0.1f * SpellLevel);
            Damage += 2 * SpellLevel;
        }

        private void Update() {
            if (_moovingOverHead) {
                transform.position = Vector3.Lerp(transform.position, _overHeadPosition, Time.deltaTime * 5);
                    
            }

            if (_splitting) {
                if (Count <= SpellLevel * 2) {
                    var x = Instantiate(this);
                    x.SpellLevel = SpellLevel;
                    x.CastOrigin = CastOrigin;
                    x.Count++;
                    _splitting = false;
                    _shooting = true;
                }
            }

            if (_shooting) {
                if (_target == null) {
                    _target = ProximityProvider.FindClosestEnemyAsPlayer(CastOrigin);
                    //Debug.Log("Found a target " + _target.gameObject.tag);
                }
                else {
                    //transform.LookAt(_target.transform);
                    //transform.Translate(_target.transform.position * Time.deltaTime * 50);
                    //transform.RotateAround(transform.position, Vector3.down, 12f);
                    //_rb.AddForce((_target.transform.position - transform.position) * Speed);
                    transform.position = Vector3.MoveTowards(transform.position, 
                        _target.transform.position, Time.deltaTime * 100);
                }
                //Debug.Log("Moving to " + _target.transform.position);
                //transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * 100);
                
            }
        }

        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.CompareTag("Enemy")) {
                //Invoke("Die", 1);
                Die();
            }
        }

        private void Die() {
            Destroy(gameObject);
        }

        private void Transit() {
            _moovingOverHead = false;
            _splitting = true;
        }
    }
}