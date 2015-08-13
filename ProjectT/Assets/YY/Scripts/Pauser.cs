/*
 * Pauser.cs
 * 
 * 説明：アタッチしたオブジェクトをポーズさせることができる。
 * 
 * --- How To Use ---
 * アタッチ：必須：“PauseMST”(GameObject), "TextController"(GameObject)
 *         任意：ポーズさせたいGameObject（主にステータス画面以外） (GameObject)
 * 
 * 制作：2015/08/12  Guttyon
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Pauser : MonoBehaviour {

	static List<Pauser> targets = new List<Pauser>();	// ポーズ対象のスクリプト
	Behaviour[] pauseBehavs = null;	// ポーズ対象のコンポーネント

	// 3D用オブジェクト
	Rigidbody[] rgBodies = null;
	Vector3[] rgBodyVels = null;
	Vector3[] rgBodyAVels = null;

	// 2D用オブジェクト
	/*
	Rigidbody2D[] rg2dBodies = null;
	Vector2[] rg2dBodyVels = null;
	float[] rg2dBodyAVels = null;
	*/
	public static bool isPause;					// ポーズ中かどうか
	public static bool isKeyP        = false;	// "Pキー”を押しているかどうか
	public static bool isNotArrowKey;			// 矢印キーを押しているかどうか
	
	// Use this for initialization
	void Start() {

		// ポーズ対象に追加する
		targets.Add(this);
		
		
		// 最初にtrueにしておくと、1回目の”P”キーで反応しない不具合を回避（原因不明）
		isPause = true;

		isNotArrowKey = true;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.P)) {

			Debug.Log(Input.GetKey(KeyCode.LeftArrow) + "," + Input.GetKey(KeyCode.RightArrow) + "," + Input.GetKey(KeyCode.UpArrow) + "," + Input.GetKey(KeyCode.DownArrow) + ",G:" + PlayerGround.isPlayerGround);

			// 矢印キーを押しているかどうかチェック
			if (!Input.GetKey(KeyCode.LeftArrow)
			    && !Input.GetKey(KeyCode.RightArrow)
			    && !Input.GetKey(KeyCode.UpArrow)
			    && !Input.GetKey(KeyCode.DownArrow))
			{
				isNotArrowKey = true;
			} else {
				isNotArrowKey = false;
			}


			if( !isPause ){
				// ポーズしてないならポーズする
				isPause = true;
				Pause();

			} else {
				if( !isKeyP ){
					// ポーズしているならポーズ解除する
					isPause = false;
					Resume();

				}
			}
			isKeyP = true;
		}

		// キャリブレーション対策
		if (Input.GetKeyUp (KeyCode.P)) {

			// Debug.Log("isPause: " + isPause);
			isKeyP = false;
			// Debug.Log("isKeyP: " +  isKeyP);
		}
	}
	
	// 破棄されるとき（一応）
	void OnDestory() {

		// ポーズ対象から除外する
		targets.Remove(this);
	}
	
	// ポーズされたとき
	void OnPause() {

		// ポーズ対象が無ければ処理終了
		if ( pauseBehavs != null ) {
			return;
		}
		
		// 有効なコンポーネントを取得
		pauseBehavs = Array.FindAll(GetComponentsInChildren<Behaviour>(), (obj) => { return obj.enabled; });
		foreach ( var comp in pauseBehavs ) {
			comp.enabled = false;
		}

		// 3D用オブジェクト
		rgBodies = Array.FindAll(GetComponentsInChildren<Rigidbody>(), (obj) => { return !obj.IsSleeping(); });
		rgBodyVels = new Vector3[rgBodies.Length];
		rgBodyAVels = new Vector3[rgBodies.Length];

		for ( var i = 0 ; i < rgBodies.Length ; ++i ) {
			rgBodyVels[i] = rgBodies[i].velocity;
			rgBodyAVels[i] = rgBodies[i].angularVelocity;
			rgBodies[i].Sleep();
		}

		// 2D用オブジェクト
		/*
		rg2dBodies = Array.FindAll(GetComponentsInChildren<Rigidbody2D>(), (obj) => { return !obj.IsSleeping(); });
		rg2dBodyVels = new Vector2[rg2dBodies.Length];
		rg2dBodyAVels = new float[rg2dBodies.Length];
		
		for ( var i = 0 ; i < rg2dBodies.Length ; ++i ) {
			rg2dBodyVels[i] = rg2dBodies[i].velocity;
			rg2dBodyAVels[i] = rg2dBodies[i].angularVelocity;
			rg2dBodies[i].Sleep();
		}
		*/
	}

	
	// ポーズ解除されたとき
	void OnResume() {

		// ポーズ対象が無ければ処理終了
		if ( pauseBehavs == null ) {
			return;
		}
		
		// ポーズ前の状態にコンポーネントの有効状態を復元
		foreach ( var comp in pauseBehavs ) {
			comp.enabled = true;
		}


		// 3D用オブジェクト
		for ( var i = 0 ; i < rgBodies.Length ; ++i ) {
			rgBodies[i].WakeUp();
			rgBodies[i].velocity = rgBodyVels[i];
			rgBodies[i].angularVelocity = rgBodyAVels[i];
		}

		// 2D用オブジェクト
		/*
		for ( var i = 0 ; i < rg2dBodies.Length ; ++i ) {
			rg2dBodies[i].WakeUp();
			rg2dBodies[i].velocity = rg2dBodyVels[i];
			rg2dBodies[i].angularVelocity = rg2dBodyAVels[i];
		}
		*/
		
		pauseBehavs = null;

		// 3D用オブジェクト
		rgBodies = null;
		rgBodyVels = null;
		rgBodyAVels = null;

		// 2D用オブジェクト
		/*
		rg2dBodies = null;
		rg2dBodyVels = null;
		rg2dBodyAVels = null;
		*/
	}
	
	// ポーズ
	public static void Pause() {

		foreach ( var obj in targets ) {

			// スクリプトを適用したオブジェクト名のデバッグ用
			Debug.Log("Pause: " + obj);

			if( obj.name != "PauseMST" ){
				obj.OnPause();
			}
		}
	}
	
	// ポーズ解除
	public static void Resume() {

		foreach ( var obj in targets ) {

			Debug.Log("Resume: " + obj);
			obj.OnResume();
		}
	}
}