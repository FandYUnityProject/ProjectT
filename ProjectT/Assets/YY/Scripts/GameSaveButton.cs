/*
 * GameSaveButton.cs
 * 
 * 説明：「セーブ」を押すと、ゲームデータをセーブする
 * 
 * --- How To Use ---
 * アタッチ：“GameSave”(UIButton)
 * 
 * 制作：2015/08/13  Guttyon
*/

using UnityEngine;
using System.Collections;

public class GameSaveButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// ボタンをクリックするとセーブを行う
	public void OnClick() {

		PlayerPrefs.SetInt ("TEST_DATA", 1);
	}
}
