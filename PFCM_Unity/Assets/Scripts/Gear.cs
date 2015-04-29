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

	public Gear ()
	{}

	public Gear (string name, int price, int weight, bool masterwork)
	{
		_name = name;
		_price = price;
		_weight = weight;
		_masterwork = masterwork;
	}
}

public class Armor : Gear
{
	private int _ac;
	private int _dex;
	private int _checkpen;
	private float _spellfail;
	private bool _speed;

	public Armor()
	{}

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

	}
}

public class Weapon : Gear
{
	private int _damDie;
	private int _numDie;
	private int _minCrit;
	private int _critMult;
}