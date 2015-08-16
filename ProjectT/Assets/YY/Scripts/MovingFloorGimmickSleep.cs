/*
 * MovingFloorGimmickSleep.cs
 * 
 * 説明：・移動する床(乗るまで移動しないver)の処理
 *		・Playerが乗るまで乗り物が動かず、眠っている。
 *		・Playerが乗ると、乗り物の表情が変わり、指定先の往路を辿る。
 *		・移動が終わると、再び眠る。
 *		・もう一度乗ると、復路を辿る。
 * 
 * --- How To Use ---
 * アタッチ：MovingFloorGimmick(gameObject)
 * Inspector：【PropellerObj】MovingGimmick_Propeller(gameObject)
 *            【PropellerRotateSpeed】プロペラの回転スピード
 *            【FaceObj】MovingGimmick_Face(gameObject)
 *            【EyeObj】MovingGimmick_Eye(gameObject)
 *            【PaulObj】MovingGimmick_Paul(gameObject)
 *            【FacePaulMaterials】顔とポールのマテリアル格納
 *            【EyeMaterials】目のマテリアル格納
 *            【MaterialIndex】格納したマテリアルの番号
 *
 * 制作：2015/08/15  Guttyon
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingFloorGimmickSleep : MonoBehaviour {
	
	public Vector3 moveSpeed    = Vector3.zero; // 移動スピード
	public Vector3 moveDistance = Vector3.zero; // 移動距離
	
	private Vector3 moved            = Vector3.zero;            //移動した距離を保持
	private List<GameObject> rideObj = new List<GameObject>();  //床に乗ってるオブジェクト
	
	private GameObject cameraObject;			// カメラオブジェクト
	
	public GameObject propellerObj;				// プロペラのオブジェクト
	public float propellerRotateSpeed = 0.0f;	// プロペラの回転スピード
	
	// moveDistanceまで動いたら折り返すためにフラグを立てる
	private bool isTurn = false;
	private bool isMove = false;
	
	// マテリアル変更
	public GameObject faceObj;		// 顔
	public GameObject eyeObj;		// 目
	public GameObject paulObj;		// ポール
	
	public Material[] facePaulMaterials;	// マテリアル格納
	public Material[] eyeMaterials;			// マテリアル格納
	public int material_index = 0;			// マテリアル番号
	
	
	// Use this for initialization
	void Start () {
		
		// カメラオブジェクトを取得
		cameraObject = GameObject.Find ("Main Camera");
		
		// マテリアルを配置
		material_index = 0;
		faceObj.GetComponent<Renderer>().material = facePaulMaterials[material_index];
		paulObj.GetComponent<Renderer>().material = facePaulMaterials[material_index];
		eyeObj.GetComponent<Renderer>().material  = eyeMaterials[material_index];
	}
	
	// Update is called once per frame
	void Update() {

		if (isMove) {
		
			// 床を動かす
			float x = moveSpeed.x;
			float y = moveSpeed.y;
			float z = moveSpeed.z;
			if (moved.x >= moveDistance.x)
				x = 0;
			else if (moved.x + moveSpeed.x > moveDistance.x)
				x = moveDistance.x - moved.x;
			if (moved.y >= moveDistance.y)
				y = 0;
			else if (moved.y + moveSpeed.y > moveDistance.y)
				y = moveDistance.y - moved.y;
			if (moved.z >= moveDistance.z)
				z = 0;
			else if (moved.z + moveSpeed.z > moveDistance.z)
				z = moveDistance.z - moved.z;
			transform.Translate (x, y, z);
			
			//動いた距離を保存
			moved.x += Mathf.Abs (moveSpeed.x);
			moved.y += Mathf.Abs (moveSpeed.y);
			moved.z += Mathf.Abs (moveSpeed.z);
			
			//床の上のオブジェクトを床と連動して動かす
			foreach (GameObject gameObj in rideObj) {
				Vector3 v = gameObj.transform.position;
				gameObj.transform.position = new Vector3 (v.x + x, v.y + y, v.z + z);
			}
			
			//折り返すか？
			if (moved.x >= moveDistance.x && moved.y >= moveDistance.y && moved.z >= moveDistance.z) {

				isMove = false;
				MoveFloorGimmickSleep();	// 動く床を眠っているテクスチャに戻す
				moveSpeed *= -1; //逆方向へ動かす
				moved = Vector3.zero;
			}
		}

		// プロペラを回す
		propellerObj.transform.Rotate (new Vector3 (0, propellerRotateSpeed, 0), Space.World);
	}
	
	void OnCollisionEnter(Collision other) {
		
		Debug.Log (other.gameObject);
		
		//床の上に乗ったオブジェクトを保存
		rideObj.Add(other.gameObject);
		
		// プロペラのスピードを上げる
		// カメラを床ブロックの子オブジェクトに設定することで、画面のブレを防ぐ
		if (other.gameObject.name == "Player") {

			// 動作開始
			isMove = true;
			
			// プロペラの回転スピードを早くする
			MovePropeller(20.0f);
			cameraObject.transform.parent = this.gameObject.transform;
			
			// マテリアル変更
			material_index = 1;
			faceObj.GetComponent<Renderer>().material = facePaulMaterials[material_index];
			paulObj.GetComponent<Renderer>().material = facePaulMaterials[material_index];
			eyeObj.GetComponent<Renderer>().material  = eyeMaterials[material_index];
		}
	}
	
	void OnCollisionExit(Collision other) {
		
		//床から離れたので削除
		rideObj.Remove(other.gameObject);
		
		// プロペラのスピードを元に戻し、カメラの子オブジェクト化を解除する
		if (other.gameObject.name == "Player") {

			cameraObject.transform.parent = null;

		}
	}

	// 動く床を眠っているテクスチャに戻す
	void MoveFloorGimmickSleep(){
		
		// プロペラの回転スピードを元に戻す
		MovePropeller(0.0f);
		
		// マテリアル変更
		material_index = 0;
		faceObj.GetComponent<Renderer>().material = facePaulMaterials[material_index];
		paulObj.GetComponent<Renderer>().material = facePaulMaterials[material_index];
		eyeObj.GetComponent<Renderer>().material  = eyeMaterials[material_index];
	}

	void MovePropeller(float movePropellerRotateSpeed){

		// プロペラを徐々に動かす/停止させる
		iTween.ValueTo(gameObject, iTween.Hash("from", propellerRotateSpeed, "to", movePropellerRotateSpeed, "time", 1.5f, "onupdate", "UpdateHandler"));
	}

	void UpdateHandler(float value)
	{
		// プロペラの速度を反映
		propellerRotateSpeed = value;
	}
}
