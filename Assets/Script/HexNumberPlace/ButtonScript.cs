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
		int selx, sely;
		selx = int.Parse(SelectScript.selectx, System.Globalization.NumberStyles.HexNumber);
		sely = int.Parse(SelectScript.selecty, System.Globalization.NumberStyles.HexNumber);;

		if (t.text == x) {
			mapdata.table [sely * Constants.N + selx].num = "";
			obj.tag = "Finish";
		} else if (t.text.Length <= 1){
			mapdata.table [sely * Constants.N + selx].num = x;
			SelectScript.selectnum = x;
			obj.tag = "filled";
		}
	}
}
