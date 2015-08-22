using UnityEngine;
using System.Collections;

public class BossMovement : MonoBehaviour {

	Transform player;
	NavMeshAgent nav;
	Animator anim;
	bool isDetect = false;
	bool isCaution =false;
	public float timer;
	public float cautionTime = 5;
	
	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		timer = 0;
	}
	
	void FixedUpdate(){

		if (isDetect) {
			nav.SetDestination (player.position);
			nav.speed = 1f;
			if(timer>cautionTime){
				isCaution = true;
				isDetect = false;
			}
		} else if(isCaution){
			anim.SetBool("Running",true);
			nav.speed = 3f;
			nav.SetDestination(player.position);
		} else if(!isDetect){
			nav.speed = 1f;
			nav.SetDestination(new Vector3(0f,0f,0f));
			anim.SetBool("Running",false);
			anim.SetBool ("Walking", false);
			timer = 0;
		}
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Player") {	
			isDetect =true;
			anim.SetBool ("Walking", true);
		}
	}

	void OnTriggerStay(Collider coll){
		if (coll.gameObject.tag == "Player") {	
			timer+=0.01f;
		}
	}

	void OnTriggerExit(Collider coll){
		if (coll.gameObject.tag == "Player") {	
			isDetect =false;
			isCaution = false;
		}
	}
}
