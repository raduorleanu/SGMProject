using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormCallParticleCollision : MonoBehaviour {

	void OnParticleCollision(GameObject other) {
		Debug.Log(other.gameObject.tag);
	}

	private void OnCollisionEnter(Collision other) {
		Debug.Log("Coolsion message from particle");
	}
}
