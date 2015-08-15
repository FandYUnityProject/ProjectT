using UnityEngine;
using System.Collections;

public class LiftGimmick : MonoBehaviour {

	private GameObject liftFloor_01;
	private GameObject liftFloor_02;
	private GameObject liftFloor_03;
	private GameObject liftFloor_04;

	// Use this for initialization
	void Start () {
		
		liftFloor_01 = GameObject.Find ("LiftFloor_01");
		liftFloor_02 = GameObject.Find ("LiftFloor_02");
		liftFloor_03 = GameObject.Find ("LiftFloor_03");
		liftFloor_04 = GameObject.Find ("LiftFloor_04");
	}
	
	// Update is called once per frame
	void Update () {

		// リフトオブジェクト全体を回転させる
		transform.Rotate(new Vector3(0, 0, 1),Space.World);

		// リフトの床のみ逆回転させ、床自体は回転させない
		liftFloor_01.transform.Rotate(new Vector3(0, 0, -1),Space.World);
		liftFloor_02.transform.Rotate(new Vector3(0, 0, -1),Space.World);
		liftFloor_03.transform.Rotate(new Vector3(0, 0, -1),Space.World);
		liftFloor_04.transform.Rotate(new Vector3(0, 0, -1),Space.World);
	}
}
