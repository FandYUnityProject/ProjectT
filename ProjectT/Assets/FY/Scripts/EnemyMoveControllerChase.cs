using UnityEngine;
using System.Collections;

public class EnemyMoveControllerChase : MonoBehaviour {

	Transform player;
	NavMeshAgent nav;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent<NavMeshAgent> ();
	}

	void Update(){
		nav.SetDestination (player.position);
	}
}
