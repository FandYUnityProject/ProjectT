/*
 * CharacterText.cs
 * 
 * 説明：外部スクリプト”CharacterText.cs”にInspectorで設定した会話内容を送る。
 *      設定には”Size: 表示させるテキストの行数(ウィンドウ内に収まる文字列で1行とする), ElementXX: 表示する文字列” がある。
 *      キャラクター毎にアタッチすれば、キャラクター毎に異なるメッセージが表示可能。
 *      アタッチしたキャラクターのTriggerにプレイヤーが触れ、クリック(Enter)することで会話ウィンドウが表示される
 * 
 * 【注意】：アタッチしたキャラクターのObject内に”MessageIcon(+MessageIcon.cs)”をセットすること！
 * 【！】会話中にPlayer(Unity-chan)を動かさない場合は、UnityChanControlScriptWithRgidBody.cs内にある
 *       “void FixedUpdate”関数を全てif(CharacterText.isTextEnd){}でくくる。
 * 
 * --- How To Use ---
 * アタッチ：会話メッセージをするキャラクター (GameObject)
 * Inspector：【TextControllerClass】TextController(GameObject)をセット
 *            【Scenarios > Size】表示させるテキストの行数(ウィンドウ内に収まる文字列で1行とする)
 *            【Scenarios > ElementXX】各行の表示する文字列
 *            【TextCanvas】TextCanvas(GameObject)をセット
 *            【TextPanel】TextPanel(GameObject)をセット
 *            【intervalForCharacterTextSpeed】1文字の表示にかかる時間
 * 
 * 制作：2015/08/11  Guttyon
*/

using UnityEngine;
using System.Collections;

public class CharacterText : MonoBehaviour {

	public  TextController 	 textControllerClass;	// textControllerのClass

	public  string[] scenarios;		// シナリオを格納する
	private int currentLine = 0;	// 現在の行番号
	
	public  GameObject textCanvas;	// uGUIのテキストキャンバス
	public  GameObject textPanel;	// uGUIのテキストPanel
	private GameObject messageIcon;	// 会話のアイコン
	private GameObject playerObj;	// Playerのオブジェクト
	
	private bool isTextStart     = false;	// 会話を開始したか
	public static bool isTextEnd = true;	// 会話が終了したか
	
	[SerializeField] [Range(0.001f, 0.3f)]
	float intervalForCharacterTextSpeed = 0.05f;	// 1文字の表示にかかる時間

	// Use this for initialization
	void Start () {

		// 会話開始時に”TextCanvas”を表示する
		//textCanvas  = GameObject.Find ("TextCanvas");
		//textPanel   = GameObject.Find ("TextPanel");
		messageIcon = GameObject.Find ("MessageIcon");

		// 会話アイコンの大きさを0にする
		messageIcon.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);

		// Playerのオブジェクトを取得
		playerObj = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
		// トリガー（会話可能範囲）に接触し、マウスクリック（Enter)を押した時の処理
		if(isTextStart){

			if (currentLine < scenarios.Length && (Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Return))) {

				// クリックもしくはEnterキーで会話開始
				if (currentLine == 0) {

					// 会話を開始したら、吹き出しアイコンを小さくするアニメーションを開始する
					iTween.ScaleTo (messageIcon, iTween.Hash ("scale", new Vector3 (0.0f, 0.0f, 1.0f), "time", 0.3f));

					// 会話終了フラグを下ろす
					isTextEnd = false;

					// テキストウィンドウの大きさを初期化し、アニメーションスタート
					textPanel.transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
					iTween.ScaleTo(textPanel, iTween.Hash("scale", new Vector3(1.0f, 1.0f, 1.0f), "time", 0.3f));

					// 会話時にプレイヤーが会話相手を向くようにする
					Vector3 playerLookAtTargetPos  = this.transform.position;
					playerLookAtTargetPos.y = playerObj.transform.position.y;
					playerObj.transform.LookAt(playerLookAtTargetPos);

					// TextControllerのStartScenarios関数を実行
					// (表示するテキスト（Inspectorで設定）, テキスト表示スピード(float 目安: 【早】0.03f〜0.12f【遅】))
					textCanvas.SetActive (true);
					textControllerClass.StartScenarios (scenarios, intervalForCharacterTextSpeed);
					Debug.Log(this.name + ": 会話開始");
				}	
				currentLine ++;
			} else if (isTextEnd) {

				// 会話が終了したら吹き出しアイコンを再表示する
				iTween.ScaleTo (messageIcon, iTween.Hash ("scale", new Vector3 (0.2f, 0.2f, 1.0f), "time", 0.3f));

				// 会話が終了したら行数番号を0に戻す
				currentLine = 0;
			}
		}
	}
	
	// トリガー（会話可能範囲）に接触した瞬間の処理
	void OnTriggerEnter(Collider coll){

		if (coll.tag == "Player") {

			// 会話可能範囲内に入ったら、吹き出しアイコンの位置を調整し、吹き出しアイコン（会話可能を表すUI）を表示する
			messageIcon.transform.position = new Vector3 (this.transform.position.x
			                                             , this.transform.position.y + 1.0f
			                                              , this.transform.position.z);
			iTween.ScaleTo (messageIcon, iTween.Hash ("scale", new Vector3 (0.2f, 0.2f, 1.0f), "time", 0.3f));
		}
	}


	// トリガー（会話可能範囲）に接触している間の処理
	void OnTriggerStay(Collider coll){

		if (coll.tag == "Player") {

			// 会話開始の許可を行う
			if( Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Return)){
				isTextStart = true;
			}
		}
	}

	// トリガー（会話可能範囲）から出たら会話可能フラグを下ろし、吹き出しアイコン/会話ウインドウを小さくするアニメーションを開始する
	void OnTriggerExit(Collider coll){
		
		if (coll.tag == "Player") {

			// 会話開始の禁止を行う
			isTextStart = false;

			// 吹き出しアイコンを小さくするアニメーションを開始する
			iTween.ScaleTo (messageIcon, iTween.Hash ("scale", new Vector3 (0.0f, 0.0f, 1.0f), "time", 0.3f));
			
			// 会話を強制的に終了させる
			currentLine = 0;
			iTween.ScaleTo(textPanel, iTween.Hash("scale", new Vector3(0.0f, 1.0f, 1.0f), "time", 0.3f, "oncomplete", "OnComplete", "onCompletetarget", this.gameObject));
		}
	}

	
	// テキストウィンドウのアニメーション（ウィンドウを閉じる）が終了したら、全ての文字を表示しCanvasを非表示にする
	void OnComplete()
	{
		textCanvas.SetActive (false);
		Debug.Log(this.name + ": 会話強制終了");
	}
}
