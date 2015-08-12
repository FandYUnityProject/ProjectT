/*
 * CharacterText.cs
 * 
 * 説明：外部スクリプト”CharacterText.cs”にInspectorで設定した会話内容を送る。
 *      設定には”Size: 表示させるテキストの行数(ウィンドウ内に収まる文字列で1行とする), ElementXX: 表示する文字列” がある。
 *      キャラクター毎にアタッチすれば、キャラクター毎に異なるメッセージが表示可能。
 *      アタッチしたキャラクターのTriggerにプレイヤーが触れ、クリック(Enter)することで会話ウィンドウが表示される
 * 
 * --- How To Use ---
 * アタッチ：会話メッセージをするキャラクター (GameObject)
 * Inspector：【TextControllerClass】TextController(GameObject)をセット
 *            【Scenarios > Size】表示させるテキストの行数(ウィンドウ内に収まる文字列で1行とする)
 *            【Scenarios > ElementXX】各行の表示する文字列
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
	
	private GameObject textCanvas;	// uGUIのテキストキャンバス
	private GameObject textPanel;	// uGUIのテキストPanel
	
	private bool isTextStart      = false;	// 会話を開始したか
	public static bool isTextEnd = false;	// 会話が終了したか
	
	[SerializeField] [Range(0.001f, 0.3f)]
	float intervalForCharacterTextSpeed = 0.05f;	// 1文字の表示にかかる時間

	// Use this for initialization
	void Start () {

		// 会話開始時に”TextCanvas”を表示する
		textCanvas = GameObject.Find ("TextCanvas");
		textPanel  = GameObject.Find ("TextPanel");
	}
	
	// Update is called once per frame
	void Update () {
	
		// トリガー（会話可能範囲）に接触し、マウスクリック（Enter)を押した時の処理
		if(isTextStart){

			// TODO: 会話可能範囲内に入ったら、キャラクターから吹き出しアイコン（会話可能を表すUI）を表示するようにする


			if (currentLine < scenarios.Length && (Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Return))) {

				// クリックもしくはEnterキーで会話開始
				if (currentLine == 0) {

					isTextEnd = false;

					// テキストウィンドウの大きさを初期化し、アニメーションスタート
					textPanel.transform.localScale = new Vector3(0.0f,1.0f,1.0f);
					iTween.ScaleTo(textPanel, iTween.Hash("scale", new Vector3(1.0f, 1.0f, 1.0f), "time", 0.3f));

					// TextControllerのStartScenarios関数を実行
					// (表示するテキスト（Inspectorで設定）, テキスト表示スピード(float 目安: 【早】0.03f〜0.12f【遅】))
					textCanvas.SetActive (true);
					textControllerClass.StartScenarios (scenarios, intervalForCharacterTextSpeed);
					Debug.Log(this.name + ": 会話開始");
				}	
				currentLine ++;
			} else if (isTextEnd) {

				// 会話が終了したら行数番号を0に戻す
				currentLine = 0;
			}
		}
	}

	// トリガー（会話可能範囲）に接触している間の処理
	void OnTriggerStay(Collider coll){

		if (coll.tag == "Player") {

			// TODO: 会話可能範囲内に入ったら、キャラクターから吹き出しアイコン（会話可能を表すUI）を表示するようにする

			if( Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Return)){
				isTextStart = true;
			}
		}
	}

	// トリガー（会話可能範囲）から出たら、会話可能フラグを下ろす
	void OnTriggerExit(Collider coll){
		
		if (coll.tag == "Player") {
			isTextStart = false;
		}
	}
}
