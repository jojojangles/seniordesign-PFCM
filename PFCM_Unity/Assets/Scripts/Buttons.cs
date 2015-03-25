using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using PFCM;

public class Buttons : MonoBehaviour {
	Character c; 
	Dictionary<ABILITY_SCORES,int> raceBonus;

	//vars for classes-dropdown
	private GUIContent[] classList;
	private bool[] cshow = {false, false, false};
	private bool[] cpicked = {false, false, false};
	private int[] centry = {0, 0, 0};
	private GUIContent[] cselection;
	private int[] clevels = {0, 0, 0};

	//vars for races-dropdown
	private GUIContent[] raceList;
	private bool rshow = false;
	private bool rpicked = false;
	private int rentry = 0;
	private GUIContent rselection;

	//vars for ability scores-dropdown
	private GUIContent[] absList;
	private bool ashow = false;
	private bool apicked = false;
	private int aentry = 0;
	private GUIContent aselection;

	private GUIStyle listStyle;
	private GUIStyle bluehl;
	private GUIStyle yellhl;
	private GUIStyle redhl;

	// Use this for initialization
	void Start () {
		c = GameObject.FindGameObjectWithTag("stats").GetComponent<CharacterStatTracker>().curChar;
		raceBonus = c.racialAbs();

		listStyle = new GUIStyle(); listStyle.normal.textColor = Color.white;
		bluehl = new GUIStyle(); bluehl.normal.textColor = Color.cyan;
		yellhl = new GUIStyle(); yellhl.normal.textColor = Color.yellow;
		redhl = new GUIStyle(); redhl.normal.textColor = Color.red;

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

		ABILITY_SCORES[] atemp = (ABILITY_SCORES[])Enum.GetValues(typeof(ABILITY_SCORES));
		absList = new GUIContent[atemp.Length];
		for(int i = 0; i < atemp.Length; i++)
		{
			absList[i] = new GUIContent(atemp[i].ToString());
		}
		aselection = new GUIContent("ATB");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		//just in case....
		c = GameObject.FindGameObjectWithTag("stats").GetComponent<CharacterStatTracker>().curChar;
		raceBonus = c.racialAbs();

		//buttons that I need:
		//New Character, Load Character, Save Character
		GUI_saveLoad();

		//display: Character Name, Player Name
		GUI_names();

		//race select
		GUI_race();

		//ability scores
		GUI_ability();

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
		GUI.Label(new Rect (5, 35, 100, 25), "Character: ");
		c.cname(GUI.TextField (new Rect(70,35,135,25), c.cname()));
		GUI.Label(new Rect (210, 35, 100, 25), "Player: " );
		c.pname(GUI.TextField (new Rect(255,35,135,25), c.pname()));
	}

	void GUI_race()
	{
		GUI.Label(new Rect(400,40,140,25), "Race: ", bluehl);
		if (Popup.List(new Rect (440, 35, 100, 25),ref rshow,ref rentry,rselection,raceList,listStyle)) //class 2
		{
			rpicked = true;
			rselection = raceList[rentry];
		}
		GUI.Label(new Rect(550,35,100,25), "Favored: ");
		if (Popup.List(new Rect (610, 35, 40, 25),ref ashow,ref aentry,aselection,absList,listStyle)) //class 2
		{
			apicked = true;
			aselection = absList[aentry];
			string thing = aselection.text;
			c.absFave((ABILITY_SCORES)Enum.Parse(typeof(ABILITY_SCORES),thing));
		}
	}

	void GUI_ability()
	{
		GUI.Label(new Rect(25,65,100,25), "Ability Scores");
		GUI.Label(new Rect(150,65,100,25), "Points Spent: " + c.absPoints());

		//LEFT: STR, AGI, CON
		GUI.Label(new Rect(25,85,50,25), "STR: ");
		int str = Int32.Parse(GUI.TextField(new Rect(60,85,25,25), c.abilityBase(ABILITY_SCORES.STR).ToString()));
		c.abilityBase(ABILITY_SCORES.STR,str);
		GUI.Label(new Rect(85,85,50,25), " + " + raceBonus[ABILITY_SCORES.STR]);

		GUI.Label(new Rect(25,110,50,25), "DEX: ");
		int dex = Int32.Parse(GUI.TextField(new Rect(60,110,25,25), c.abilityBase(ABILITY_SCORES.DEX).ToString()));
		c.abilityBase(ABILITY_SCORES.DEX,dex);
		GUI.Label(new Rect(85,110,50,25), " + " + raceBonus[ABILITY_SCORES.DEX]);

		GUI.Label(new Rect(25,135,50,25), "CON: ");
		int con = Int32.Parse(GUI.TextField(new Rect(60,135,25,25), c.abilityBase(ABILITY_SCORES.CON).ToString()));
		c.abilityBase(ABILITY_SCORES.CON,con);
		GUI.Label(new Rect(85,135,50,25), " + " + raceBonus[ABILITY_SCORES.CON]);

		//RIGHT: INT, WIS, CHA
		GUI.Label(new Rect(200,85,50,25), "INT");
		int gint = Int32.Parse(GUI.TextField(new Rect(235,85,25,25), c.abilityBase(ABILITY_SCORES.INT).ToString()));
		c.abilityBase(ABILITY_SCORES.INT,gint);
		GUI.Label(new Rect(260,85,50,25), " + " + raceBonus[ABILITY_SCORES.INT]);

		GUI.Label(new Rect(200,110,50,25), "WIS");
		int wis = Int32.Parse(GUI.TextField(new Rect(235,110,25,25), c.abilityBase(ABILITY_SCORES.WIS).ToString()));
		c.abilityBase(ABILITY_SCORES.WIS,wis);
		GUI.Label(new Rect(260,110,50,25), " + " + raceBonus[ABILITY_SCORES.WIS]);

		GUI.Label(new Rect(200,135,50,25), "CHA");
		int cha = Int32.Parse(GUI.TextField(new Rect(235,135,25,25), c.abilityBase(ABILITY_SCORES.CHA).ToString()));
		c.abilityBase(ABILITY_SCORES.CHA,cha);
		GUI.Label(new Rect(260,135,50,25), " + " + raceBonus[ABILITY_SCORES.CHA]);
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
