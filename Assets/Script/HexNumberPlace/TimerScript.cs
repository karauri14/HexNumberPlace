using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {
	private int h;
	private int min;
	private float sec;

	private float oldSec;
	private Text timerText;

	// Use this for initialization
	void Start () {
		min = 0;
		sec = 0f;
		oldSec = 0f;
		timerText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		sec += Time.deltaTime;
		if (sec >= 60f) {
			min++;
			sec = sec - 60;
		}
		if (min == 60) {
			h++;
			min = 0;
		}
		if ((int)sec != (int)oldSec) {
			timerText.text = h.ToString ("00") + ":" + min.ToString ("00") + ":" + sec.ToString ("00");
		}
		oldSec = sec;
	}

	public string getTime(){
		return timerText.text;
	}
}
