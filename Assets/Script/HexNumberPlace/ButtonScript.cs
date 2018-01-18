using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClick(){
		string name = "Table/" + SelectScript.selecty + "/" + SelectScript.selectx + "/Canvas/Text";
		GameObject obj = GameObject.Find(name).gameObject;
		Text t = obj.GetComponent<Text>();
		string x = transform.gameObject.name.Replace("Button", "");
		if (t.text == x) {
			t.text = "";
			obj.tag = "Finish";
		} else {
			t.text = x;
			obj.tag = "filled";
		}
	}
}
