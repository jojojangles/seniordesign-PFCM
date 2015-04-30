using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PFCM;

[System.Serializable]
public class Character {
	//char specific
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
	private Dictionary<BONUS_TYPES, int> acBonus;
	private ABILITY_SCORES hfave;

	//char agnostic, rules
	private Dictionary<CLASSES,int> hitdice;
	private Dictionary<CLASSES,int[]> saves; //0 - bad, 1 - good

	//stuff related stuff
	private Dictionary<string,Armor> _armory;
	private Dictionary<EQUIP,Gear> _equipment;

	public Character()
	{		
		playerName = "New Player";
		characterName = "New Character";
		align = ALIGNMENT.NEUTRAL_NEUTRAL;
		race = RACES.HUMAN;
		classes = new CLASSES[3]{CLASSES.NONE,CLASSES.NONE,CLASSES.NONE};
		levels = new int[]{0,0,0};
		ability_BASE = new Dictionary<ABILITY_SCORES, int>();
		skill_BASE = new Dictionary<SKILLS, int>();
		ability_BONUS = new Dictionary<BONUS_TYPES, Dictionary<ABILITY_SCORES, int>>();
		skill_BONUS = new Dictionary<BONUS_TYPES, Dictionary<SKILLS, int>>();
		acBonus = new Dictionary<BONUS_TYPES,int>();
		hfave = ABILITY_SCORES.STR;

		foreach(ABILITY_SCORES abs in ABILITY_SCORES.GetValues(typeof(ABILITY_SCORES))){ability_BASE[abs] = 10;}
		foreach(SKILLS s in SKILLS.GetValues(typeof(SKILLS))){skill_BASE[s] = 0;}
		foreach(BONUS_TYPES b in BONUS_TYPES.GetValues(typeof(BONUS_TYPES)))
		{
			acBonus[b] = 0;
		}

		hitdice = new Dictionary<CLASSES,int>();
		saves = new Dictionary<CLASSES,int[]>();
		hitdice[CLASSES.ALCHEMIST] = 8; saves[CLASSES.ALCHEMIST] = new int[]{1,1,0};
		hitdice[CLASSES.ANTIPALADIN] = 10; saves[CLASSES.ANTIPALADIN] = new int[]{1,0,1};
		hitdice[CLASSES.ARCANIST] = 6; saves[CLASSES.ARCANIST] = new int[]{0,0,1};
		hitdice[CLASSES.BARBARIAN] = 12; saves[CLASSES.BARBARIAN] = new int[]{1,0,0};
		hitdice[CLASSES.BARD] = 8; saves[CLASSES.BARD] = new int[]{0,1,1};
		hitdice[CLASSES.BLOODRAGER] = 10; saves[CLASSES.BLOODRAGER] = new int[]{1,0,0};
		hitdice[CLASSES.BRAWLER] = 10; saves[CLASSES.BRAWLER] = new int[]{1,1,0};
		hitdice[CLASSES.CAVALIER] = 10; saves[CLASSES.CAVALIER] = new int[]{1,0,0};
		hitdice[CLASSES.CLERIC] = 8; saves[CLASSES.CLERIC] = new int[]{1,0,1};
		hitdice[CLASSES.DRUID] = 8; saves[CLASSES.DRUID] = new int[]{1,0,1};
		hitdice[CLASSES.FIGHTER] = 10; saves[CLASSES.FIGHTER] = new int[]{1,0,0};
		hitdice[CLASSES.GUNSLINGER] = 10; saves[CLASSES.GUNSLINGER] = new int[]{1,1,0};
		hitdice[CLASSES.HUNTER] = 8; saves[CLASSES.HUNTER] = new int[]{1,1,0};
		hitdice[CLASSES.INQUISITOR] = 8; saves[CLASSES.INQUISITOR] = new int[]{1,0,1};
		hitdice[CLASSES.INVESTIGATOR] = 8; saves[CLASSES.INVESTIGATOR] = new int[]{0,1,1};
		hitdice[CLASSES.MAGUS] = 8; saves[CLASSES.MAGUS] = new int[]{1,0,1};
		hitdice[CLASSES.MONK] = 8; saves[CLASSES.MONK] = new int[]{1,1,1};
		hitdice[CLASSES.NINJA] = 8; saves[CLASSES.NINJA] = new int[]{0,1,0};
		hitdice[CLASSES.ORACLE] = 8; saves[CLASSES.ORACLE] = new int[]{0,0,1};
		hitdice[CLASSES.PALADIN] = 10; saves[CLASSES.PALADIN] = new int[]{1,0,1};
		hitdice[CLASSES.RANGER] = 10; saves[CLASSES.RANGER] = new int[]{1,1,0};
		hitdice[CLASSES.ROGUE] = 8; saves[CLASSES.ROGUE] = new int[]{0,1,0};
		hitdice[CLASSES.SAMURAI] = 10; saves[CLASSES.SAMURAI] = new int[]{1,0,0};
		hitdice[CLASSES.SHAMAN] = 8; saves[CLASSES.SHAMAN] = new int[]{0,0,1};
		hitdice[CLASSES.SKALD] = 8; saves[CLASSES.SKALD] = new int[]{1,0,1};
		hitdice[CLASSES.SLAYER] = 10; saves[CLASSES.SLAYER] = new int[]{1,1,0};
		hitdice[CLASSES.SORCERER] = 6; saves[CLASSES.SORCERER] = new int[]{0,0,1};
		hitdice[CLASSES.SUMMONER] = 8; saves[CLASSES.SUMMONER] = new int[]{0,0,1};
		hitdice[CLASSES.SWASHBUCKLER] = 10; saves[CLASSES.SWASHBUCKLER] = new int[]{0,1,0};
		hitdice[CLASSES.WARPRIEST] = 8; saves[CLASSES.WARPRIEST] = new int[]{1,0,1};
		hitdice[CLASSES.WITCH] = 6; saves[CLASSES.WITCH] = new int[]{0,0,1};
		hitdice[CLASSES.WIZARD] = 6; saves[CLASSES.WIZARD] = new int[]{0,0,1};
		hitdice[CLASSES.NONE] = 0; saves[CLASSES.NONE] = new int[]{0,0,0};

		_armory = Armor.armory();
		_equipment = new Dictionary<EQUIP, Gear>();
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
			acBonus[BONUS_TYPES.SIZE] = 0;
			break;
		case RACES.ELF:
			d = new Dictionary<ABILITY_SCORES,int>();
			d[ABILITY_SCORES.STR] = 0;
			d[ABILITY_SCORES.DEX] = 2;
			d[ABILITY_SCORES.CON] = -2;
			d[ABILITY_SCORES.INT] = 2;
			d[ABILITY_SCORES.WIS] = 0;
			d[ABILITY_SCORES.CHA] = 0;
			acBonus[BONUS_TYPES.SIZE] = 0;
			break;
		case RACES.GNOME:
			d = new Dictionary<ABILITY_SCORES,int>();
			d[ABILITY_SCORES.STR] = -2;
			d[ABILITY_SCORES.DEX] = 0;
			d[ABILITY_SCORES.CON] = 2;
			d[ABILITY_SCORES.INT] = 0;
			d[ABILITY_SCORES.WIS] = 0;
			d[ABILITY_SCORES.CHA] = 2;
			acBonus[BONUS_TYPES.SIZE] = 1;
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
			acBonus[BONUS_TYPES.SIZE] = 0;
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
			acBonus[BONUS_TYPES.SIZE] = 0;
			break;
		case RACES.HALFLING:
			d = new Dictionary<ABILITY_SCORES,int>();
			d[ABILITY_SCORES.STR] = -2;
			d[ABILITY_SCORES.DEX] = 2;
			d[ABILITY_SCORES.CON] = 0;
			d[ABILITY_SCORES.INT] = 0;
			d[ABILITY_SCORES.WIS] = 0;
			d[ABILITY_SCORES.CHA] = 2;
			acBonus[BONUS_TYPES.SIZE] = 1;
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
			acBonus[BONUS_TYPES.SIZE] = 0;
			break;
		default:
			d = new Dictionary<ABILITY_SCORES,int>();
			d[ABILITY_SCORES.STR] = 0;
			d[ABILITY_SCORES.DEX] = 0;
			d[ABILITY_SCORES.CON] = 0;
			d[ABILITY_SCORES.INT] = 0;
			d[ABILITY_SCORES.WIS] = 0;
			d[ABILITY_SCORES.CHA] = 0;
			acBonus[BONUS_TYPES.SIZE] = 0;
			break;
		}
		return d;
	}

	public void absFave(ABILITY_SCORES a) {hfave = a;}
	public ABILITY_SCORES absFave() {return hfave;}

	public int BAB()
	{
		int BAB = 0;
		for(int i = 0; i < classes.Length; i++)
		{
			
			if(hitdice[classes[i]] == 10 || hitdice[classes[i]] == 12)
			{
				BAB += levels[i];
			}
			else if(hitdice[classes[i]] == 8)
			{
				BAB += (int)(levels[i]*.75);
			}
			else if(hitdice[classes[i]] == 6)
			{
				BAB += (int)(levels[i]*.5);
			}
			else
			{
				BAB += 0;
			}
		}
		return BAB;
	}

	public int[] saveFRW()
	{
		int[] s = new int[]{0,0,0};
		for(int i = 0; i < classes.Length; i++)
		{
			for(int j = 0; j < s.Length; j++)
			{
				if(classes[i] != CLASSES.NONE) s[j] += saves[classes[i]][j] == 1 && levels[i] > 0 ? (int)(2 + levels[i]/2) : (int)(levels[i]/3);
			}
		}
		return s;
	}

	public int hitpoints()
	{
		return (int)(hitdice[classes[0]] +
			hitdice[classes[0]]*(levels[0]-1)/2.0 +
			hitdice[classes[1]]*levels[1]/2.0 +
			hitdice[classes[2]]*levels[2]/2.0);
	}

	public int absMod(ABILITY_SCORES a)
	{
		return (int)((ability_BASE[a]+racialAbs()[a])*.5) - 5;
	}

	public int totlev()
	{
		return levels[0] + levels[1] + levels[2];
	}

	public Dictionary<string,Armor> armory()
	{
		return _armory;
	}

	public Dictionary<EQUIP,Gear> equipment()
	{
		return _equipment;
	}

	public void equip(EQUIP slot, Gear g)
	{
		_equipment[slot] = g;
	}

	public Gear equip(EQUIP slot)
	{
		return _equipment[slot] != null ? _equipment[slot] : new Gear();
	}

	public int ac()
	{
		return 10 + 
			absMod(ABILITY_SCORES.DEX) + 
			acBonus[BONUS_TYPES.DODGE] + 
			acBonus[BONUS_TYPES.DEFLECT] + 
			acBonus[BONUS_TYPES.ARMOR] + 
			acBonus[BONUS_TYPES.SHIELD] + 
			acBonus[BONUS_TYPES.NATARM] + 
			acBonus[BONUS_TYPES.SIZE];
	}

	public int flatac()
	{
		return 10 + 
			acBonus[BONUS_TYPES.DEFLECT] + 
			acBonus[BONUS_TYPES.ARMOR] + 
			acBonus[BONUS_TYPES.SHIELD] + 
			acBonus[BONUS_TYPES.NATARM] + 
			acBonus[BONUS_TYPES.SIZE];
	}

	public int touchac()
	{
		return 10 + 
			absMod(ABILITY_SCORES.DEX) + 
			acBonus[BONUS_TYPES.DODGE] + 
			acBonus[BONUS_TYPES.DEFLECT] +  
			acBonus[BONUS_TYPES.SIZE];
	}
}
