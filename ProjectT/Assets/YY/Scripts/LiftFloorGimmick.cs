/*
 * LiftFloorGimmick.cs
 * 
 * 説明：リフトの床に接触した時の処理
 * 
 * --- How To Use ---
 * アタッチ：LiftFloor_XX(gameObject)
 * Inspector：【LiftParent】LiftGimmick(gameObject)
 *
 * 制作：2015/08/15  Guttyon
*/

using UnityEngine;
using System.Collections;

public class LiftFloorGimmick : MonoBehaviour {
	
	private GameObject cameraObject;	// カメラオブジェクト
	private GameObject playerObject;	// プレイヤーオブジェクト

	// Unityちゃんのデフォルト移動スピード
	private float defaultForwardSpeed  = 7.0f;
	private float defaultBackwardSpeed = 2.0f;

	public GameObject liftParent;

	// Use this for initialization
	void Start () {
		
		// カメラオブジェクトを取得
		cameraObject = GameObject.Find ("Main Camera");
		
		// プレイヤーオブジェクトを取得
		playerObject = GameObject.Find ("Player");

		// Unityちゃんのデフォルト移動スピード
		// float defaultForwardSpeed  = UnityChanControlScriptWithRgidBody.forwardSpeed;
		// float defaultBackwardSpeed = UnityChanControlScriptWithRgidBody.backwardSpeed;

		if (this.transform.localScale.x != this.transform.localScale.z) {

			transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.x);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other) {

		// カメラを回転リフトの子オブジェクトに設定することで、画面のブレを防ぐ
		// プレイヤーを回転リフトの子オブジェクトに設定することで、擬似的に慣性を作る
		if (other.gameObject.name == "Player") {

			// リフトに載ってる間、リフトの大きさに応じて移動速度が変化してしまうので、スピード調整
			UnityChanControlScriptWithRgidBody.forwardSpeed  = 2.0f / ( liftParent.transform.localScale.x * 2.3f );
			UnityChanControlScriptWithRgidBody.backwardSpeed = 0.6f / ( liftParent.transform.localScale.x * 2.3f );

			// カメラを回転リフトの子オブジェクトに設定することで、画面のブレを防ぐ
			// プレイヤーを回転リフトの子オブジェクトに設定することで、擬似的に慣性を作る
			cameraObject.transform.parent = this.gameObject.transform;
			playerObject.transform.parent = this.gameObject.transform;
		}
	}

	void OnCollisionExit(Collision other) {

		// カメラとプレイヤーの子オブジェクト化を解除する
		if (other.gameObject.name == "Player") {

			// 移動スピードを戻す
			UnityChanControlScriptWithRgidBody.forwardSpeed  = defaultForwardSpeed;
			UnityChanControlScriptWithRgidBody.backwardSpeed = defaultBackwardSpeed;

			// カメラとプレイヤーの子オブジェクト化を解除する
			cameraObject.transform.parent = null;
			playerObject.transform.parent = null;
		}
	}
}
