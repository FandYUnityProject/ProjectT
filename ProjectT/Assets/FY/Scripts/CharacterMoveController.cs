using UnityEngine;
using System.Collections;

public class CharacterMoveController : MonoBehaviour {

	float h;
	float v;
	Vector3 movement;
	public float speed=2;
	public float jumpPower = 1;
	public float rotateSpeed = 1;

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
		movement = transform.TransformDirection (movement);

		if (v > 0.1) {
			movement *= 7f;		// 移動速度を掛ける
		} else if (v < -0.1) {
			movement *= 2f;	// 移動速度を掛ける
		}

		if(Input.GetButtonDown("Jump")){
			rb.AddForce(Vector3.up * jumpPower);//, ForceMode.VelocityChange);
		}

		transform.localPosition += movement * Time.fixedDeltaTime;
		transform.Rotate (0, h * rotateSpeed, 0);

	}
}