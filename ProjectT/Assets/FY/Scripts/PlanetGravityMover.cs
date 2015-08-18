using UnityEngine;
using System.Collections;

public class PlanetGravityMover : MonoBehaviour {

	private Rigidbody rb;
	public Vector3 localGravity;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}

	void Update(){
		AddGravity();
	}

	void OnTriggerStay(Collider coll){
		if (coll.gameObject.tag == "Planet") {
			localGravity = PlanetGravity.GravityCalc(transform.position);
		}
	}

	void AddGravity(){
		rb.AddForce (localGravity);
	}
}
