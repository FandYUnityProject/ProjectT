using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int health;

	void OnCollisionEnter (Collision coll){
		if (coll.gameObject.tag == "BallWeapon") {
			TakeDamage();
		}
	}

	void TakeDamage(){
		health -= 1;
		if (health <= 0) {
			Destroy(gameObject,5f);
		}
	}
}
