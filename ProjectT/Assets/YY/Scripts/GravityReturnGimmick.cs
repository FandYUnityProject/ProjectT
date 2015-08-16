using UnityEngine;
using System.Collections;

public class GravityReturnGimmick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider coll){
		if (coll.gameObject.name == "Player") {
			coll.transform.rotation = Quaternion.Euler (0.0f, coll.transform.rotation.y, 0.0f);
			Physics.gravity = new Vector3( 0.00f, -9.81f,  0.00f);
			GravityChangeGimmick.isGravityChange = false;
			this.gameObject.SetActive(false);
		}
	}
}
