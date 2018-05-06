using System.Collections.Generic;
using Interfaces;
using Spells;
using UnityEngine;
using UnityEngine.AI;
using Utillity;

public class Enemy : MonoBehaviour, IEnemy {

	[SerializeField]
	private float _health;

	[SerializeField]
	private int _experienceGranted;

	public float HitDamage = 2f;

	public float AttackSpeed = 4f; // one attack each 4 seconds

	public float MovementSpeed = 0f;

	private float _originalHealth;

	private List<int> _timeOut;
//	 
//	public Rigidbody spellPrefab;
//	public Transform autoSpellPos;

	// event for Experience
	public delegate void OnDeadAddExperience(int experience, int playerCode);
	public static event OnDeadAddExperience AddExperience;

	// event for Achievement System
	public delegate void OnKillIncrementCounter(int playerCode);
	public static event OnKillIncrementCounter IncrementCounter;
	

	private void Start() {
		_originalHealth = _health;
		GetComponent<NavMeshAgent>().speed += Random.Range(0.1f, 1.2f) + MovementSpeed;
		_timeOut = new List<int>();
		InvokeRepeating("ClearTimeOutList", 1, 3);
	}

	private void OnCollisionEnter(Collision other) {
		
		//Debug.Log(other.gameObject.GetComponent<Spell>().CastOrigin.GetHashCode());

		// prevent same spell to damage the same enemy multiple times
		// this can happen with sphere and capsule colliders
		// and how collision is calculated on entry.
		var hasCode = other.GetHashCode();
		if (!_timeOut.Contains(hasCode)) {
			var spell = other.gameObject.GetComponent<Spell>();
			var auto = other.gameObject.GetComponent<AttackProjectile>();
			var damage = 1;
			
			if (spell) {
				damage = spell.Damage;
			}
			else if (auto) {
				damage = auto.Damage;
			}
			_timeOut.Add(hasCode);

//			var origin = spell
//				? other.gameObject.GetComponent<Spell>().CastOrigin.GetHashCode()
//				: other.gameObject.GetComponent<AttackProjectile>().Origin.GetHashCode();

			var origin = 0;
			if (spell) {
				origin = other.gameObject.GetComponent<Spell>().CastOrigin.GetHashCode();
			}
			else {
				if (other.gameObject.GetComponent<AttackProjectile>() != null) {
					origin = other.gameObject.GetComponent<AttackProjectile>().Origin.GetHashCode();	
				}
			}
			
			SubstarctDamageFromLife_(damage, origin);
			if (auto) {
				Destroy(other.gameObject);
			}
		}
	}

	
//	private void OnTriggerEnter(Collider other)
//	{
//		// toDo - Layer issue ( not meant to be 11 but logs 11 on player ) 
//		if (other.gameObject.layer != 11) return;
//		
//		Rigidbody rocketInstance;
//		rocketInstance = Instantiate(spellPrefab, autoSpellPos.position, autoSpellPos.rotation) as Rigidbody;
//		rocketInstance.AddForce(autoSpellPos.forward * 300);
//		
//	}


	public void SubstarctDamageFromLife_(float damageTaken, int playerCode) {
		_health -= damageTaken;
		if (_health <= 0) {
			if (AddExperience != null) AddExperience(_experienceGranted, playerCode);
			if (IncrementCounter != null) IncrementCounter(playerCode);
			Destroy(gameObject);
		}
		GetComponent<HealthBar>().ShrinkBar((int) damageTaken, _originalHealth);
	}

	private void ClearTimeOutList() {
		_timeOut.Clear();
	}
}
