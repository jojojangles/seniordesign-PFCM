using UnityEngine;
using System.Collections;
using PFCM;

public class Customize : MonoBehaviour {
	private int _characterMeshIndex = 0;
	private int _chestArmorIndex = 0;

	private string _characterModelName = "Human";
	private string _chestArmorName = "None";

	private CharacterAssets cas;
	private PlayerCharacter pc;

	private GameObject chestArmorMesh;

	private GameObject _raceHead; // human = 0, elf, dwarf, orc, halfling
	private GameObject[] _raceArmor; 
	private GameObject _armor;// clothing = 0, leather, breast, half, full
	private GameObject headMesh, armorMesh;


	void Start()
	{
		cas = GameObject.Find("CharacterAssetManager").GetComponent<CharacterAssets>();
		pc = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();
		_raceHead = cas.raceHeads[0];
		_raceArmor = cas.humanArmor;
		_armor = cas.humanArmor[0];
		InstantiateModels();
	}

	void InstantiateModels()
	{
		pc = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();
		if(pc.chestNode != null)
		{
			for(int i = 0; i < pc.chestNode.transform.childCount; i++)
			{
				GameObject thing = pc.chestNode.transform.GetChild(i).gameObject;
				Destroy(thing);
			}
		}if(pc.headNode != null)
		{
			for(int i = 0; i < pc.headNode.transform.childCount; i++)
			{
				GameObject thing = pc.headNode.transform.GetChild(i).gameObject;
				Destroy(thing);
			}
		}
		headMesh = Instantiate(_raceHead,pc.headNode.transform.position,Quaternion.identity) as GameObject;
		headMesh.transform.parent = pc.headNode.transform;
		headMesh.transform.rotation = new Quaternion(0,0,0,0);
		
		armorMesh = Instantiate(_armor,pc.armorNode.transform.position,Quaternion.identity) as GameObject;
		armorMesh.transform.parent = pc.armorNode.transform;
		armorMesh.transform.rotation = new Quaternion(0,0,0,0);
	}

	public void showAvatar(RACES r, Armor a)
	{
		switch(r)// human = 0, elf, dwarf, orc, halfling
		{
		case(RACES.HUMAN):
			_raceHead = cas.raceHeads[0];
			_raceArmor = cas.humanArmor;
			break;
		case(RACES.ELF):
		case(RACES.HALF_ELF):
			_raceHead = cas.raceHeads[1];
			_raceArmor = cas.elfArmor;
			break;
		case(RACES.DWARF):
			_raceHead = cas.raceHeads[2];
			_raceArmor = cas.dwarfArmor;
			break;
		case(RACES.HALF_ORC):
			_raceHead = cas.raceHeads[3];
			_raceArmor = cas.orcArmor;
			break;
		case(RACES.HALFLING):
		case(RACES.GNOME):
			_raceHead = cas.raceHeads[4];
			_raceArmor = cas.halflingArmor;
			break;
		}


		switch(a.name())// clothing = 0, leather, breast, half, full
		{
		case("Clothes"):
			_armor = _raceArmor[0];
			break;
		case("Leather"):
			_armor = _raceArmor[1];
			break;
		case("Breastplate"):
			_armor = _raceArmor[2];
			break;
		case("Half Plate"):
			_armor = _raceArmor[3];
			break;
		case("Full Plate"):
			_armor = _raceArmor[4];
			break;
		}

		InstantiateModels();
	}
}
