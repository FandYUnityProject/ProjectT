using UnityEngine;
using System.Collections;

public class PlazaScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		//画像(Texture2D)がない場合も必ず必要！
		iTween.CameraFadeAdd();
		
		// 表示をだんだん明るくするアニメーション
		iTween.CameraFadeFrom(iTween.Hash("amount",1.0f,"time",1.5f,"oncomplete", "OnComplete","oncompletetarget",this.gameObject));

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
