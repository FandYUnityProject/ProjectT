/*
 * TextController.cs
 * 
 * 説明：外部スクリプト”CharacterText.cs”をアタッチしたObjectに設定した会話内容を取得し、表示する。
 *　　　 会話内容は、”CharacterText.cs”をアタッチしたObjectのInspector内にある”Scenarios”内で設定可能。
 *      設定には”Size: 表示させるテキストの行数(ウィンドウ内に収まる文字列で1行とする), ElementXX: 表示する文字列” がある。
 * 
 * アタッチ：TextCanvas (uGUI)
 * Inspector：【Ui Text】Canvas内にあるuGUIのUI Text
 *
 * 制作：2015/08/11  Guttyon
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextController : MonoBehaviour {
	
	private string[]  scenarios;	// 外部スクリプトから受け取ったシナリオを格納する
	[SerializeField] Text uiText;	// uiTextへの参照を保つ

	[SerializeField] [Range(0.001f, 0.3f)]
	float intervalForCharacterDisplay = 0.05f;	// 1文字の表示にかかる時間
	
	private int currentLine = 0;				// 現在の行番号
	private string currentText = string.Empty;	// 現在の文字列
	private float timeUntilDisplay = 0;			// 表示にかかる時間
	private float timeElapsed = 1;				// 文字列の表示を開始した時間
	private int lastUpdateCharacter = -1;		// 表示中の文字数

	private GameObject textCanvas;	// uGUIのテキストキャンバス
	bool isStartText = false;

	// 文字の表示が完了しているかどうか
	public bool IsCompleteDisplayTest{
		get { return Time.time > timeElapsed + timeUntilDisplay; }
	}

	void Start()
	{
		// ”TextCanvas”を非表示にする
		textCanvas = GameObject.Find ("TextCanvas");
		textCanvas.SetActive(false);
	}
	
	void Update () 
	{
		if (isStartText) {
			// 文字の表示が完了しているならクリック(Enter)時に次の行を表示する
			if (IsCompleteDisplayTest) {
				// 現在の行番号がラストまで行ってない状態でクリック(Enter)すると、テキストを更新する
				if (currentLine < scenarios.Length && (Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Return))) {
					SetNextLine ();
				} else if (currentLine >= scenarios.Length && (Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Return))) {
					// 全ての文字を表示したらCanvasを非表示にする
					textCanvas.SetActive (false);
					isStartText = false;
					Debug.Log ("会話終了");
				}
			} else {
				// 完了していないなら文字をすべて表示する
				if (Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Return)) {
					timeUntilDisplay = 0;
				}
			}
			
			// クリックから経過した時間が想定表示時間の何%か確認し、表示文字数を出す
			int displayCharacterCount = (int)(Mathf.Clamp01 ((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
			
			// 表示文字数が前回の表示文字数と異なるならテキストを更新する
			if (displayCharacterCount != lastUpdateCharacter) {
				uiText.text = currentText.Substring (0, displayCharacterCount);
				lastUpdateCharacter = displayCharacterCount;
			}
		}
	}

	// テキストを更新する
	void SetNextLine()
	{
		// 現在の行のテキストをuiTextに流し込み、現在の行番号を一つ追加する
		currentText = scenarios[currentLine];
		currentLine ++;
		
		// 想定表示時間と現在の時刻をキャッシュ
		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;
		
		// 文字カウントを初期化
		lastUpdateCharacter = -1;
	}

	// 表示するテキストを受け取り、表示させる
	public void StartScenarios(string[] scenario){

		// TextCanvas表示
		textCanvas.SetActive(true);

		// 示するテキスト内容を取得し、テキスト表示開始
		scenarios = scenario;
		isStartText = true;

		// 初期化
		currentLine = 0;
		currentText = string.Empty;
		timeUntilDisplay = 0;
		timeElapsed = 1;
		lastUpdateCharacter = -1;
		
		SetNextLine();
	}
}
