using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PFCM;

[System.Serializable]
public class Character {
	private string playerName;
	private string characterName;
	private ALIGNMENT align;
	private RACES race;
	private Dictionary<CLASSES, int> classLevels;
	private Dictionary<ABILITY_SCORES,int> ability_BASE;
	private Dictionary<SKILLS,int> skill_BASE;
	private Dictionary<BONUS_TYPES, Dictionary<ABILITY_SCORES,int>> ability_BONUS;
	private Dictionary<BONUS_TYPES, Dictionary<SKILLS,int>> skill_BONUS;

	public Character()
	{		
		playerName = "New Player";
		characterName = "New Character";
		align = ALIGNMENT.NEUTRAL_NEUTRAL;
		race = RACES.HUMAN;
		classLevels = new Dictionary<CLASSES,int>();
		ability_BASE = new Dictionary<ABILITY_SCORES, int>();
		skill_BASE = new Dictionary<SKILLS, int>();
		ability_BONUS = new Dictionary<BONUS_TYPES, Dictionary<ABILITY_SCORES, int>>();
		skill_BONUS = new Dictionary<BONUS_TYPES, Dictionary<SKILLS, int>>();

		foreach(ABILITY_SCORES abs in ABILITY_SCORES.GetValues(typeof(ABILITY_SCORES)))
		{
			ability_BASE.Add(abs,10);
		}

		foreach(SKILLS s in SKILLS.GetValues(typeof(SKILLS)))
		{
			skill_BASE.Add(s, 0);
		}
	}

	public void pname(string name) {playerName = name;}
	public string pname() {return playerName;}
	
	public void cname(string name) {characterName = name;}
	public string cname() {return characterName;}

	public void alignment(ALIGNMENT a) {align = a;}
	public ALIGNMENT alignment() {return align;}

	public void charRace(RACES r) {race = r;}
	public RACES charRace() {return race;}

	public void classLevel(CLASSES c, int level) {classLevels[c] = level;}
	public int classLevel(CLASSES c) {return classLevels[c];}

	public void abilityBase (ABILITY_SCORES abs, int a) {ability_BASE[abs] = a;}
	public int abilityBase (ABILITY_SCORES abs) {return ability_BASE[abs];}

	public void skillBase (SKILLS skl, int s) {skill_BASE[skl] = s;}
	public int skillBase (SKILLS skl) {return skill_BASE[skl];}

	public void abilityBonus (BONUS_TYPES bns, ABILITY_SCORES abs, int a)
	{
		if(a > ability_BONUS[bns][abs]) {ability_BONUS[bns][abs] = a;}
	}
	public int abilityBonus (BONUS_TYPES bns, ABILITY_SCORES abs) {return ability_BONUS[bns][abs];}

	public void skillBonus (BONUS_TYPES bns, SKILLS skl, int s)
	{
		if(s > skill_BONUS[bns][skl]) {skill_BONUS[bns][skl] = s;}
	}
	public int skillBonus (BONUS_TYPES bns, SKILLS skl) {return skill_BONUS[bns][skl];}

	public int absPoints()
	{
		int p = 0;
		foreach(ABILITY_SCORES a in ABILITY_SCORES.GetValues(typeof(ABILITY_SCORES)))
		{
			switch(abilityBase(a))
			{
			case 7:
				p += -4;
				break;
			case 8:
				p += -2;
				break;
			case 9:
				p += -1;
				break;
			case 10:
				break;
			case 11:
				p += 1;
				break;
			case 12:
				p += 2;
				break;
			case 13:
				p += 3;
				break;
			case 14:
				p += 5;
				break;
			case 15:
				p += 7;
				break;
			case 16:
				p += 10;
				break;
			case 17:
				p += 13;
				break;
			case 18:
				p += 17;
				break;
			default:
				break;
			}
		}
		return p;
	}
}
