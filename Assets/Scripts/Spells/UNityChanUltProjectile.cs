using UnityEngine;

namespace Spells {
    public class UNityChanUltProjectile : Spell {

        private Vector3 _direction;
        private Rigidbody _rb;

        private void Start() {
            _direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            _rb = GetComponent<Rigidbody>();
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            Destroy(gameObject, 2);
        }

        private void Update() {
            _rb.AddForce(_direction * 100f);
        }
    }
}