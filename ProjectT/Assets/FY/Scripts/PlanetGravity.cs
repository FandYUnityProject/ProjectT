using UnityEngine;
using System.Collections;

public class PlanetGravity : MonoBehaviour {
	
	private Vector3 gravityDirection;
	private Vector3 planetPosition;
	GameObject player;
	bool isNear = false;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		planetPosition = this.transform.position;

	}

	void FixedUpdate(){
		gravityDirection = -1*(PlanetGravityMover.playerPosittion - planetPosition);
		if (isNear) {
			PlanetGravityMover.AddGravity(gravityDirection);
		}
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Player") {
			isNear = true;
		}
	}

	void OnTriggerExit(Collider coll){
		if (coll.gameObject.tag == "Player") {
			isNear = false;
		}
	}
}
