using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using PFCM;

public class Buttons : MonoBehaviour {
	//vars for classes
	private GUIContent[] classList;
	private bool[] cshow = {false, false, false};
	private bool[] cpicked = {false, false, false};
	private int[] centry = {0, 0, 0};
	private GUIContent[] cselection;
	private int[] clevels = {0, 0, 0};

	//vars for races
	private GUIContent[] raceList;
	private bool rshow = false;
	private bool rpicked = false;
	private int rentry = 0;
	private GUIContent rselection;

	private GUIStyle listStyle;

	// Use this for initialization
	void Start () {
		listStyle = new GUIStyle();

		CLASSES[] ctemp = (CLASSES[])Enum.GetValues(typeof(CLASSES));
		classList = new GUIContent[ctemp.Length];
		for(int i = 0; i < ctemp.Length; i++)
		{
			classList[i] = new GUIContent(ctemp[i].ToString());
		}
		cselection = new GUIContent[3];
		cselection[0] = new GUIContent("CLASS 1");
		cselection[1] = new GUIContent("CLASS 2");
		cselection[2] = new GUIContent("CLASS 3");

		RACES[] rtemp = (RACES[])Enum.GetValues(typeof(RACES));
		raceList = new GUIContent[rtemp.Length];
		for(int i = 0; i < rtemp.Length; i++)
		{
			raceList[i] = new GUIContent(rtemp[i].ToString());
		}
		rselection = new GUIContent("RACE");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		//buttons that I need:
		//New Character, Load Character, Save Character
		GUI_saveLoad();

		//display: Character Name, Player Name
		GUI_names();

		GUI_race();
		//ability scores

		//class info
		GUI_classInfo();


	}

	void GUI_saveLoad()
	{
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
	}

	void GUI_names()
	{
		Character c = GameObject.FindGameObjectWithTag("stats").GetComponent<CharacterStatTracker>().curChar;
		GUI.Label(new Rect (5, 35, 100, 25), "Character: ");
		c.cname(GUI.TextField (new Rect(70,35,135,25), c.cname()));
		GUI.Label(new Rect (210, 35, 100, 25), "Player: " );
		c.pname(GUI.TextField (new Rect(255,35,135,25), c.pname()));
	}

	void GUI_race()
	{
		GUI.Label(new Rect(400,35,140,25), "Race: ");
		if (Popup.List(new Rect (440, 35, 100, 25),ref rshow,ref rentry,rselection,raceList,listStyle)) //class 2
		{
			rpicked = true;
			rselection = raceList[rentry];
		}
	}

	void GUI_classInfo()
	{
		GUI.Label (new Rect (5, 165, 100, 25), "Classes: ");
		if (Popup.List(new Rect (70, 165, 100, 25),ref cshow[0],ref centry[0],cselection[0],classList,listStyle)) //class 1
		{
			cpicked[0] = true;
			cselection[0] = classList[centry[0]];
		}
		clevels[0] = Int32.Parse(GUI.TextField(new Rect(175,165,25,25), clevels[0].ToString()));
		
		if (Popup.List(new Rect (205, 165, 100, 25),ref cshow[1],ref centry[1],cselection[1],classList,listStyle)) //class 2
		{
			cpicked[1] = true;
			cselection[1] = classList[centry[1]];
		}
		clevels[1] = Int32.Parse(GUI.TextField(new Rect(310,165,25,25), clevels[1].ToString()));
		
		if (Popup.List(new Rect (340, 165, 100, 25),ref cshow[2],ref centry[2],cselection[2],classList,listStyle)) //class 3
		{
			cpicked[2] = true;
			cselection[2] = classList[centry[2]];
		}
		clevels[2] = Int32.Parse(GUI.TextField(new Rect(445,165,25,25), clevels[2].ToString()));
	}
}
