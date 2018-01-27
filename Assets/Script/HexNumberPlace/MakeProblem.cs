using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MakeProblem : MonoBehaviour {
	public static string[,] table;
	public bool[,] hint;

	private string[,] ans1;
	private string[,] ans2;

	// Use this for initialization
	void Start () {
		ans1 = new string[16, 16];
		ans2 = new string[16, 16];
		hint = new bool[16, 16];
		table = new string[16, 16];
		int count = new int();
		//問題用配列作成ここから
		int n = 0;
		for (int i = 0; i < 16; i++) {
			for (int j = 0; j < 16; j++) {
				table [i, j] = (n + i).ToString ("X");
				n++;
				if (n + i > 15) {
					n = -i;
				}
				hint [i, j] = true;
			}
		}

		swap_tate (1, 4);
		swap_tate (2, 8);
		swap_tate (3, 12);
		swap_tate (6, 9);
		swap_tate (7, 13);
		swap_tate (11, 14);

		for (int i = 0; i < 300; i++) {
			int l;
			swap_grid_tate (Random.Range(0, 16), Random.Range(0, 16));
			swap_grid_yoko (Random.Range(0, 16), Random.Range(0, 16));
			swap_grid_tate (Random.Range(0, 16), Random.Range(0, 16));
			swap_grid_yoko (Random.Range(0, 16), Random.Range(0, 16));
			swap_grid_tate (Random.Range(0, 16), Random.Range(0, 16));
			swap_grid_yoko (Random.Range(0, 16), Random.Range(0, 16));
			l = Random.Range (0, 16) / 4 * 4;
			swap_tate (l, l + 2);
			l = Random.Range (0, 16) / 4 * 4;
			swap_tate (l, l + 1);
			l = Random.Range (0, 16) / 4 * 4;
			swap_yoko (l, l + 2);
			l = Random.Range (0, 16) / 4 * 4;
			swap_yoko (l, l + 1);
			l = Random.Range (0, 16) / 4 * 4;
			swap_tate (l + 2, l + 1);
			l = Random.Range (0, 16) / 4 * 4;
			swap_yoko (l + 2, l + 1);
			l = Random.Range (0, 16) / 4 * 4;
			swap_yoko (l, l + 3);
			l = Random.Range (0, 16) / 4 * 4;
			swap_tate (l, l + 3);
			l = Random.Range (0, 16) / 4 * 4;
			swap_yoko (l + 2, l + 3);
			l = Random.Range (0, 16) / 4 * 4;
			swap_tate (l + 2, l + 3);
			l = Random.Range (0, 16) / 4 * 4;
			swap_yoko (l + 1, l + 3);
			l = Random.Range (0, 16) / 4 * 4;
			swap_tate (l + 1, l + 3);
		}
		//ここまで

		//空白作成
		count = 0;
		while (count < DifficultySelect.diff) {
			int limit = 0;
			string tmp;
			bool flag;


			//ランダムに選んで空白にする
			int n1 = Random.Range (0, 16);
			int n2 = Random.Range (0, 16);
			if (++limit == 600) {
				break;
			}
			if (table [n1, n2] == "") {
				continue;
			}

			tmp = string.Copy(table [n1, n2]);
			table [n1, n2] = "";
			hint [n1, n2] = false;

			flag = solver (table);
			//解が複数個でたなら元に戻す
			if (flag != true) {
				table [n1, n2] = string.Copy (tmp);
				hint [n1, n2] = true;
			} else {
				count++;
			}

		}
	}
	
	// Update is called once per frame
	void Update () {
		string path;
		Text target;
		for (int i = 0; i < 16; i++) {
			for (int j = 0; j < 16; j++) {
				path = "Table" + i.ToString("X") + "/" + j.ToString("X") + "/Canvas/Text";
				target = GameObject.Find (path).gameObject.GetComponent<Text> ();
				if (table [i, j] == "") {
					target.text = "";
				} else if (hint [i, j]) {
					target.text = "<b>" + table[i, j] + "</b>";
					target.tag = "filled";
				} else {
					target.text = table[i, j];
				}
			}
		}
	}

	//問題作成用あれこれ
	void swap_tate (int a, int b){
		string tmp;
		for (int i = 0; i < 16; i++) {
			tmp = table [a, i];
			table [a, i] = table [b, i];
			table [b, i] = tmp;
		}
	}

	void swap_yoko (int a, int b){
		string tmp;
		for (int i = 0; i < 16; i++) {
			tmp = table [i, a];
			table [i, a] = table [i, b];
			table [i, b] = tmp;
		}
	}

	void swap_grid_tate(int a, int b){
		int s = a / 4 * 4;
		int d = b / 4 * 4;

		swap_tate (s, d);
		swap_tate (s + 1, d + 1);
		swap_tate (s + 2, d + 2);
		swap_tate (s + 3, d + 3);
	}

	void swap_grid_yoko(int a, int b){
		int s = a / 4 * 4;
		int d = b / 4 * 4;

		swap_yoko (s, d);
		swap_yoko (s + 1, d + 1);
		swap_yoko (s + 2, d + 2);
		swap_yoko (s + 3, d + 3);
	}
	//ここまで

	//問題が解けるか確認する
	bool solver(string[,] sol){
		//配列コピー
		string[,] sol1 = new string[16, 16];
		string[,] sol2 = new string[16, 16];

		System.Array.Copy (sol, sol1, 256);
		System.Array.Copy (sol, sol2, 256);

		//前から解く
		BruteForce(sol1, 0, 1);
		//後ろから解く
		BruteForce(sol2, 0, -1);
		//間違いがあるとfalse
		for (int i = 0; i < 16; i++) {
			for (int j = 0; j < 16; j++) {
				if (ans1 [i, j] != ans2 [i, j]) {
					return false;
				}
				if (string.IsNullOrEmpty(ans1[i, j])){
					return false;
				}
			}
		}
		return true;
	}

	bool BruteForce(string[,] board, int pos, int direction){
		bool flag;
		int num;

		for (int n = pos; n < 256; n++) {
			if (System.String.IsNullOrEmpty (board [n / 16, n % 16])) {
				pos = n;
				break;
			}
		}
		if (pos == 256) {
			if (direction == 1) {
				System.Array.Copy (board, ans1, 256);
			} else {
				System.Array.Copy (board, ans2, 256);
			}
			return true;
		}
		if (direction == 1) {
			num = 0;
		}
		else {
			num = 15;
		}
		while(num != 16 && num != -1){
			if (CanInput(board, pos / 16, pos % 16, num.ToString("X"))){
				board[pos / 16, pos % 16] = num.ToString("X");
				flag = BruteForce(board, pos + 1, direction);
				if (flag) {
					break;
				}
				board[pos / 16, pos % 16] = "";
			}
			num += direction;
		}

		return false;
	}

	bool CanInput(string[,] board, int posx, int posy, string inputNum){
		List<string> numlist1 = new List<string>();
		List<string> numlist2 = new List<string>();
		List<string> numlist3 = new List<string>();

		for (int i = 0; i < 16; i++) {
			numlist1.Add(board [posy, i]);
			numlist2.Add(board [i, posx]);
		}
		int topLeftx = posx / 4 * 4;
		int topLefty = posy / 4 * 4;

		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 4; j++) {
				numlist3.Add (board [topLefty + i, topLeftx + j]);
			}
		}
		//入れられない数字の和集合
		var numlist = numlist1.Union<string> (numlist2.Union<string> (numlist3)).Distinct<string>();

		if (numlist.Contains (inputNum)) {
			return false;
		}
		return true;
	}
}
