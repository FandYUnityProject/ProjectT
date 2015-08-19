using UnityEngine;
using System.Collections;

public class PlanetGravityMover : MonoBehaviour {

	public static Rigidbody rb;
	public static Vector3 playerPosittion;
	public static float strongness;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		strongness = rb.mass;
	}

	void FixedUpdate(){
		playerPosittion = transform.position;
	}

	public static void AddGravity(Vector3 localGravity){
		rb.AddForce (localGravity * strongness);
		//Debug.Log (localGravity);
	}
}
