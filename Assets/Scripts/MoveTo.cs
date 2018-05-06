using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour {

	private NavMeshAgent _agent;
	private bool _timePassed = false;
	private Animation _animation;
	
	// Use this for initialization
	private void Start () {
		_agent = GetComponent<NavMeshAgent>();
		_agent.enabled = false;

		if (GetComponent<Animation>() != null) {
			_animation = GetComponent<Animation>();
			//_animation.Play("walk");
			if (_animation["walk"]) {
				_animation.Play("walk");
			}
			else if (_animation["Walk"]) {
				_animation.Play("Walk");
			}
		}
		
		Invoke("ChangeTime", 1);
	}

	private void Update() {
		if (_animation != null && !_animation.isPlaying) {
			//_animation.Play("walk");
			if (_animation["walk"]) {
				_animation.Play("walk");
			}
			else if (_animation["Walk"]) {
				_animation.Play("Walk");
			}
		}
	}

	private void ChangeTime() {
		//_timePassed = true;
		_agent.enabled = true; // fix teleport
		//toDo change to house!
		_agent.SetDestination(new Vector3(39, 0, 23));
	}
}
