/*
 * RotateGimick.cs
 * 
 * 説明：回転するギミックのアニメーションと、回転する間に遠心力を働かせる
 * 
 * --- How To Use ---
 * アタッチ：回転させるオブジェクト(gameObject)
 * Inspector：【RotateMotionTime】回転する時間
 *            【RotateSpeed】回転するスピード
 *            【moveMotionTime】最初の挙動の移動時間
 *            【MoveSpeed】最初の挙動の移動スピード
 *            【FirstDelay】アニメーションスタート前の停止時間
 *            【addForceX,Y,X】それぞれの軸の遠心力
 *
 * 制作：2015/08/13  Guttyon
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotateGimmick : MonoBehaviour {

	// 回転する角度、時間、スピードの設定
	private int   rotateTo          = 0;
	public  float rotateMotionTime  = 2.0f;
	public  float rotateSpeed       = 5.0f;
	
	// 最初の挙動の移動量、時間、スピードの設定
	private float moveScale      = 0.2f;
	public  float moveMotionTime = 2.0f;
	public  float moveSpeed      = 10.0f;

	// アニメーションスタート前の停止時間
	public float firstDelay     = 1.0f;

	// 遠心力処理のON/OFFと遠心力の設定
	private bool isAddForceStart = false;
	public float addForceX = 0.0f;
	public float addForceY = 0.0f;
	public float addForceZ = 2500.0f;

	// Use this for initialization
	void Start () {

		// 回転スピードに応じて遠心力アップ
		addForceX = addForceX / rotateMotionTime;
		addForceY = addForceY / rotateMotionTime;
		addForceZ = addForceZ / rotateMotionTime;

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

		// 遠心力無効
		isAddForceStart = false;

		// 初めの上下に動く挙動が終わった後、遠心力を有効にする
		// (Unityでは、動くオブジェクトの上に別のオブジェクトを乗せても、上のオブジェクトが動かない)
		iTween.MoveTo (gameObject, iTween.Hash ( "y", transform.position.y + moveScale, "delay", firstDelay,                                "time",  moveMotionTime / moveSpeed, "easetype", iTween.EaseType.linear));
		iTween.MoveTo (gameObject, iTween.Hash ( "y", transform.position.y            , "delay", firstDelay + (moveMotionTime / moveSpeed), "time", moveMotionTime / moveSpeed, "easetype", iTween.EaseType.linear));

		// 回転角度を更新
		rotateTo += 90;
		if( rotateTo >=360 ) { rotateTo = 0; }

		//if( rotateTo >=  )

		// 回転後、アニメーションを繰り返す
		iTween.RotateTo(gameObject, iTween.Hash("x", rotateTo, "delay", firstDelay + (moveMotionTime / moveSpeed) * 5, "time", rotateMotionTime / rotateSpeed, "easetype", iTween.EaseType.linear, "onstart", "AddForceStart", "oncomplete", "MoveRotate"));
	}

	void AddForceStart(){

		// 遠心力を有効にする
		// Debug.Log ("AddForceStart");
		isAddForceStart = true;
	}


	void OnCollisionStay (Collision coll){

		if (coll.gameObject.name == "Player") {

			if ( isAddForceStart ){

				// 遠心力の実行
				// Debug.Log ("AddForce");
				coll.transform.GetComponent<Rigidbody>().AddForce(addForceX, addForceY, addForceZ);
			}
		}
	}

}
