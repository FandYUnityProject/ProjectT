/*
 * 移動( 3Dゲーム用 )
 * プログラム適用時、適用するオブジェクトに”Rididbody”, ""CupsleColliderを適用させる必要がある。
 * 2015/07/26 Sun - Guttyon
*/

using UnityEngine;
using System.Collections;

public class move3D : MonoBehaviour {
		
	public float lookSmoother = 3.0f;			// a smoothing setting for camera motion
	public bool   useCurves = true;				// Mecanimでカーブ調整を使うか設定する
	// このスイッチが入っていないとカーブは使われない
	public float useCurvesHeight = 0.5f;		// カーブ補正の有効高さ（地面をすり抜けやすい時には大きくする）
		
	// 以下キャラクターコントローラ用パラメタ
	// 前進速度
	public float forwardSpeed = 7.0f;
	// 後退速度
	public float backwardSpeed = 2.0f;
	// 旋回速度
	public float rotateSpeed = 2.0f;
	// ジャンプ威力
	public float jumpPower = 3.0f; 
	// キャラクターコントローラ（カプセルコライダ）の参照
	private CapsuleCollider col;
	private Rigidbody rb;
	// キャラクターコントローラ（カプセルコライダ）の移動量
	private Vector3 velocity;
	// CapsuleColliderで設定されているコライダのHeiht、Centerの初期値を収める変数
	private float orgColHight;
	private Vector3 orgVectColCenter;
		
	private GameObject cameraObject;	// メインカメラへの参照
		
	// 初期化
	void Start ()
	{
		// CapsuleColliderコンポーネントを取得する（カプセル型コリジョン）
		col = GetComponent<CapsuleCollider> ();
		rb  = GetComponent<Rigidbody> ();
		//メインカメラを取得する
		cameraObject = GameObject.FindWithTag ("MainCamera");
		// CapsuleColliderコンポーネントのHeight、Centerの初期値を保存する
		orgColHight = col.height;
		orgVectColCenter = col.center;
	}
		
	// 以下、メイン処理.リジッドボディと絡めるので、FixedUpdate内で処理を行う.
	void FixedUpdate ()
	{
		float h = Input.GetAxis ("Horizontal");				// 入力デバイスの水平軸をhで定義
		float v = Input.GetAxis ("Vertical");				// 入力デバイスの垂直軸をvで定義
		rb.useGravity = true;//ジャンプ中に重力を切るので、それ以外は重力の影響を受けるようにする
			
		// 以下、キャラクターの移動処理
		velocity = new Vector3 (0, 0, v);		// 上下のキー入力からZ軸方向の移動量を取得
		// キャラクターのローカル空間での方向に変換
		velocity = transform.TransformDirection (velocity);
		//以下のvの閾値は、Mecanim側のトランジションと一緒に調整する
		if (v > 0.1) {
			velocity *= forwardSpeed;		// 移動速度を掛ける
		} else if (v < -0.1) {
			velocity *= backwardSpeed;	// 移動速度を掛ける
		}
			
		// 上下のキー入力でキャラクターを移動させる
		transform.localPosition += velocity * Time.fixedDeltaTime;
			
		// 左右のキー入力でキャラクタをY軸で旋回させる
		transform.Rotate (0, h * rotateSpeed, 0);
	}
}