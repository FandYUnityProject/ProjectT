using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotateGimmick : MonoBehaviour {

	public int   rotateTo          = 0;
	public float rotateMotionTime  = 2.0f;
	public float rotateSpeed       = 2.0f;

	public float moveScale      = 0.2f;
	public float moveMotionTime = 0.1f;
	public float moveSpeed      = 10.0f;
	public float firstDelay     = 1.0f;

	public bool  isAddForceStart = false;
	public float addFourceX = 0.0f;
	public float addFourceY = 0.0f;
	public float addFourceZ = 2500.0f;

	// Use this for initialization
	void Start () {

		// 回転スピードに応じて遠心力アップ
		addFourceX = addFourceX / rotateMotionTime;
		addFourceY = addFourceY / rotateMotionTime;
		addFourceZ = addFourceZ / rotateMotionTime;

		// スピード調整
		if (moveSpeed  <= 5.0f ) { moveSpeed  = 5.0f;  }
		if (moveSpeed  >= 30.0f) { moveSpeed  = 30.0f; }
		if (firstDelay <= 0.05f) { firstDelay = 0.05f; }
		if (firstDelay <= 2.0f ) { firstDelay = 2.0f;  }

		MoveRotate ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void MoveRotate(){

		isAddForceStart = false;

		iTween.MoveTo (gameObject, iTween.Hash ( "y", transform.position.y + moveScale, "delay", firstDelay,                                "time",  moveMotionTime / moveSpeed, "easetype", iTween.EaseType.linear));
		iTween.MoveTo (gameObject, iTween.Hash ( "y", transform.position.y            , "delay", firstDelay + (moveMotionTime / moveSpeed), "time", moveMotionTime / moveSpeed, "easetype", iTween.EaseType.linear));

		// 回転角度を更新
		rotateTo += 90;
		if( rotateTo >=360 ) { rotateTo = 0; }

		iTween.RotateTo(gameObject, iTween.Hash("x", rotateTo, "delay", firstDelay + (moveMotionTime / moveSpeed) * 5, "time", rotateMotionTime / rotateSpeed, "easetype", iTween.EaseType.linear, "onstart", "AddForceStart", "oncomplete", "MoveRotate"));
	}

	void AddForceStart(){
		
		Debug.Log ("AddForceStart");
		isAddForceStart = true;
	}


	void OnCollisionStay (Collision coll){

		if (coll.gameObject.name == "Player") {

			if ( isAddForceStart ){
				
				Debug.Log ("AddForce");
				coll.transform.GetComponent<Rigidbody>().AddForce(addFourceX, addFourceY, addFourceZ);
			}
		}
	}

}
