using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using PFCM;

public class Buttons : MonoBehaviour {
	private GUIContent[] classList;
	private bool[] cshow = {false, false, false};
	private bool[] cpicked = {false, false, false};
	private int[] centry = {0, 0, 0};
	private GUIContent[] cselection;
	private int[] clevels = {0, 0, 0};
	private GUIStyle listStyle;

	// Use this for initialization
	void Start () {
		CLASSES[] temp = (CLASSES[])Enum.GetValues(typeof(CLASSES));
		classList = new GUIContent[temp.Length];
		for(int i = 0; i < temp.Length; i++)
		{
			classList[i] = new GUIContent(temp[i].ToString());
		}
		listStyle = new GUIStyle();
		cselection = new GUIContent[3];
		cselection[0] = new GUIContent("CLASS 1");
		cselection[1] = new GUIContent("CLASS 2");
		cselection[2] = new GUIContent("CLASS 3");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		//buttons that I need:
		//New Character, Load Character, Save Character
		if(GUI.Button(new Rect(5,5,100,25),"New Character"))
		{
			Debug.Log("Pressed: New Character");
		}
		if(GUI.Button(new Rect(110,5,100,25),"Save Character"))
		{
			Debug.Log("Pressed: Save Character");
		}
		if(GUI.Button(new Rect(215,5,100,25),"Load Character"))
		{
			Debug.Log("Pressed: Load Character");
		}

		//display: Character Name, Player Name
		Character c = GameObject.FindGameObjectWithTag("stats").GetComponent<CharacterStatTracker>().curChar;
		GUI.Label(new Rect (5, 35, 100, 25), "Character: ");
		c.cname(GUI.TextField (new Rect(70,35,100,25), c.cname()));
		GUI.Label(new Rect (210, 35, 100, 25), "Player: " );
		c.pname(GUI.TextField (new Rect(255,35,100,25), c.pname()));

		//class info
		GUI.Label (new Rect (5, 165, 100, 25), "Classes: ");
		if (Popup.List(new Rect (70, 165, 100, 25),ref cshow[0],ref centry[0],cselection[0],classList,listStyle)) //class 1
		{
			cpicked[0] = true;
			cselection[0] = classList[centry[0]];
		}
		clevels[0] = Int32.Parse(GUI.TextField(new Rect(170,165,25,25), clevels[0].ToString()));

		if (Popup.List(new Rect (200, 165, 100, 25),ref cshow[1],ref centry[1],cselection[1],classList,listStyle)) //class 2
		{
			cpicked[1] = true;
			cselection[1] = classList[centry[1]];
		}
		clevels[1] = Int32.Parse(GUI.TextField(new Rect(300,165,25,25), clevels[1].ToString()));

		if (Popup.List(new Rect (330, 165, 100, 25),ref cshow[2],ref centry[2],cselection[2],classList,listStyle)) //class 3
		{
			cpicked[2] = true;
			cselection[2] = classList[centry[2]];
		}
		clevels[2] = Int32.Parse(GUI.TextField(new Rect(430,165,25,25), clevels[2].ToString()));
		
	}
}
