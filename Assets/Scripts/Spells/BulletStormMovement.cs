using UnityEngine;

namespace Spells {
    public class BulletStormMovement : Spell {

        private Rigidbody _rb;

        public void Start() {
            _rb = GetComponent<Rigidbody>();
            Destroy(gameObject, 3);
        }
        
        public void Update() {
            _rb.AddForce(transform.forward * 20f);
        }
        
    }
}