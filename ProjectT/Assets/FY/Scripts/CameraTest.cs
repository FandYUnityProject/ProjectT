using UnityEngine;
using System.Collections;

public class CameraTest : MonoBehaviour {

	Vector3 playerOffset;
	Vector3 planetOffset;
	Vector3 viewPoint;
	GameObject player;
	GameObject planet;
	Transform target;
	static float angle = 0f;//= Vector3.Angle (playerOffset, target.position);
	static float radius = 15f;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		planet = GameObject.FindGameObjectWithTag ("Planet");
		target= player.transform;
		playerOffset = transform.position - player.transform.position;
		planetOffset = transform.position - planet.transform.position;
	}

	void FixedUpdate (){

		//Debug.Log (angle);
		//transform.position = player.transform.position + planetOffset;
		//transform.LookAt (planet.transform);
	
		//transform.rotation = Quaternion.AngleAxis (angle,Vector3.right);
	}
}
