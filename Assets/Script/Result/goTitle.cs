using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goTitle : MonoBehaviour {
	public void OnClick(){
		SceneManager.LoadScene ("Scene/Title");
	}
}
