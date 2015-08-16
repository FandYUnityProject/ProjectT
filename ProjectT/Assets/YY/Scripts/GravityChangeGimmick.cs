using UnityEngine;
using System.Collections;

public class GravityChangeGimmick : MonoBehaviour {

	public Vector3 gravityChangeDirection;	// グラビティの変更
	public Vector3 gravityReturnPlayer;		// 重力が戻る時のプレイヤーの向きを設定
	
	public bool isGravity_PlusX  = false;
	public bool isGravity_MinusX = false;
	public bool isGravity_PlusY  = false;
	public bool isGravity_MinusY = false;
	public bool isGravity_PlusZ  = false;
	public bool isGravity_MinusZ = false;

	public static bool isGravityChange  = false;

	public GameObject gravityReturn;

	// Use this for initialization
	void Start () {

		if( !isGravity_PlusX && !isGravity_MinusX && !isGravity_PlusY && !isGravity_MinusY && !isGravity_PlusZ && !isGravity_MinusZ ){ isGravity_MinusY = true; }

		if (isGravity_PlusX)  { AllFlagIsGravityFalse(); isGravity_PlusX  = true; gravityChangeDirection = new Vector3( 9.81f,  0.00f,  0.00f); }
		if (isGravity_MinusX) { AllFlagIsGravityFalse(); isGravity_MinusX = true; gravityChangeDirection = new Vector3(-9.81f,  0.00f,  0.00f); }
		if (isGravity_PlusY)  { AllFlagIsGravityFalse(); isGravity_PlusY  = true; gravityChangeDirection = new Vector3( 0.00f,  9.81f,  0.00f); }
		if (isGravity_MinusY) { AllFlagIsGravityFalse(); isGravity_MinusY = true; gravityChangeDirection = new Vector3( 0.00f, -9.81f,  0.00f); }
		if (isGravity_PlusZ)  { AllFlagIsGravityFalse(); isGravity_PlusZ  = true; gravityChangeDirection = new Vector3( 0.00f,  0.00f,  9.81f); }
		if (isGravity_MinusZ) { AllFlagIsGravityFalse(); isGravity_MinusZ = true; gravityChangeDirection = new Vector3( 0.00f,  0.00f, -9.81f); }

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider coll){

		if (coll.gameObject.name == "Player") {

			if( !isGravityChange ){

				if (isGravity_PlusX)  { gravityChangeDirection = new Vector3( 9.81f,  0.00f,  0.00f); coll.transform.rotation = Quaternion.Euler (  0.0f, 0.0f,  90.0f); }
				if (isGravity_MinusX) { gravityChangeDirection = new Vector3(-9.81f,  0.00f,  0.00f); coll.transform.rotation = Quaternion.Euler (  0.0f, 0.0f, 270.0f); }
				if (isGravity_PlusY)  { gravityChangeDirection = new Vector3( 0.00f,  9.81f,  0.00f); coll.transform.rotation = Quaternion.Euler (  0.0f, 0.0f, 180.0f); }
				if (isGravity_MinusY) { gravityChangeDirection = new Vector3( 0.00f, -9.81f,  0.00f); coll.transform.rotation = Quaternion.Euler (  0.0f, 0.0f,   0.0f); }
				if (isGravity_PlusZ)  { gravityChangeDirection = new Vector3( 0.00f,  0.00f,  9.81f); coll.transform.rotation = Quaternion.Euler (270.0f, 0.0f,   0.0f); }
				if (isGravity_MinusZ) { gravityChangeDirection = new Vector3( 0.00f,  0.00f, -9.81f); coll.transform.rotation = Quaternion.Euler ( 90.0f, 0.0f,   0.0f); }
			} else {

				gravityChangeDirection = new Vector3( 0.00f, -9.81f,  0.00f);
				coll.transform.rotation = Quaternion.Euler (gravityReturnPlayer);
			}
			
			Physics.gravity = gravityChangeDirection;

			gravityReturn.SetActive(true);
		}
	}

	void OnTriggerExit(Collider coll){

		Debug.Log("Gravity");
		if (coll.gameObject.name == "Player") {
			isGravityChange = !isGravityChange;
		}
	}


	void AllFlagIsGravityFalse(){

		isGravity_PlusX  = false;
		isGravity_MinusX = false;
		isGravity_PlusY  = false;
		isGravity_MinusY = false;
		isGravity_PlusZ  = false;
		isGravity_MinusZ = false;
	}
}
