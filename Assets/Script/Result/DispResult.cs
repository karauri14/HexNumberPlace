using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DispResult : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<Text> ().text = data.result_time;
	}
}
