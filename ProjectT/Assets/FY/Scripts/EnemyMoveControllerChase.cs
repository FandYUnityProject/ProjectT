using UnityEngine;
using System.Collections;

public class EnemyMoveControllerChase : MonoBehaviour {

	Transform player;
	NavMeshAgent nav;
	Animator anim;
	bool isDetect = false;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
	}

	void Update(){
		if (isDetect) {
			anim.SetBool ("Walking", true);
			nav.SetDestination (player.position);
		} else {
			anim.SetBool ("Walking", false);
		}
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Player") {	
			isDetect =true;
		}
	}
	void OnTriggerExit(Collider coll){
		if (coll.gameObject.tag == "Player") {	
			isDetect =false;
		}
	}
}
