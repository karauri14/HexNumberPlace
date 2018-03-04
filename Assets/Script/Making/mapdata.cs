using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Constants{
	public const int N = 16;
	public const int NN = N * N;
	public const int SHUFFLE = 600;
}

public class mapdata : MonoBehaviour {
	public static MakingClass[] table = new MakingClass[Constants.NN];

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
