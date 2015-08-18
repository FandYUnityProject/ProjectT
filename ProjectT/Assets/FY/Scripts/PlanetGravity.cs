using UnityEngine;
using System.Collections;

public class PlanetGravity : MonoBehaviour {
	
	public static Vector3 gravityDirection;

	static Vector3 planetPosition;

	void Start(){
		planetPosition = transform.position;
	}

	public static Vector3 GravityCalc(Vector3 targetPos){
		gravityDirection = targetPos - planetPosition;
		return -1*gravityDirection;
	}
}
