using UnityEngine;
using System.Collections;

public class SkyBoxRotateChange : MonoBehaviour {
	
	public Material SkyBox;
	
	public float duration;	 // Skyboxを徐々に変化させる時間
	public float skyRotate ; // Skyboxを回転させる早さ
	
	public float smooth  = 1.5f;	// カメラモーションのスムーズ化用変数
	public Transform skyCamPosNormal;	// the usual position for the camera, specified by a transform in the game
	public Transform skyCamPosUp;		// Jump Camera locater
	
	// スムーズに繋がない時（クイック切り替え）用のブーリアンフラグ
	bool bQuickSwitch = false;	//Change Camera Position Quickly
	
	// Use this for initialization
	void Start () {
	
		// スカイボックスをセットし、shaderのBlendの値を0(朝）に設定。
		RenderSettings.skybox = SkyBox;
		SkyBox.SetFloat("_Blend", 0.0f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate ()	// このカメラ切り替えはFixedUpdate()内でないと正常に動かない
	{
		//Alt
		if(Input.GetButton("Fire2")){	
			// Change Jump Camera
			setCameraPositionJumpView();
		} else {	
			// return the camera to standard position and direction
			setCameraPositionNormalView();
		}

		
		// スカイボックスをを回転させる
		skyCamPosNormal.Rotate (0.0f, skyRotate, 0.0f);
		skyCamPosUp.Rotate (0.0f, skyRotate, 0.0f);

		/*
		if (Input.GetButtonDown ("Fire2")) {
			isAltKey = true;
			iTween.RotateTo (gameObject, iTween.Hash ("x", -60.0f, "time", smooth));
		}
		
		if (Input.GetButtonUp ("Fire2")) {
			isAltKey = false;
			iTween.RotateTo (gameObject, iTween.Hash ("x",  0.0f, "time", smooth));
		}
		*/
	}

	void setCameraPositionNormalView()
	{
		if(bQuickSwitch == false){
			// the camera to standard position and direction
			transform.position = Vector3.Lerp(transform.position, skyCamPosNormal.position, Time.fixedDeltaTime * smooth);	
			transform.forward = Vector3.Lerp(transform.forward, skyCamPosNormal.forward, Time.fixedDeltaTime * smooth);
		}
		else{
			// the camera to standard position and direction / Quick Change
			transform.position = skyCamPosNormal.position;	
			transform.forward = skyCamPosNormal.forward;
			bQuickSwitch = false;
		}
	}
	
	void setCameraPositionJumpView()
	{
		// Change Jump Camera
		bQuickSwitch = false;
		transform.position = Vector3.Lerp(transform.position, skyCamPosUp.position, Time.fixedDeltaTime * smooth);	
		transform.forward = Vector3.Lerp(transform.forward, skyCamPosUp.forward, Time.fixedDeltaTime * smooth);		
	}



	private void UpdateSky(float value)
	{
		SkyBox.SetFloat("_Blend", value);
	}


	void OnTriggerEnter(Collider coll){

		if (coll.gameObject.name == "Player") {

			// 朝から夜に変化
			iTween.ValueTo(gameObject, iTween.Hash("from", 0.0f, "to", 1.0f, "time", duration, "onupdate", "UpdateSky"));
		}
	}

	void OnTriggerExit(Collider coll){
		
		if (coll.gameObject.name == "Player") {

			// 夜から朝に変化
			iTween.ValueTo(gameObject, iTween.Hash("from", 1.0f, "to", 0.0f, "time", duration, "onupdate", "UpdateSky"));
		}
	}
}
