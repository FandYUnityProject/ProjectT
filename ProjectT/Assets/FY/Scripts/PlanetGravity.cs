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
		Debug.Log (planetPosition);
	}

	void FixedUpdate(){
		gravityDirection = -1*(PlanetGravityMover.playerPosittion - planetPosition);
		if (isNear) {
			Debug.Log(gravityDirection);
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
