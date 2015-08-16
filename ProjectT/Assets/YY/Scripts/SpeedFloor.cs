/*
 * SleedFloor.cs
 * 
 * 説明：乗ることで加速/原則する床。
 * 
 * --- How To Use ---
 * アタッチ：SpeedFloorGimmick(gameObject)
 * Inspector：【addForceX,Y,X】それぞれの軸の加速力
 *            【MaterialOffsetSpeed】矢印のアニメーション（テクスチャのオフセット）のスピードを処理
 *            【isUpSpeed】アップスピードかダウンスピードか管理。床のアニメーションの動きを反対にする
 * 
 * 制作：2015/08/16  Guttyon
*/

using UnityEngine;
using System.Collections;

public class SpeedFloor : MonoBehaviour {
	
	public float addForceX = 0.0f;
	public float addForceY = 0.0f;
	public float addForceZ = 500.0f;
	
	public float addedForceX = 0.0f;
	public float addedForceY = 0.0f;
	public float addedForceZ = 0.0f;

	public  float materialOffsetSpeed = 0.05f;	// スクロールするスピード
	private float scrollOffset = 0.0f;			// オフセットのスクロール具合

	public  bool  isUpSpeed = true;	//アップスピードかダウンスピードか

	// Use this for initialization
	void Start () {
	
		if (isUpSpeed) {
			materialOffsetSpeed *= -1; 
		}
	}
	
	// Update is called once per frame
	void Update () {

		scrollOffset = scrollOffset + materialOffsetSpeed;
		Vector2 offset = new Vector2 (0, scrollOffset);									// Xの値がずれていくオフセットを作成。
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", offset);	// マテリアルにオフセットを設定する。
	
	}

	void OnCollisionStay (Collision coll){

		if (coll.gameObject.name == "Player") {
			
			addedForceX = addForceX * Time.deltaTime;
			addedForceY = addForceY * Time.deltaTime;
			addedForceZ = addForceZ * Time.deltaTime;

			coll.transform.GetComponent<Rigidbody> ().AddForce (addForceX, addForceY, addForceZ);
		}
	}

	void OnCollisionExit (Collision coll){

		//if( this.gameObject.name == "SpeedFloorUpY" ) { coll.transform.Rotate ( -270.0f, 0.0f, 0.0f); new Vector3( 0.00f, -9.81f,  0.00f);}
		
		addedForceX = 0.0f;
		addedForceY = 0.0f;
		addedForceZ = 0.0f;
	}
}
