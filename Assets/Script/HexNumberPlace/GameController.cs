using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	private int emptycount;
	public TimerScript component;

	// Use this for initialization
	void Start () {
		component = gameObject.GetComponent<TimerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		emptycount = GameObject.FindGameObjectsWithTag("Finish").Length;
		if (emptycount == 255) {
			data.result_time = component.getTime ();
			SceneManager.LoadScene ("Result");
		}
	}
}
