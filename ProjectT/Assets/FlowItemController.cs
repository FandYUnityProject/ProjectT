using UnityEngine;
using System.Collections;

public class FlowItemController : MonoBehaviour {

	Vector3 flowMovement;
	Transform juelTransform;
	public float flowTime;
	public float timer;
	bool isUp = false;
	public float speed;
	public float rotationalSpeed;
	
	void Update (){
		MoveFlow ();
	}

	void MoveFlow(){
		if (timer < flowTime && isUp) {
			transform.Translate (new Vector3(0f,0f,1f* Time.deltaTime * speed));
			transform.Rotate (new Vector3 (0f,0f,rotationalSpeed));
			timer += Time.deltaTime;
			if (timer > flowTime) {
				isUp = false;
			}
		} else if (timer >= 0f && !isUp) {
			transform.Translate (new Vector3(0f,0f,-1f* Time.deltaTime * speed));
			transform.Rotate (new Vector3 (0f,0f,rotationalSpeed));
			timer -= Time.deltaTime;
			if(timer < 0f){
				isUp = true;
			}
		}
	}
}
