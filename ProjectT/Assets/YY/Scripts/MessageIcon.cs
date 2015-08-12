/*
 * MessageIcon.cs
 * 
 * 説明：吹き出しアイコン表示時、アイコンを常にカメラ目線にさせる。
 * 
 * 【注意】：アタッチしたキャラクターのObject内に”MessageIcon(+MessageIcon.cs)”をセットすること！
 * 
 * --- How To Use ---
 * アタッチ：MessageIcon (GameObject)
 * Inspector：【CameraTarget】"Main Camera"(GameObject)をセット
 * 
 * 制作：2015/08/12  Guttyon
*/

using UnityEngine;
using System.Collections;

public class MessageIcon : MonoBehaviour {
	
	public Transform cameraTarget;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.LookAt(cameraTarget);
	}
}
