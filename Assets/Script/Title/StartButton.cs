using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClick(){
		string difficulty = this.gameObject.name;
		if (difficulty.Equals ("EASY")) {
			DifficultySelect.diff = 100;
		} else if (difficulty.Equals ("NORMAL")) {
			DifficultySelect.diff = 150;
		} else if (difficulty.Equals ("HARD")) {
			DifficultySelect.diff = 255;
		}
		SceneManager.LoadScene("HexNumberPlace");
	}
}
