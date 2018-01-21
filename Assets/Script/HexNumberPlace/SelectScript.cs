using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectScript : MonoBehaviour {
	public static string selectx;
	public static string selecty;
	public static string selectnum;
	public static Text selectText;

	// Use this for initialization
	void Start () {
		selectx = null;
		selecty = null;
		selectnum = " ";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		selectx = transform.gameObject.name;
		selecty = transform.parent.gameObject.name;
		selectText = transform.Find("Canvas/Text").gameObject.GetComponent<Text>();

		if (SelectScript.selectText.text != "")
			selectnum = SelectScript.selectText.text;
	}
}
