using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeProblem : MonoBehaviour {
	public static string[,] table;
	public bool[,] hint;

	// Use this for initialization
	void Start () {
		hint = new bool[16, 16];
		table = new string[16, 16];

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

		for (int i = 0; i < 150; i++) {
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
		for (int i = 0; i < 255; i++) {
			string tmp;
			//ランダムに選んで空白にする
			int n1 = Random.Range (0, 16);
			int n2 = Random.Range (0, 16);
			tmp = table [n1, n2];
			table [n1, n2] = "";
			hint [n1, n2] = false;

			//解が複数個でたなら元に戻す
			if (!solver (table)) {
				table [n1, n2] = tmp;
				hint [n1, n2] = true;
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
		BruteForce(sol1, 0);
		//後ろから解く
		BruteForce2(sol2, 0);
		//一致したらtrue
		for (int i = 0; i < 16; i++) {
			for (int j = 0; j < 16; j++) {
				if (sol1 [i, j] != sol2 [i, j]) {
					return false;
				}
			}
		}
		return true;
	}

	void BruteForce(string[,] board, int pos){
		int emptyPosy = pos / 16;
		int emptyPosx = pos % 16;

		for (int y = emptyPosy; y < 16; y++) {
			for (int x = emptyPosx; x < 16; x++) {
				if (board [y, x] == "") {
					pos = y * 16 + x;
					emptyPosx = x;
					emptyPosy = y;
					break;
				}
			}
		}

		if (emptyPosy == 16) {
			return;
		}

		for (int n =  0; n < 16; n++){
			if (CanInput(board, emptyPosx, emptyPosy, n.ToString("X"))){
				board[emptyPosy, emptyPosx] = n.ToString("X");
				BruteForce(board, pos + 1);
				board[emptyPosy, emptyPosx] = "";
			}
		}
	}

	void BruteForce2(string[,] board, int pos){
		int emptyPosy = pos / 16;
		int emptyPosx = pos % 16;

		for (int y = emptyPosy; y < 16; y++) {
			for (int x = emptyPosx; x < 16; x++) {
				if (board [y, x] == "") {
					pos = y * 16 + x;
					emptyPosx = x;
					emptyPosy = y;
					break;
				}
			}
		}
		if (emptyPosy == 16) {
			return;
		}
		for (int n =  15; n >= 0; n--){
			if (CanInput(board, emptyPosx, emptyPosy, n.ToString("X"))){
				board[emptyPosy, emptyPosx] = n.ToString("X");
				BruteForce2(board, pos + 1);
				board[emptyPosy, emptyPosx] = "";
			}
		}
	}

	bool CanInput(string[,] board, int posx, int posy, string inputNum){
		try {
			for (int i = 0; i < 16; i++) {
				if (board [posy, i] == inputNum)
					return false;
				if (board [i, posx] == inputNum)
					return false;
			}
		}
		catch (System.Exception e){
			if (e is System.IndexOutOfRangeException) {
				Debug.Log (posy);
			}
		}
		int topLeftx = posx / 4 * 4;
		int topLefty = posy / 4 * 4;

		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 4; j++) {
				if (board [topLefty + i, topLeftx + j] == inputNum)
					return false;
			}
		}
		return true;
	}
}
