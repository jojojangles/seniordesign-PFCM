using UnityEngine;
using System.Collections;


public class Game  : MonoBehaviour {

	public static Character curChar;
	public FileBrowser fb;
	
	// Use this for initialization
	void Start () {
		curChar = null;
		fb = new FileBrowser();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
