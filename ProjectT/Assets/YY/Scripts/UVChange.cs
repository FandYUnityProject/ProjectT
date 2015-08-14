/*
 * UVChange.cs
 * 
 * 説明：1枚のTextureを６分割し、立方体の各面に貼り付けることを可能にする
 * 
 * --- How To Use ---
 * アタッチ：立方体のオブジェクト(gameObject)
 * ※Textureは【□□□□□□】のように、横一列に６面分のデザインを作成する
 *
 * 制作：2015/08/15  Guttyon
*/

using UnityEngine;
using System.Collections;

public class UVChange : MonoBehaviour {

	// 立方体の各面の頂点 × 立方体の面の数 = 4 × 6 = 24
	Vector2[] uv = new Vector2[24];
	
	void Awake()
	{
		// 1の面
		uv[2].x = 0.0f; uv[2].y = 0.0f;
		uv[3].x = 1.0f / 6.0f; uv[3].y = 0.0f;
		uv[0].x = 0.0f; uv[0].y = 1.0f;
		uv[1].x = 1.0f / 6.0f; uv[1].y = 1.0f;
		
		// 2の面
		uv[4].x = 1.0f / 6.0f; uv[4].y = 0.0f;
		uv[5].x = 2.0f / 6.0f; uv[5].y = 0.0f;
		uv[8].x = 1.0f / 6.0f; uv[8].y = 1.0f;
		uv[9].x = 2.0f / 6.0f; uv[9].y = 1.0f;
		
		// 3の面
		uv[23].x = 2.0f / 6.0f; uv[23].y = 0.0f;
		uv[21].x = 3.0f / 6.0f; uv[21].y = 0.0f;
		uv[20].x = 2.0f / 6.0f; uv[20].y = 1.0f;
		uv[22].x = 3.0f / 6.0f; uv[22].y = 1.0f;
		
		// 4の面
		uv[19].x = 3.0f / 6.0f; uv[19].y = 0.0f;
		uv[17].x = 4.0f / 6.0f; uv[17].y = 0.0f;
		uv[16].x = 3.0f / 6.0f; uv[16].y = 1.0f;
		uv[18].x = 4.0f / 6.0f; uv[18].y = 1.0f;
		
		// 5の面
		uv[15].x = 4.0f / 6.0f; uv[15].y = 0.0f;
		uv[13].x = 5.0f / 6.0f; uv[13].y = 0.0f;
		uv[12].x = 4.0f / 6.0f; uv[12].y = 1.0f;
		uv[14].x = 5.0f / 6.0f; uv[14].y = 1.0f;
		
		// 6の面
		uv[6].x  = 5.0f / 6.0f; uv[6].y  = 0.0f;
		uv[7].x  = 6.0f / 6.0f; uv[7].y  = 0.0f;
		uv[10].x = 5.0f / 6.0f; uv[10].y = 1.0f;
		uv[11].x = 6.0f / 6.0f; uv[11].y = 1.0f;
	}

	// Use this for initialization
	void Start () {
	
		// ６面に分割する
		MeshFilter mf = gameObject.GetComponent<MeshFilter>();
		mf.mesh.uv = uv;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
