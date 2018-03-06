using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MakingClass {
	public string num;
	public bool hint;
	public List<string> canInput;

	public MakingClass(){
		num = "";
		hint = true;
		canInput = new List<string>(){"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F"};
	}
	public MakingClass(MakingClass source){
		this.num = System.String.Copy(source.num);
		this.hint = source.hint;
		this.canInput = source.canInput;
	}

	public void UpdateCanInput(MakingClass[] board, int posx, int posy){
		List<string> numlist1 = new List<string>();
		List<string> numlist2 = new List<string>();
		List<string> numlist3 = new List<string>();

		for (int i = 0; i < Constants.N; i++) {
			numlist1.Add(board[posy * Constants.N + i].num);
			numlist2.Add(board [i * Constants.N + posx].num);
		}
		int topLeftx = posx / 4 * 4;
		int topLefty = posy / 4 * 4;

		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 4; j++) {
				numlist3.Add (board[(topLefty + i) * Constants.N + topLeftx + j].num);
			}
		}
		//入れられない数字の和集合
		List<string> numlist = numlist1.Union<string> (numlist2.Union<string> (numlist3)).Distinct<string>().ToList();
		this.canInput = this.canInput.Except<string>(numlist).ToList();
	}

	public void UpdateCanInput(MakingClass[] board, int posx, int posy, string num){
		for (int i = 0; i < Constants.N; i++) {
			board [posy * Constants.N + i].canInput.Remove (num);
			board [i * Constants.N + posx].canInput.Remove (num);
		}
		int topLeftx = posx / 4 * 4;
		int topLefty = posy / 4 * 4;

		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 4; j++) {
				board [(topLefty + i) * Constants.N + topLeftx + j].canInput.Remove (num);
			}
		}
	}
}

public class MakeTable : MonoBehaviour {
	public static string[] ans1 = new string[Constants.NN];
	public static string[] ans2 = new string[Constants.NN];

	private int count = 0;
	private int limit = 0;

	public Slider slide;

	void Start(){
		InitMap ();

	}

	void Update() {
		StartCoroutine ("Making");
	}

	IEnumerator Making(){
		string tmp;
		bool flag;

		while (count < DifficultySelect.diff) {
			//ランダムに選んで空白にする
			int n = Random.Range (0, Constants.NN);
			if (++limit == 400) {
				SceneManager.LoadScene ("Scene/HexNumberPlace");
			}
			if (System.String.IsNullOrEmpty (mapdata.table [n].num) == false) {
				tmp = mapdata.table [n].num;
				mapdata.table [n].num = "";
				mapdata.table [n].hint = false;
				MakeEmpty (mapdata.table, n);
				flag = solver (mapdata.table);
				//解が複数個でたなら元に戻す
				if (flag != true) {
					mapdata.table [n].num = tmp;
					mapdata.table [n].hint = true;
				} else {
					count++;
					slide.value = count / (DifficultySelect.diff * 1.0f);
				}
			}
			yield return null;
		}

		SceneManager.LoadScene ("Scene/HexNumberPlace");
	}

	//空白作成時リスト更新用
	void MakeEmpty(MakingClass[] board, int pos){
		int y = pos / Constants.N;
		int x = pos % Constants.N;

		//縦と横
		for (int i = 0; i < Constants.N; i++) {
			if (board [i * Constants.N + x].hint == false) {
				board [i * Constants.N + x].UpdateCanInput (board, x, i);
			}
			if (board [y * Constants.N + i].hint == false) {
				board [y * Constants.N + i].UpdateCanInput (board, i, y);
			}
		}
		//グリッド
		int leftx = x / 4 * 4;
		int lefty = y / 4 * 4;

		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 4; j++) {
				if (board [(lefty + i) * Constants.N + leftx + j].hint == false) {
					board [(lefty + i) * Constants.N + leftx + j].UpdateCanInput (board, leftx + j, lefty + i);
				}
			}
		}
	}
	//ソルバ
	bool solver(MakingClass[] board){
		//配列コピー
		MakingClass[] sol1 = new MakingClass[Constants.NN];
		MakingClass[] sol2 = new MakingClass[Constants.NN];

		for (int i = 0; i < Constants.NN; i++) {
			sol1[i] = new MakingClass(board[i]);
			sol2[i] = new MakingClass(board[i]);
		}

		//前から解く
		BruteForce(sol1, 0, 1);
		//後ろから解く
		BruteForce(sol2, 0, -1);
		//間違いがあるとfalse
		for (int i = 0; i < Constants.NN; i++) {
			if (ans1 [i] != ans2 [i]) {
				return false;
			}
			if (ans1 [i] == "") {
				return false;
			}
		}
		return true;
	}

	bool BruteForce(MakingClass[] board, int pos, int direction){
		bool flag;
		int num;
		int n;

		//空白探し
		for (n = pos; n < Constants.NN; n++) {
			if (board [n].canInput.Count() == 1 && board[n].hint == false) {
				board [n].num = board [n].canInput.First().ToString ();
				continue;
			}
			if (System.String.IsNullOrEmpty (board [n].num)) {
				pos = n;
				break;
			}
		}

		//全部埋まったとき答えをコピー
		if (pos == Constants.NN || n == Constants.NN) {
			if (direction == 1) {
				for (int i = 0; i < Constants.NN; i++){
					ans1[i] = board[i].num;
				}
			} else {
				for (int i = 0; i < Constants.NN; i++){
					ans2[i] = board[i].num;
				}
			}
			return true;
		}

		if (direction == 1) {
			num = 0;
		}
		else {
			num = Constants.N - 1;
		}

		while(num != Constants.N && num != -1){
			if (board[pos].canInput.Contains(num.ToString())){
				board[pos].num = num.ToString("X");
				flag = BruteForce(board, pos + 1, direction);
				if (flag) {
					break;
				}
				board[pos].num = "";
			}
			num += direction;
		}

		return false;
	}

	//答え作成
	void InitMap(){
		//int n;

		//力こそパワー
		string[] tmp = new string[]{
		//-----------------------------------------------------------------------------------
			"0", "2", "8", "A",  "1", "F", "4", "9",  "6", "C", "3", "B",  "5", "D", "7", "E",
			"F", "4", "D", "B",  "3", "E", "8", "0",  "7", "9", "A", "5",  "1", "C", "2", "6",
			"6", "9", "E", "7",  "2", "5", "A", "C",  "4", "0", "D", "1",  "B", "3", "8", "F",
			"C", "1", "3", "5",  "D", "B", "6", "7",  "F", "8", "2", "E",  "9", "0", "4", "A",
		//-----------------------------------------------------------------------------------
			"5", "8", "A", "0",  "4", "7", "F", "3",  "1", "B", "9", "2",  "6", "E", "D", "C",
			"7", "E", "4", "6",  "9", "8", "0", "B",  "D", "3", "C", "A",  "F", "2", "5", "1",
			"1", "3", "2", "F",  "6", "A", "C", "D",  "5", "E", "7", "0",  "8", "4", "B", "9",
			"B", "D", "C", "9",  "E", "1", "2", "5",  "8", "4", "6", "F",  "A", "7", "0", "3",
		//-----------------------------------------------------------------------------------
			"9", "C", "7", "D",  "F", "3", "1", "E",  "B", "5", "0", "6",  "2", "8", "A", "4",
			"3", "5", "0", "2",  "C", "6", "9", "8",  "A", "D", "1", "4",  "7", "F", "E", "B",
			"A", "F", "1", "8",  "7", "D", "B", "4",  "C", "2", "E", "9",  "0", "6", "3", "5",
			"4", "B", "6", "E",  "A", "0", "5", "2",  "3", "7", "F", "8",  "C", "1", "9", "D",
		//-----------------------------------------------------------------------------------
			"E", "0", "9", "1",  "B", "C", "3", "A",  "2", "F", "8", "D",  "4", "5", "6", "7",
			"2", "6", "B", "C",  "8", "4", "7", "1",  "E", "A", "5", "3",  "D", "9", "F", "0",
			"D", "7", "5", "4",  "0", "2", "E", "F",  "9", "6", "B", "C",  "3", "A", "1", "8",
			"8", "A", "F", "3",  "5", "9", "D", "6",  "0", "1", "4", "7",  "E", "B", "C", "2"
		//-----------------------------------------------------------------------------------
		};

		for (int i = 0; i < Constants.NN; i++) {
			mapdata.table [i] = new MakingClass ();
			mapdata.table [i].num = System.String.Copy(tmp [i]);
		}

		/*
		for (int i = 0; i < Constants.N; i++){
			n = i;
			for (int j = 0; j < Constants.N; j++) {
				mapdata.table [i * Constants.N + j].num = n.ToString ("X");
				n = (n + 1) % Constants.N;
			}
		}

		swap_tate (1, 4);
		swap_tate (2, 8);
		swap_tate (3, 12);
		swap_tate (6, 9);
		swap_tate (7, 13);
		swap_tate (11, 14);
		*/

		Shuffle(20);
	}

	public void Shuffle(int num){
		int l;
		for (int i = 0; i < num; i++) {
			l = Random.Range (0, Constants.N) / 4 * 4;
			swap_tate(l + Random.Range(0, 4), l + Random.Range(0, 4));
			l = Random.Range (0, Constants.N) / 4 * 4;
			swap_yoko(l + Random.Range(0, 4), l + Random.Range(0, 4));
			swap_grid_tate (Random.Range (0, Constants.N), Random.Range (0, Constants.N));
			swap_grid_yoko (Random.Range (0, Constants.N), Random.Range (0, Constants.N));
		}
	}

	private void swap_tate (int a, int b){
		string tmp;
		for (int i = 0; i < Constants.N; i++) {
			tmp = System.String.Copy(mapdata.table[a * Constants.N + i].num);
			mapdata.table[a * Constants.N + i].num = System.String.Copy(mapdata.table[b * Constants.N + i].num);
			mapdata.table[b * Constants.N + i].num = System.String.Copy(tmp);
		}
	}

	private void swap_yoko (int a, int b){
		string tmp;
		for (int i = 0; i < Constants.N; i++) {
			tmp = mapdata.table[i * Constants.N + a].num;
			mapdata.table[i * Constants.N + a].num = mapdata.table[i * Constants.N + b].num;
			mapdata.table[i * Constants.N + b].num = tmp;
		}
	}

	private void swap_grid_tate(int a, int b){
		int s = a / 4 * 4;
		int d = b / 4 * 4;

		swap_tate (s, d);
		swap_tate (s + 1, d + 1);
		swap_tate (s + 2, d + 2);
		swap_tate (s + 3, d + 3);
	}

	private void swap_grid_yoko(int a, int b){
		int s = a / 4 * 4;
		int d = b / 4 * 4;

		swap_yoko (s, d);
		swap_yoko (s + 1, d + 1);
		swap_yoko (s + 2, d + 2);
		swap_yoko (s + 3, d + 3);
	}
}