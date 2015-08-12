/*
 *itemをその場でy軸中心に回転させる
 * 2015/8/12 - Fujita
 */

using UnityEngine;
using System.Collections;

public class ItemRotateController : MonoBehaviour {

	public float rotationalSpeed;

	void Update(){
		transform.Rotate (new Vector3 (0f, rotationalSpeed, 0f));
	}

}
