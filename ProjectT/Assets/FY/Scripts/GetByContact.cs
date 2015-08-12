using UnityEngine;
using System.Collections;

public class GetByContact : MonoBehaviour {
	public AudioSource av;

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "BallWeapon") {
			//process get items and reflect to pose scene
			//UIかPlayerのScriptの関数を呼び出す形で連携
			av.Play ();
			if (!av.isPlaying)
				Destroy (gameObject);
		}
	}
}