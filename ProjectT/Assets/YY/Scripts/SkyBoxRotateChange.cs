using UnityEngine;
using System.Collections;

public class SkyBoxRotateChange : MonoBehaviour {
	
	public Material SkyBox;
	
	public float duration  = 1.0f;	// Skyboxを徐々に変化させる時間
	public float skyRotate = 0.01f; // Skyboxを回転させる早さ
	
	// Use this for initialization
	void Start () {
	
		// スカイボックスをセットし、shaderのBlendの値を0(朝）に設定。
		RenderSettings.skybox = SkyBox;
		SkyBox.SetFloat("_Blend", 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
		// スカイボックスをを回転させる
		transform.Rotate (0.0f, skyRotate, 0.0f);
	}

	private void UpdateHandler(float value)
	{
		SkyBox.SetFloat("_Blend", value);
	}


	void OnTriggerEnter(Collider coll){

		if (coll.gameObject.name == "Player") {

			// 朝から夜に変化
			iTween.ValueTo(gameObject, iTween.Hash("from", 0.0f, "to", 1.0f, "time", duration, "onupdate", "UpdateHandler"));
		}
	}

	void OnTriggerExit(Collider coll){
		
		if (coll.gameObject.name == "Player") {

			// 夜から朝に変化
			iTween.ValueTo(gameObject, iTween.Hash("from", 1.0f, "to", 0.0f, "time", duration, "onupdate", "UpdateHandler"));
		}
	}
}
