/*
 * PlayerGround.cs
 * 
 * 説明：プレイヤーが地面に触れているかどうかを確認する
 * 
 * --- How To Use ---
 * アタッチ："Player”(GameObject), "TextController"(GameObject)
 * 
 * 制作：2015/08/13  Guttyon
*/

using UnityEngine;
using System.Collections;

public class PlayerGround : MonoBehaviour {

	public static bool isPlayerGround = true; // プレイヤーが地面に触れているかどうか

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 地面に触れている場合は”true”
	void OnCollisionStay(Collision coll){

		isPlayerGround = true;
	}
	
	// 地面に触れていない場合は”false”
	void OnCollisionExit(Collision coll){

		isPlayerGround = false;
	}
}
