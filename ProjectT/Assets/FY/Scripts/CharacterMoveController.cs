using UnityEngine;
using System.Collections;

public class CharacterMoveController : MonoBehaviour {

	float h;
	float v;
	Vector3 movement;
	public float speed=2;

	Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}

	void Update (){
		Move ();
	}

	void Move(){
		h = Input.GetAxis ("Horizontal");
		v = Input.GetAxis ("Vertical");
		movement = new Vector3(h,0f,v);
		rb.AddForce (movement * speed);
	}
}
