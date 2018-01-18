using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorScript : MonoBehaviour {
	private Material mat;
	//private string selectnum;
	// Use this for initialization
	void Start () {
		mat = this.gameObject.GetComponent<Renderer> ().material;
		//selectnum = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (SelectScript.selectx == gameObject.name && SelectScript.selecty == gameObject.transform.parent.gameObject.name) {
			mat.color = Color.yellow;
		} /*else if (SelectScript.selectText.text != "" && SelectScript.selectText.text == this.transform.Find ("Canvas/Text").gameObject.GetComponent<Text> ().text) {
			mat.color = Color.green;
		} */else {
			mat.color = Color.white;
		}
	}
}
