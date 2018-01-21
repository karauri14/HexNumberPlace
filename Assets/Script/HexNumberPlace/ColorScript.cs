using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorScript : MonoBehaviour {
	private Material mat;
	// Use this for initialization
	void Start () {
		mat = this.gameObject.GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		if (SelectScript.selectx == gameObject.name && SelectScript.selecty == gameObject.transform.parent.gameObject.name) {
			mat.color = Color.yellow;
		} else if (SelectScript.selectnum == gameObject.transform.Find ("Canvas/Text").GetComponent<Text> ().text) {
			mat.color = Color.green;
		} else if ("<b>" + SelectScript.selectnum + "</b>"== gameObject.transform.Find ("Canvas/Text").GetComponent<Text> ().text) {
			mat.color = Color.green;
		} else if (SelectScript.selectnum == "<b>" + gameObject.transform.Find ("Canvas/Text").GetComponent<Text> ().text + "</b>") {
			mat.color = Color.green;
		} else {			
			mat.color = Color.white;
		}
	}
}
