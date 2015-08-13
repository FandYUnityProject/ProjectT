/*
 * CourseOutButton.cs
 * 
 * 説明：「でなおす」を押すと、メインマップへ移動する。
 * 尚、移動中もしくはジャンプ、落下中は「つづける」ボタンを押すことはできない。
 * 
 * --- How To Use ---
 * アタッチ：“CourseOut”(UIButton)
 * 
 * 制作：2015/08/13  Guttyon
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CourseOutButton : MonoBehaviour {

	private Button courseOutButtonButtonScript;	// uGUIボタンのUIスクリプト
	private GameObject statusPanel;		// uGUIのステータスPanel

	// Use this for initialization
	void Start () {

		// UIButtonを取得
		courseOutButtonButtonScript = this.GetComponent<Button>();

		//画像(Texture2D)がない場合も必ず必要！
		iTween.CameraFadeAdd();

		// StatusPanelを取得
		statusPanel = GameObject.Find ("StatusPanel");

	}
	
	// Update is called once per frame
	void Update () {

		// 矢印キーに触れず（移動してない）かつ、地面に触れていない（ジャンプ中、落下中でない）場合
		if (Pauser.isNotArrowKey && PlayerGround.isPlayerGround) {

			// UIButtonを活性にする
			courseOutButtonButtonScript.interactable = true;
		} else if ( !Pauser.isNotArrowKey || !PlayerGround.isPlayerGround ) {

			// UIButtonを活性にする
			courseOutButtonButtonScript.interactable = false;
		}
	}
	
	// ボタンをクリックするとコースから出て、メインマップに戻る
	public void OnClick() {

		// ステータスパネルの大きさを小さくする
		iTween.ScaleTo(statusPanel, iTween.Hash("scale", new Vector3(0.0f, 0.0f, 1.0f), "time", 0.4f));

		// 表示を真っ暗にしていくアニメーション
		iTween.CameraFadeTo(iTween.Hash("amount",1.0f,"time",1.5f,"oncomplete", "OnComplete","oncompletetarget",this.gameObject));
	}

	// 表示が真っ暗になったら”メインマップ”シーンへ移動する。
	void OnComplete()
	{
		Debug.Log ("End");
		Application.LoadLevel ("MainMap_YY");
	}
}
