using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	public GameObject loadingUI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClick(){
		string difficulty = this.gameObject.name;
		if (difficulty.Equals ("EASY")) {
			DifficultySelect.diff = 50;
		} else if (difficulty.Equals ("NORMAL")) {
			DifficultySelect.diff = 80;
		} else if (difficulty.Equals ("HARD")) {
			DifficultySelect.diff = 90;
		}
		loadingUI.SetActive (true);
		SceneManager.LoadScene ("HexNumberPlace");
	}

}
