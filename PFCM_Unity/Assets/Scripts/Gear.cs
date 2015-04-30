using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PFCM;

public class Gear
{
	protected string _name;
	protected int _price;
	protected int _weight;
	protected bool _masterwork;
	protected Dictionary<BONUS_TYPES,int> bs;

	public Gear ()
	{
		_name = "None";
		_price = 0;
		_weight = 0;
		_masterwork = false;
		bs = new Dictionary<BONUS_TYPES, int>();
		foreach(BONUS_TYPES b in BONUS_TYPES.GetValues(typeof(BONUS_TYPES)))
		{
			bs[b] = 0;
		}
	}

	public Gear (string name, int price, int weight, bool masterwork)
	{
		_name = name;
		_price = price;
		_weight = weight;
		_masterwork = masterwork;
	}

	public string name() {return _name;}
	public int bonus(BONUS_TYPES b) {return bs[b];}
}

public class Armor : Gear
{
	private int _ac;
	private int _dex;
	private int _checkpen;
	private float _spellfail;
	private bool _speed;

	public Armor()
	{
		_ac = 0;
		_dex = 10;
		_checkpen = 0;
		_spellfail = 0;
		_speed = false;
	}

	public Armor (
		string name,
		int price,
		int weight,
		bool masterwork,
		int ac,
		int dex,
		int checkpen,
		float spellfail,
		bool speed)
	{
		_name = name;
		_price = price;
		_weight = weight;
		_masterwork = masterwork;
		_ac = ac;
		_dex = dex;
		_checkpen = checkpen;
		_spellfail = spellfail;
		_speed = speed;
		bs[BONUS_TYPES.ARMOR] = ac;

	}

	public static Dictionary<string, Armor> armory() //generates an armory of armor sets to look through
	{
		Armor clothing = new Armor("Clothes",0,0,false,0,10,0,0.0f,false);
		Armor leather = new Armor("Leather",10,15,false,2,6,0,0.10f,false);
		Armor breastplate = new Armor("Breastplate",200,30,false,6,3,4,0.25f,true);
		Armor halfplate = new Armor("Half Plate",600,50,false,8,0,7,0.40f,true);
		Armor fullplate = new Armor("Full Plate",1500,50,false,9,1,6,0.35f,true);
		Dictionary<string, Armor> a = new Dictionary<string,Armor>();
		a[clothing.name()] = clothing;
		a[leather.name ()] = leather;
		a[breastplate.name ()] = breastplate;
		a[halfplate.name()] = halfplate;
		a[fullplate.name ()] = fullplate;
		return a;
	}

	public int ac() {return _ac;}
}

public class Weapon : Gear
{
	private int _damDie;
	private int _numDie;
	private int _minCrit;
	private int _critMult;
}