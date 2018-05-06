using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRotate : MonoBehaviour {

	[SerializeField]
	private Vector3 _direction;
	
	[SerializeField]
	private float _speed;

	private void Update() {
		transform.Rotate(_direction * Time.deltaTime * _speed);
	}
}
