/*
 * LiftGimmick.cs
 * 
 * 説明：リフトを回転させる処理。
 *      リフトの床(Floor）は逆回転させ、床自体が回転しないようにする。
 * 
 * --- How To Use ---
 * アタッチ：LiftGimmick(gameObject)
 * Inspector：【LiftFloor_01】liftFloor_01(gameObject)
 *            【LiftFloor_02】liftFloor_02(gameObject)
 *            【LiftFloor_03】liftFloor_03(gameObject)
 *            【LiftFloor_04】liftFloor_04(gameObject)
 *            【RotateSpeed】 回転スピード
 *            【IsAntiRotate】逆回転させるか
 *
 * 制作：2015/08/15  Guttyon
*/

using UnityEngine;
using System.Collections;

public class LiftGimmick : MonoBehaviour {

	public GameObject liftFloor_01;
	public GameObject liftFloor_02;
	public GameObject liftFloor_03;
	public GameObject liftFloor_04;

	public float rotateSpeed  = 1.0f;
	public bool  isAntiRotate = false;

	// Use this for initialization
	void Start () {
		
		if (!isAntiRotate) { rotateSpeed *= -1; }
	}
	
	// Update is called once per frame
	void Update () {

		// リフトオブジェクト全体を回転させる
		transform.Rotate(new Vector3(0, 0, rotateSpeed),Space.World);

		// リフトの床のみ逆回転させ、床自体は回転させない
		liftFloor_01.transform.Rotate(new Vector3(0, 0, -rotateSpeed),Space.World);
		liftFloor_02.transform.Rotate(new Vector3(0, 0, -rotateSpeed),Space.World);
		liftFloor_03.transform.Rotate(new Vector3(0, 0, -rotateSpeed),Space.World);
		liftFloor_04.transform.Rotate(new Vector3(0, 0, -rotateSpeed),Space.World);
	}
}
