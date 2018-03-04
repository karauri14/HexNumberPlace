using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class disp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < Constants.NN; i++) {
			string path = "" + (i / Constants.N).ToString("X") + "/" + (i % Constants.N).ToString("X") + "/Canvas/Text";
			Text target = transform.Find (path).gameObject.GetComponent<Text> ();

			if (mapdata.table [i].num == "") {
				target.text = "";
			} else if (mapdata.table [i].hint) {
				target.text = "<b>" + mapdata.table [i].num + "</b>";
				target.tag = "filled";
			} else {
				target.text = mapdata.table [i].num;
			}
		}
	}
}
