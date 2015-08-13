/*
 * ContinueButton.cs
 * 
 * 説明：「つづける」を押すと、ポーズ画面を閉じてゲームを再開する
 * 
 * --- How To Use ---
 * アタッチ：“Continue”(UIButton)
 * 
 * 制作：2015/08/13  Guttyon
*/

using UnityEngine;
using System.Collections;

public class ContinueButton : MonoBehaviour {
	
	private GameObject statusPanel;		// uGUIのステータスPanel

	// Use this for initialization
	void Start () {

		// ステータス画面のキャンバスとパネルを取得
		statusPanel = GameObject.Find ("StatusPanel");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// ボタンをクリックするとゲーム画面に戻る
	public void OnClick() {

		// ステータスパネルの大きさを小さくする
		iTween.ScaleTo(statusPanel, iTween.Hash("scale", new Vector3(0.0f, 0.0f, 1.0f), "time", 0.4f,"oncomplete", "OnComplete","oncompletetarget",this.gameObject));
	}

	// ポーズ画面を閉じたらポーズ解除
	void OnComplete()
	{

		// ポーズ解除
		Pauser.Resume ();
		Pauser.isPause = false;
	}
}
