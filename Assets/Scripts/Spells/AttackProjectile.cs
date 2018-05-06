using UnityEngine;
using Utillity;

namespace Spells {
    public class AttackProjectile : MonoBehaviour{
        
        [HideInInspector]
        public GameObject Target;

        [HideInInspector] public GameObject Origin;
        
        
        public int Damage = 10;
        public float ProjectileSpeed = 2;


        private void Update() {
            if (Target) {
                transform.position = Vector3.MoveTowards(transform.position, Target.transform.position,
                    ProjectileSpeed * Time.deltaTime);

                if (transform.position == Target.transform.position) {
                    Destroy(gameObject);
                }
            }
            else {
                Destroy(gameObject);
            }
        }
        
    }
}