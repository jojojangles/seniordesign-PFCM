﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PFCM;

[System.Serializable]
public class Character {
	private string playerName;
	private string characterName;
	private ALIGNMENT align;
	private RACES race;
	private CLASSES[] classes;
	private int[] levels;
	private Dictionary<ABILITY_SCORES,int> ability_BASE;
	private Dictionary<SKILLS,int> skill_BASE;
	private Dictionary<BONUS_TYPES, Dictionary<ABILITY_SCORES,int>> ability_BONUS;
	private Dictionary<BONUS_TYPES, Dictionary<SKILLS,int>> skill_BONUS;
	private ABILITY_SCORES hfave;

	public Character()
	{		
		playerName = "New Player";
		characterName = "New Character";
		align = ALIGNMENT.NEUTRAL_NEUTRAL;
		race = RACES.HUMAN;
		classes = new CLASSES[3];
		levels = new int[]{0,0,0};
		ability_BASE = new Dictionary<ABILITY_SCORES, int>();
		skill_BASE = new Dictionary<SKILLS, int>();
		ability_BONUS = new Dictionary<BONUS_TYPES, Dictionary<ABILITY_SCORES, int>>();
		skill_BONUS = new Dictionary<BONUS_TYPES, Dictionary<SKILLS, int>>();
		hfave = ABILITY_SCORES.STR;

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

	public CLASSES charClass(int c) {return classes[c];}
	public void charClass(int c, CLASSES cl) {classes[c] = cl;}
	public int classLevel(int c) {return levels[c];}
	public void classLevel(int c, int l) {levels[c] = l;}

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

	public Dictionary<ABILITY_SCORES,int> racialAbs()
	{
		Dictionary<ABILITY_SCORES,int> d;
		switch(race)
		{
		case RACES.DWARF:
			d = new Dictionary<ABILITY_SCORES,int>();
			d[ABILITY_SCORES.STR] = 0;
			d[ABILITY_SCORES.DEX] = 0;
			d[ABILITY_SCORES.CON] = 2;
			d[ABILITY_SCORES.INT] = 0;
			d[ABILITY_SCORES.WIS] = 2;
			d[ABILITY_SCORES.CHA] = -2;
			break;
		case RACES.ELF:
			d = new Dictionary<ABILITY_SCORES,int>();
			d[ABILITY_SCORES.STR] = 0;
			d[ABILITY_SCORES.DEX] = 2;
			d[ABILITY_SCORES.CON] = -2;
			d[ABILITY_SCORES.INT] = 2;
			d[ABILITY_SCORES.WIS] = 0;
			d[ABILITY_SCORES.CHA] = 0;
			break;
		case RACES.GNOME:
			d = new Dictionary<ABILITY_SCORES,int>();
			d[ABILITY_SCORES.STR] = -2;
			d[ABILITY_SCORES.DEX] = 0;
			d[ABILITY_SCORES.CON] = 2;
			d[ABILITY_SCORES.INT] = 0;
			d[ABILITY_SCORES.WIS] = 0;
			d[ABILITY_SCORES.CHA] = 2;
			break;
		case RACES.HALF_ELF:
			d = new Dictionary<ABILITY_SCORES,int>();
			d[ABILITY_SCORES.STR] = 0;
			d[ABILITY_SCORES.DEX] = 0;
			d[ABILITY_SCORES.CON] = 0;
			d[ABILITY_SCORES.INT] = 0;
			d[ABILITY_SCORES.WIS] = 0;
			d[ABILITY_SCORES.CHA] = 0;
			d[hfave] = 2;
			break;
		case RACES.HALF_ORC:
			d = new Dictionary<ABILITY_SCORES,int>();
			d[ABILITY_SCORES.STR] = 0;
			d[ABILITY_SCORES.DEX] = 0;
			d[ABILITY_SCORES.CON] = 0;
			d[ABILITY_SCORES.INT] = 0;
			d[ABILITY_SCORES.WIS] = 0;
			d[ABILITY_SCORES.CHA] = 0;
			d[hfave] = 2;
			break;
		case RACES.HALFLING:
			d = new Dictionary<ABILITY_SCORES,int>();
			d[ABILITY_SCORES.STR] = -2;
			d[ABILITY_SCORES.DEX] = 2;
			d[ABILITY_SCORES.CON] = 0;
			d[ABILITY_SCORES.INT] = 0;
			d[ABILITY_SCORES.WIS] = 0;
			d[ABILITY_SCORES.CHA] = 2;
			break;
		case RACES.HUMAN:
			d = new Dictionary<ABILITY_SCORES,int>();
			d[ABILITY_SCORES.STR] = 0;
			d[ABILITY_SCORES.DEX] = 0;
			d[ABILITY_SCORES.CON] = 0;
			d[ABILITY_SCORES.INT] = 0;
			d[ABILITY_SCORES.WIS] = 0;
			d[ABILITY_SCORES.CHA] = 0;
			d[hfave] = 2;
			break;
		default:
			d = new Dictionary<ABILITY_SCORES,int>();
			d[ABILITY_SCORES.STR] = 0;
			d[ABILITY_SCORES.DEX] = 0;
			d[ABILITY_SCORES.CON] = 0;
			d[ABILITY_SCORES.INT] = 0;
			d[ABILITY_SCORES.WIS] = 0;
			d[ABILITY_SCORES.CHA] = 0;
			break;
		}
		return d;
	}

	public void absFave(ABILITY_SCORES a) {hfave = a;}
	public ABILITY_SCORES absFave() {return hfave;}
}
