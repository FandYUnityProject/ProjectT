/*
 * CharacterText.cs
 * 
 * 説明：外部スクリプト”CharacterText.cs”にInspectorで設定した会話内容を送る。
 *      設定には”Size: 表示させるテキストの行数(ウィンドウ内に収まる文字列で1行とする), ElementXX: 表示する文字列” がある。
 *      キャラクター毎にアタッチすれば、キャラクター毎に異なるメッセージが表示可能。
 * 
 * アタッチ：会話メッセージをするキャラクター (GameObject)
 * Inspector：【TextControllerClass】TextController(GameObject)
 *            【Scenarios > Size】表示させるテキストの行数(ウィンドウ内に収まる文字列で1行とする)
 *            【Scenarios > ElementXX】各行の表示する文字列
 * 
 * 制作：2015/08/11  Guttyon
*/

using UnityEngine;
using System.Collections;

public class CharacterText : MonoBehaviour {

	public  TextController 	 textControllerClass;	// textControllerのClass

	public  string[] scenarios;		// シナリオを格納する
	private int currentLine = 0;	// 現在の行番号
	
	public GameObject textCanvas;	// uGUIのテキストキャンバス

	// Use this for initialization
	void Start () {

		// 会話開始時に”TextCanvas”を表示する
		textCanvas = GameObject.Find ("TextCanvas");
	}
	
	// Update is called once per frame
	void Update () {
	
		if (currentLine < scenarios.Length && (Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Return))) {

			// クリックもしくはEnterキーで会話開始
			if (currentLine == 0) {
				// textControllerのGameObject取得
				textCanvas.SetActive (true);
				textControllerClass.StartScenarios (scenarios);
				Debug.Log(this.name + ": 会話開始");
			}	
			currentLine ++;
		} else if (currentLine >= scenarios.Length && (Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Return))) {

			// 会話が終了したら行数番号を0に戻す
			currentLine = 0;
		}
	}
}
