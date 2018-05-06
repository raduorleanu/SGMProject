using UnityEngine;

namespace Spells {
    public class SpinningBoomerang : Spell {
        
        public float LifeTime;
        
        private Vector3 _extendedTarget;
        private Vector3 _target;

        // Use this for initialization
//        void Start () {
//            _extendedTarget = TargetPosition - Origin;
//            _target = TargetPosition + _extendedTarget * SpellLevel;
//        }
	
        // Update is called once per frame
        void Update () {
            if (!(transform.position == _target)) {
                transform.position = Vector3.MoveTowards(transform.position, _target, Speed * Time.deltaTime);
            }
            
            if (transform.position == _target) {
                ActivateGrip();
            }
        }

        private void ActivateGrip() {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies) {
                enemy.GetComponent<Rigidbody>().AddForce(transform.position * SpellLevel);
                Invoke("DestroyAfter", 0.2f);
            }
        }

        private void DestroyAfter() {
            Destroy(gameObject);
        }
        
    }
}