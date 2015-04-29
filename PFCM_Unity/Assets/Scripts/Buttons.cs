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

	//vars for armor dropdown
	private GUIContent[] armList;
	private bool armshow = false;
	private bool armpicked = false;
	private int armentry = 0;
	private GUIContent armselection;

	//styles for popups and fonts
	private GUIStyle listStyle;
	private GUIStyle greyhl;
	private GUIStyle dexhl;
	private GUIStyle strhl;
	private GUIStyle conhl;
	private GUIStyle inthl;
	private GUIStyle wishl;
	private GUIStyle chahl;

	//skill checks
	private int cAcr, cApp, cBlu, cCli, cDip, cDev, cDis, cEsc, cFly, 
				cHan, cHea, cInt, cLin, cPer, cRid, cSen, cSle, cSpe, cSte,
				cSur, cSwi, cUmd = 0;
	private int sAcr, sApp, sBlu, sCli, sDip, sDev, sDis, sEsc, sFly,
				sHan, sHea, sInt, sLin, sPer, sRid, sSen, sSle, sSpe, sSte,
				sSur, sSwi, sUmd = 0;

	//random bonuses that i need in lots of places
	private int sizeMod = 0;

	// Use this for initialization
	void Start () {
		c = GameObject.FindGameObjectWithTag("stats").GetComponent<CharacterStatTracker>().curChar;
		raceBonus = c.racialAbs();

		listStyle = new GUIStyle(); listStyle.normal.textColor = Color.white;
		greyhl = new GUIStyle(); greyhl.normal.textColor = Color.grey;
		dexhl = new GUIStyle(); dexhl.normal.textColor = Color.yellow;
		strhl = new GUIStyle(); strhl.normal.textColor = Color.red;
		conhl = new GUIStyle(); conhl.normal.textColor = Color.green;
		inthl = new GUIStyle(); inthl.normal.textColor = Color.cyan;
		wishl = new GUIStyle(); wishl.normal.textColor = Color.magenta;
		chahl = new GUIStyle(); chahl.normal.textColor = new Color(1.0f,0.5f,0.0f);

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

		//hp
		GUI_hp();

		//skills
		GUI_skills();

		//armor
		GUI_armor();
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
		GUI.Label(new Rect(400,40,140,25), "Race: ", greyhl);
		if (Popup.List(new Rect (440, 35, 100, 25),ref rshow,ref rentry,rselection,raceList,listStyle))
		{
			rpicked = true;
			rselection = raceList[rentry];
			c.charRace((RACES)Enum.Parse(typeof(RACES),rselection.text));
		}
		GUI.Label(new Rect(550,35,100,25), "Favored: ");
		if (Popup.List(new Rect (610, 35, 40, 25),ref ashow,ref aentry,aselection,absList,listStyle))
		{
			apicked = true;
			aselection = absList[aentry];
			string thing = aselection.text;
			c.absFave((ABILITY_SCORES)Enum.Parse(typeof(ABILITY_SCORES),thing));
		}
		switch(c.charRace())
		{
		case RACES.DWARF:
		case RACES.ELF:
		case RACES.HALF_ELF:
		case RACES.HALF_ORC:
		case RACES.HUMAN:
			sizeMod = 0;
			break;
		case RACES.HALFLING:
		case RACES.GNOME:
			sizeMod = 1;
			break;
		default:
			sizeMod = 0;
			break;
		}
	}

	void GUI_ability()
	{
		GUI.Label(new Rect(25,65,100,25), "Ability Scores");
		GUI.Label(new Rect(150,65,100,25), "Points Spent: " + c.absPoints());

		//LEFT: STR, AGI, CON
		GUI.Label(new Rect(25,85,50,25), "STR: ", strhl);
		int str = Int32.Parse(GUI.TextField(new Rect(60,85,25,25), c.abilityBase(ABILITY_SCORES.STR).ToString()));
		c.abilityBase(ABILITY_SCORES.STR,str);
		GUI.Label(new Rect(85,90,50,25), " + " + raceBonus[ABILITY_SCORES.STR], greyhl);
		GUI.Label(new Rect(110,85,100,25), " = " + (str + raceBonus[ABILITY_SCORES.STR]) + " Mod: " + c.absMod(ABILITY_SCORES.STR));

		GUI.Label(new Rect(25,110,50,25), "DEX: ", dexhl);
		int dex = Int32.Parse(GUI.TextField(new Rect(60,110,25,25), c.abilityBase(ABILITY_SCORES.DEX).ToString()));
		c.abilityBase(ABILITY_SCORES.DEX,dex);
		GUI.Label(new Rect(85,115,50,25), " + " + raceBonus[ABILITY_SCORES.DEX], greyhl);
		GUI.Label(new Rect(110,110,100,25), " = " + (dex + raceBonus[ABILITY_SCORES.DEX]) + " Mod: " + c.absMod(ABILITY_SCORES.DEX));

		GUI.Label(new Rect(25,135,50,25), "CON: ", conhl);
		int con = Int32.Parse(GUI.TextField(new Rect(60,135,25,25), c.abilityBase(ABILITY_SCORES.CON).ToString()));
		c.abilityBase(ABILITY_SCORES.CON,con);
		GUI.Label(new Rect(85,140,50,25), " + " + raceBonus[ABILITY_SCORES.CON], greyhl);
		GUI.Label(new Rect(110,135,100,25), " = " + (con + raceBonus[ABILITY_SCORES.CON]) + " Mod: " + c.absMod(ABILITY_SCORES.CON));

		//RIGHT: INT, WIS, CHA
		GUI.Label(new Rect(200,85,50,25), "INT", inthl);
		int gint = Int32.Parse(GUI.TextField(new Rect(235,85,25,25), c.abilityBase(ABILITY_SCORES.INT).ToString()));
		c.abilityBase(ABILITY_SCORES.INT,gint);
		GUI.Label(new Rect(260,90,50,25), " + " + raceBonus[ABILITY_SCORES.INT], greyhl);
		GUI.Label(new Rect(285,85,100,25), " = " + (gint + raceBonus[ABILITY_SCORES.INT]) + " Mod: " + c.absMod(ABILITY_SCORES.INT));

		GUI.Label(new Rect(200,110,50,25), "WIS", wishl);
		int wis = Int32.Parse(GUI.TextField(new Rect(235,110,25,25), c.abilityBase(ABILITY_SCORES.WIS).ToString()));
		c.abilityBase(ABILITY_SCORES.WIS,wis);
		GUI.Label(new Rect(260,115,50,25), " + " + raceBonus[ABILITY_SCORES.WIS], greyhl);
		GUI.Label(new Rect(285,110,100,25), " = " + (wis + raceBonus[ABILITY_SCORES.WIS]) + " Mod: " + c.absMod(ABILITY_SCORES.WIS));

		GUI.Label(new Rect(200,135,50,25), "CHA", chahl);
		int cha = Int32.Parse(GUI.TextField(new Rect(235,135,25,25), c.abilityBase(ABILITY_SCORES.CHA).ToString()));
		c.abilityBase(ABILITY_SCORES.CHA,cha);
		GUI.Label(new Rect(260,140,50,25), " + " + raceBonus[ABILITY_SCORES.CHA], greyhl);
		GUI.Label(new Rect(285,135,100,25), " = " + (cha + raceBonus[ABILITY_SCORES.CHA]) + " Mod: " + c.absMod(ABILITY_SCORES.CHA));
	}

	void GUI_classInfo()
	{
		GUI.Label (new Rect (5, 165, 100, 25), "Classes: ");
		if (Popup.List(new Rect (70, 165, 100, 25),ref cshow[0],ref centry[0],cselection[0],classList,listStyle)) //class 1
		{
			cpicked[0] = true;
			cselection[0] = classList[centry[0]];
			c.charClass(0,(CLASSES)Enum.Parse(typeof(CLASSES),cselection[0].text));
		}
		clevels[0] = Int32.Parse(GUI.TextField(new Rect(175,165,25,25), clevels[0].ToString()));
		c.classLevel(0,clevels[0]);
		
		if (Popup.List(new Rect (205, 165, 100, 25),ref cshow[1],ref centry[1],cselection[1],classList,listStyle)) //class 2
		{
			cpicked[1] = true;
			cselection[1] = classList[centry[1]];
			c.charClass(1,(CLASSES)Enum.Parse(typeof(CLASSES),cselection[1].text));
		}
		clevels[1] = Int32.Parse(GUI.TextField(new Rect(310,165,25,25), clevels[1].ToString()));
		c.classLevel(1,clevels[1]);
		
		if (Popup.List(new Rect (340, 165, 100, 25),ref cshow[2],ref centry[2],cselection[2],classList,listStyle)) //class 3
		{
			cpicked[2] = true;
			cselection[2] = classList[centry[2]];
			c.charClass(2,(CLASSES)Enum.Parse(typeof(CLASSES),cselection[2].text));
		}
		clevels[2] = Int32.Parse(GUI.TextField(new Rect(445,165,25,25), clevels[2].ToString()));
		c.classLevel(2,clevels[2]);

		GUI.Label(new Rect(5,190,500,25),"BAB = " + c.BAB());
		GUI.Label(new Rect(5,215,500,25),"Fort = " + c.saveFRW()[0]);
		GUI.Label(new Rect(5,240,500,25),"Refl = " + c.saveFRW()[1]);
		GUI.Label(new Rect(5,265,500,25),"Will = " + c.saveFRW()[2]);
	}

	void GUI_hp()
	{
		int hp = c.hitpoints() + c.absMod(ABILITY_SCORES.CON)*c.totlev();
		GUI.Label(new Rect(375,90,100,25), "Hit Points = " + hp); 
	}

	void GUI_skills()
	{
		GUI.Label(new Rect(200,280,200,25), "Skills");
		///////////////////first column////////////////////
		sAcr = c.skillBase(SKILLS.ACROBATICS);
		if(GUI.Button(new Rect(5,305,50,25), "Acrobatics: ", dexhl)) {cAcr = (d20() + sAcr + c.absMod(ABILITY_SCORES.DEX));}
		sAcr = Int32.Parse(GUI.TextField(new Rect(120,305,25,25), sAcr.ToString ()));
		c.skillBase(SKILLS.ACROBATICS, sAcr);
		GUI.Label(new Rect(150,305,100,25), cAcr.ToString ());

		sApp = c.skillBase(SKILLS.APPRAISE);
		if(GUI.Button(new Rect(5,330,50,25), "Appraise: ", inthl)) {cApp = (d20() + sApp + c.absMod(ABILITY_SCORES.INT));}
		sApp = Int32.Parse(GUI.TextField(new Rect(120,330,25,25), sApp.ToString ()));
		c.skillBase(SKILLS.APPRAISE, sApp);
		GUI.Label(new Rect(150,330,100,25), cApp.ToString ());
		
		sBlu = c.skillBase(SKILLS.BLUFF);
		if(GUI.Button(new Rect(5,355,50,25), "Bluff: ", chahl)) {cBlu = (d20() + sBlu + c.absMod(ABILITY_SCORES.CHA));}
		sBlu = Int32.Parse(GUI.TextField(new Rect(120,355,25,25), sBlu.ToString ()));
		c.skillBase(SKILLS.BLUFF, sBlu);
		GUI.Label(new Rect(150,355,100,25), cBlu.ToString ());
		
		sCli = c.skillBase(SKILLS.CLIMB);
		if(GUI.Button(new Rect(5,380,50,25), "Climb: ", strhl)) {cCli = (d20() + sCli + c.absMod(ABILITY_SCORES.STR));}
		sCli = Int32.Parse(GUI.TextField(new Rect(120,380,25,25), sCli.ToString ()));
		c.skillBase(SKILLS.ACROBATICS, sCli);
		GUI.Label(new Rect(150,380,100,25), cCli.ToString ());
		
		sDip = c.skillBase(SKILLS.DIPLOMACY);
		if(GUI.Button(new Rect(5,405,50,25), "Diplomacy: ", chahl)) {cDip = (d20() + sDip + c.absMod(ABILITY_SCORES.CHA));}
		sDip = Int32.Parse(GUI.TextField(new Rect(120,405,25,25), sDip.ToString ()));
		c.skillBase(SKILLS.DIPLOMACY, sDip);
		GUI.Label(new Rect(150,405,100,25), cDip.ToString ());
		
		sDev = c.skillBase(SKILLS.DISABLE_DEVICE);
		if(GUI.Button(new Rect(5,430,50,25), "Disable Device: ", dexhl)) {cDev = (d20() + sDip + c.absMod(ABILITY_SCORES.DEX));}
		sDev = Int32.Parse(GUI.TextField(new Rect(120,430,25,25), sDev.ToString ()));
		c.skillBase(SKILLS.DISABLE_DEVICE, sDev);
		GUI.Label(new Rect(150,430,100,25), cDev.ToString ());
		
		sDis = c.skillBase(SKILLS.DISGUISE);
		if(GUI.Button(new Rect(5,455,50,25), "Disguise: ", chahl)) {cDis = (d20() + sDis + c.absMod(ABILITY_SCORES.CHA));}
		sDis = Int32.Parse(GUI.TextField(new Rect(120,455,25,25), sDis.ToString ()));
		c.skillBase(SKILLS.DISGUISE, sDis);
		GUI.Label(new Rect(150,455,100,25), cDis.ToString ());
		
		sEsc = c.skillBase(SKILLS.ESCAPE_ARTIST);
		if(GUI.Button(new Rect(5,480,50,25), "Escape Artist: ", dexhl)) {cEsc = (d20() + sEsc + c.absMod(ABILITY_SCORES.DEX));}
		sEsc = Int32.Parse(GUI.TextField(new Rect(120,480,25,25), sEsc.ToString ()));
		c.skillBase(SKILLS.ESCAPE_ARTIST, sEsc);
		GUI.Label(new Rect(150,480,100,25), cEsc.ToString ());
		
		sFly = c.skillBase(SKILLS.FLY);
		if(GUI.Button(new Rect(5,505,50,25), "Fly: ", dexhl)) {cFly = (d20() + sFly + c.absMod(ABILITY_SCORES.DEX));}
		sFly = Int32.Parse(GUI.TextField(new Rect(120,505,25,25), sFly.ToString ()));
		c.skillBase(SKILLS.FLY, sFly);
		GUI.Label(new Rect(150,505,100,25), cFly.ToString ());
		
		sHan = c.skillBase(SKILLS.HANDLE_ANIMAL);
		if(GUI.Button(new Rect(5,530,50,25), "Handle Animal: ", chahl)) {cHan = (d20() + sHan + c.absMod(ABILITY_SCORES.CHA));}
		sHan = Int32.Parse(GUI.TextField(new Rect(120,530,25,25), sHan.ToString ()));
		c.skillBase(SKILLS.HANDLE_ANIMAL, sHan);
		GUI.Label(new Rect(150,530,100,25), cHan.ToString ());
		
		sHea = c.skillBase(SKILLS.HEAL);
		if(GUI.Button(new Rect(5,555,50,25), "Heal: ", wishl)) {cHea = (d20() + sHea + c.absMod(ABILITY_SCORES.WIS));}
		sHea = Int32.Parse(GUI.TextField(new Rect(120,555,25,25), sHea.ToString ()));
		c.skillBase(SKILLS.HEAL, sHea);
		GUI.Label(new Rect(150,555,100,25), cHea.ToString ());

		///////////////second column//////////////////////////
		sInt = c.skillBase(SKILLS.INTIMIDATE);
		if(GUI.Button(new Rect(180,305,50,25), "Intimidate: ", chahl)) {cInt = (d20() + sInt + c.absMod(ABILITY_SCORES.CHA));}
		sInt = Int32.Parse(GUI.TextField(new Rect(295,305,25,25), sInt.ToString ()));
		c.skillBase(SKILLS.INTIMIDATE, sInt);
		GUI.Label(new Rect(325,305,100,25), cInt.ToString ());
		
		sLin = c.skillBase(SKILLS.LINGUISTICS);
		if(GUI.Button(new Rect(180,330,50,25), "Linguistics: ", inthl)) {cLin = (d20() + sLin + c.absMod(ABILITY_SCORES.INT));}
		sLin = Int32.Parse(GUI.TextField(new Rect(295,330,25,25), sLin.ToString ()));
		c.skillBase(SKILLS.LINGUISTICS, sLin);
		GUI.Label(new Rect(325,330,100,25), cLin.ToString ());
		
		sPer = c.skillBase(SKILLS.PERCEPTION);
		if(GUI.Button(new Rect(180,355,50,25), "Perception: ", wishl)) {cPer = (d20() + sPer + c.absMod(ABILITY_SCORES.WIS));}
		sPer = Int32.Parse(GUI.TextField(new Rect(295,355,25,25), sPer.ToString ()));
		c.skillBase(SKILLS.PERCEPTION, sPer);
		GUI.Label(new Rect(325,355,100,25), cPer.ToString ());
		
		sRid = c.skillBase(SKILLS.RIDE);
		if(GUI.Button(new Rect(180,380,50,25), "Ride: ", dexhl)) {cRid = (d20() + sRid + c.absMod(ABILITY_SCORES.DEX));}
		sRid = Int32.Parse(GUI.TextField(new Rect(295,380,25,25), sRid.ToString ()));
		c.skillBase(SKILLS.RIDE, sRid);
		GUI.Label(new Rect(325,380,100,25), cRid.ToString ());
		
		sSen = c.skillBase(SKILLS.SENSE_MOTIVE);
		if(GUI.Button(new Rect(180,405,50,25), "Sense Motive: ", chahl)) {cSen = (d20() + sSen + c.absMod(ABILITY_SCORES.CHA));}
		sSen = Int32.Parse(GUI.TextField(new Rect(295,405,25,25), sSen.ToString ()));
		c.skillBase(SKILLS.SENSE_MOTIVE, sSen);
		GUI.Label(new Rect(325,405,100,25), cSen.ToString ());
		
		sSle = c.skillBase(SKILLS.SLEIGHT_OF_HAND);
		if(GUI.Button(new Rect(180,430,50,25), "Sleight of Hand: ", dexhl)) {cSle = (d20() + sSle + c.absMod(ABILITY_SCORES.DEX));}
		sSle = Int32.Parse(GUI.TextField(new Rect(295,430,25,25), sSle.ToString ()));
		c.skillBase(SKILLS.SLEIGHT_OF_HAND, sSle);
		GUI.Label(new Rect(325,430,100,25), cSle.ToString ());
		
		sSpe = c.skillBase(SKILLS.SPELLCRAFT);
		if(GUI.Button(new Rect(180,455,50,25), "Spellcraft: ", inthl)) {cSpe = (d20() + sSpe + c.absMod(ABILITY_SCORES.INT));}
		sSpe = Int32.Parse(GUI.TextField(new Rect(295,455,25,25), sSpe.ToString ()));
		c.skillBase(SKILLS.SPELLCRAFT, sSpe);
		GUI.Label(new Rect(325,455,100,25), cSpe.ToString ());
		
		sSte = c.skillBase(SKILLS.STEALTH);
		if(GUI.Button(new Rect(180,480,50,25), "Stealth: ", dexhl)) {cSte = (d20() + sSte + c.absMod(ABILITY_SCORES.DEX));}
		sSte = Int32.Parse(GUI.TextField(new Rect(295,480,25,25), sSte.ToString ()));
		c.skillBase(SKILLS.STEALTH, sSte);
		GUI.Label(new Rect(325,480,100,25), cSte.ToString ());
		
		sSur = c.skillBase(SKILLS.SURVIVAL);
		if(GUI.Button(new Rect(180,505,50,25), "Survival: ", wishl)) {cSur = (d20() + sSur + c.absMod(ABILITY_SCORES.WIS));}
		sSur = Int32.Parse(GUI.TextField(new Rect(295,505,25,25), sSur.ToString ()));
		c.skillBase(SKILLS.SURVIVAL, sSur);
		GUI.Label(new Rect(325,505,100,25), cSur.ToString ());
		
		sSwi = c.skillBase(SKILLS.SWIM);
		if(GUI.Button(new Rect(180,530,50,25), "Swim: ", strhl)) {cSwi = (d20() + sSwi + c.absMod(ABILITY_SCORES.STR));}
		sSwi = Int32.Parse(GUI.TextField(new Rect(295,530,25,25), sSwi.ToString ()));
		c.skillBase(SKILLS.SWIM, sSwi);
		GUI.Label(new Rect(325,530,100,25), cSwi.ToString ());
		
		sUmd = c.skillBase(SKILLS.USE_MAGIC_DEVICE);
		if(GUI.Button(new Rect(180,555,50,25), "Use Magic Device: ", chahl)) {cUmd = (d20() + sUmd + c.absMod(ABILITY_SCORES.CHA));}
		sUmd = Int32.Parse(GUI.TextField(new Rect(295,555,25,25), sUmd.ToString ()));
		c.skillBase(SKILLS.USE_MAGIC_DEVICE, sUmd);
		GUI.Label(new Rect(325,555,100,25), cUmd.ToString ());
	}

	void GUI_armor()
	{
		if (Popup.List(new Rect (600, 600, 40, 25),ref armshow,ref armentry,armselection,armList,listStyle))
		{
			armpicked = true;
			armselection = armList[aentry];
			string thing = armselection.text;
			//c.absFave((ABILITY_SCORES)Enum.Parse(typeof(ABILITY_SCORES),thing));
		}
	}

	int d20()
	{
		return (int)(UnityEngine.Random.Range(1.0f,21.0f));
	}
}
