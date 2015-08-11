using UnityEngine;
using System.Collections;

public class DamageByContact : MonoBehaviour {

	public int amountDamage;
	public float waitTime = 0.5f;
	public float Timer;

	void Update(){
		Timer += Time.deltaTime;
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Player" && Timer > waitTime) {
			//PlayerHealth.TakeDamage(amountDamege);
			Timer = 0f;
		}
	}
}