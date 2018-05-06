using PLayerScripts;
using UnityEngine;
using UnityEngine.Analytics;

namespace Spells {
    public class UnityChanUlt : Spell {


        public GameObject Projectile;

        private bool _gettingOverhead;
        private bool _moving;
        private bool _spinning;

        private Vector3 _point;
        private Vector3 _fwd;

        private Rigidbody _rb;

        private void Start() {
            transform.position = CastOrigin.transform.position;
            _gettingOverhead = true;
            _point = new Vector3(CastOrigin.transform.position.x, CastOrigin.transform.position.y + 14,
                CastOrigin.transform.position.z);

            _rb = GetComponent<Rigidbody>();
            _fwd = CastOrigin.transform.forward;
            Projectile.GetComponent<Spell>().Damage += (2 * SpellLevel);
//            _fwd = new Vector3(CastOrigin.transform.forward.x, CastOrigin.transform.forward.y + 10,
//                CastOrigin.transform.forward.z);
        }

        private void Update() {
            if (_gettingOverhead) {
                transform.position = Vector3.MoveTowards(transform.position, _point,
                    Time.deltaTime * 20);
                if (transform.position == _point) {
                    _gettingOverhead = false;
                    _moving = true;
                    Invoke("CancelMoving", 0.7f);
                }
            }

            if (_moving) {
//                transform.position = Vector3.Lerp(transform.position, CastOrigin.transform.forward, Time.deltaTime * 25);
                _rb.AddForce(_fwd * 50);
            }

            if (_spinning) {
                transform.Translate(transform.forward);
                for (var i = 0; i < 3; i++) {
                    var x = Instantiate(Projectile);
                    
                    // Set projectile Origin as Spell Origin for XP gain
                    x.GetComponent<Spell>().CastOrigin = CastOrigin;
                    x.GetComponent<Spell>().Damage += SpellLevel * 2;
                    x.transform.position = transform.position;
                }
            }
        }

        private void CancelMoving() {
            _moving = false;
            _spinning = true;
            Destroy(_rb);
            Invoke("CancelSpinning", 2);
        }

        private void CancelSpinning() {
            _spinning = false;
            Destroy(gameObject);
        }
    }
}