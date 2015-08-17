using UnityEngine;
using System.Collections;

public class SkyBoxRotateChange : MonoBehaviour {
	
	public Material MorningSkyBox;
	public Material NightSkyBox;
	
	public float duration  = 3.0f;	// Skyboxを徐々に変化させる時間
	public float skyRotate = 0.01f; // Skyboxを回転させる早さ

	// Use this for initialization
	void Start () {
	
		RenderSettings.skybox = MorningSkyBox;
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Rotate (0.0f, skyRotate, 0.0f);
		//float lerp = Mathf.PingPong(Time.time, duration) / duration;
	}

	void OnTriggerEnter(Collider coll){

		if (coll.gameObject.name == "Player") {
			Debug.Log("IN");
			RenderSettings.skybox = NightSkyBox;
			//RenderSettings.skybox.Lerp(MorningSkyBox, NightSkyBox, lerp);
		}
	}

	void OnTriggerExit(Collider coll){
		
		if (coll.gameObject.name == "Player") {
			Debug.Log("OUT");
			RenderSettings.skybox = MorningSkyBox;
			//RenderSettings.skybox.Lerp(NightSkyBox, MorningSkyBox, lerp);
		}
	}
}
