using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	private int emptycount;
	public TimerScript component;
	private bool flag;

	// Use this for initialization
	void Start () {
		flag = true;
		component = gameObject.GetComponent<TimerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		emptycount = GameObject.FindGameObjectsWithTag("Finish").Length;
		if (emptycount == 0) {
			flag = num_check ();
			if (flag) {
				data.result_time = component.getTime ();
				SceneManager.LoadScene ("Result");
			}
		}
	}

	//被りがないか あったらfalse
	bool num_check(){
		bool gridcheck = grid ();
		bool tatecheck = tate ();
		bool yokocheck = yoko ();

		//全部trueだったら
		if (gridcheck && tatecheck && yokocheck)
			return true;

		//1個でもfalseだったら
		return false;
	}
	bool tate(){
		for (int i = 0; i < 16; i++) {
			int count = 0;
			for (int j = 0; j < 16; j++) {
				count += int.Parse(MakeProblem.table [j, i], System.Globalization.NumberStyles.HexNumber);
			}
			if (count != 120) {
				return false;
			}
		}

		return true;
	}
	bool yoko(){
		for (int i = 0; i < 16; i++) {
			int count = 0;
			for (int j = 0; j < 16; j++) {
				count += int.Parse (MakeProblem.table [i, j], System.Globalization.NumberStyles.HexNumber);
			}
			if (count != 120) {
				return false;
			}
		}

		return true;
	}
	bool grid(){

		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 4; j++) {
				int count = 0;

				for (int a = i * 4; a < i * 4 + 4; a++) {
					for (int b = j * 4; b < j * 4 + 4; b++) {
						count += int.Parse (MakeProblem.table [a, b], System.Globalization.NumberStyles.HexNumber);
					}
				}
				if (count != 120) {
					return false;
				}

			}
		}

		return true;
	}
}
