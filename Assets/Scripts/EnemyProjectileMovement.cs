using Interfaces;
using UnityEngine;
using Utillity;

public class EnemyProjectileMovement : MonoBehaviour {


    [HideInInspector]
    public GameObject Target;

    public float ProjectileSpeed = 15;
    public float DamageActivationRange = 2;
        
    [HideInInspector]
    public float ProjectileDamage;

    private Vector3 _higher;

    private void Start() {
        _higher = new Vector3(0, 2, 0);
        Invoke("Die", 3);
    }


    private void Update() {
        if (Target) {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position + _higher,
                Time.deltaTime * ProjectileSpeed);
            if (Vector3.Distance(transform.position, Target.transform.position) <= DamageActivationRange) {
                DamageTarget();
            } 
        }
        else {
            Destroy(gameObject);
        }
    }

    private void DamageTarget() {
        if (Target.GetComponent<ITakeDamage>() != null)
        {
            Target.GetComponent<ITakeDamage>().SubstarctDamageFromLife_(ProjectileDamage);
            Destroy(gameObject);    
        }
    }

    private void Die() {
        Destroy(gameObject);
    }
}