/*
 * StatusController.cs
 * 
 * 説明："Pキー”を押してポーズすると、ポーズ（ステータス）画面を開き、もう一度押すと閉じる
 * 
 * --- How To Use ---
 * アタッチ：StatusController (uGUI)
 * Inspector：【Ui Text】StatusCanvas内にあるuGUIのStatusPanel
 *
 * 制作：2015/08/12  Guttyon
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusController : MonoBehaviour {

	private GameObject statusCanvas;	// uGUIのステータスキャンバス
	public  GameObject statusPanel;		// uGUIのステータスPanel

	bool isFirstPause = false;

	// Use this for initialization
	void Start () {

		// ステータスパネルの大きさを0にする
		statusPanel.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);

		// ”TextCanvas”を非表示にする
		statusCanvas = GameObject.Find ("StatusCanvas");
		statusCanvas.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
		// Pキーを押すと”TextCanvas”を表示してステータスパネルの大きさを元に戻す
		if (Input.GetKeyDown (KeyCode.P)) {

			// キャリブレーション対策で、初めのポーズだけ処理条件を逆にする
			if ( !isFirstPause ){
				// ポーズ（ステータス）画面を表示する
				if ( Pauser.isPause ){

					// ”TextCanvas”を表示してステータスパネルの大きさを元に戻す
					statusCanvas.SetActive(true);
					iTween.ScaleTo(statusPanel, iTween.Hash("scale", new Vector3(1.0f, 1.0f, 1.0f), "time", 0.4f));
					Debug.Log( "ステータス画面を開く");

				} else {

					// ”TextCanvas”を表示してステータスパネルの大きさを小さくする
					iTween.ScaleTo(statusPanel, iTween.Hash("scale", new Vector3(0.0f, 0.0f, 1.0f), "time", 0.4f, "oncomplete", "OnComplete", "onCompletetarget", this.gameObject));
				}

				isFirstPause = true;

			} else {

				// ポーズ（ステータス）画面を表示する
				if ( !Pauser.isPause ){
					
					// ”TextCanvas”を表示してステータスパネルの大きさを元に戻す
					statusCanvas.SetActive(true);
					iTween.ScaleTo(statusPanel, iTween.Hash("scale", new Vector3(1.0f, 1.0f, 1.0f), "time", 0.4f));
					Debug.Log( "ステータス画面を開く");
					
				} else {
					
					// ステータスパネルの大きさを小さくする
					iTween.ScaleTo(statusPanel, iTween.Hash("scale", new Vector3(0.0f, 0.0f, 1.0f), "time", 0.4f, "oncomplete", "OnComplete", "onCompletetarget", this.gameObject));
				}
			}
		}
	}

	// ステータスPanelのアニメーション（ウィンドウを閉じる）が終了したら、Canvasを非表示にする
	void OnComplete()
	{
		statusCanvas.SetActive (false);
		Debug.Log( "ステータス画面を閉じる");
	}
}
