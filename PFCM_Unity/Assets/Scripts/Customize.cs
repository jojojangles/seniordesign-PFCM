using UnityEngine;
using System.Collections;

public class Customize : MonoBehaviour {
	private int _characterMeshIndex = 0;
	private int _chestArmorIndex = 0;

	private string _characterModelName = "Human";
	private string _chestArmorName = "None";

	private CharacterAssets cas;
	private PlayerCharacter pc;

	private GameObject chestArmorMesh;

	void Start()
	{
		cas = GameObject.Find("CharacterAssetManager").GetComponent<CharacterAssets>();
		InstantiateCharacterMesh();
		pc = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();

		InstantiateChestArmor();
	}
	
	void OnGUI()
	{
		ChangeCharacterMesh();
		ChangeChestArmorMesh();
	}

	void InstantiateCharacterMesh()
	{
		switch(_characterMeshIndex)
		{
		case 1:
			_characterModelName = "Orc";
			break;
		case 2:
			_characterModelName = "Elf";
			break;
		default:
			_characterMeshIndex = 0;
			_characterModelName = "Human";
			break;
		}

		if(transform.childCount > 0)
		{
			for(int i = 0; i < transform.childCount; i++)
			{
				Destroy(transform.GetChild(i).gameObject);
			}
		}

		GameObject mesh = Instantiate(cas.characterMesh[_characterMeshIndex], transform.position, Quaternion.identity) as GameObject;
		mesh.transform.parent = transform;
		mesh.transform.rotation = transform.rotation;
		pc = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();
		InstantiateChestArmor();
	}

	void InstantiateChestArmor()
	{
		if(_chestArmorIndex > cas.chestArmorMesh.Length - 1)
		{
			_chestArmorIndex = 0;
		}
		if(pc.chestNode.transform.childCount > 0)
		{
			for(int i = 0; i < pc.chestNode.transform.childCount; i++)
			{
				Destroy(pc.chestNode.transform.GetChild(i).gameObject);
			}
		}

		switch(_chestArmorIndex)
		{
		case 1:
			_chestArmorName = "Cloth Chest";
			break;
		case 2:
			_chestArmorName = "Leather Chest";
			break;
		case 3:
			_chestArmorName = "Metal Chest";
			break;
		default:
			_chestArmorIndex = 0;
			_chestArmorName = "None";
			break;
		}

		chestArmorMesh = Instantiate(cas.chestArmorMesh[_chestArmorIndex], pc.chestNode.transform.position, Quaternion.identity) as GameObject;
		chestArmorMesh.transform.parent = pc.chestNode.transform;
		chestArmorMesh.transform.rotation = new Quaternion(0,0,0,0);
	}

	void ChangeCharacterMesh()
	{
		if(GUI.Button(new Rect(Screen.width/2 - 60, Screen.height - 30, 120, 30), _characterModelName))
		{
			_characterMeshIndex++;
			InstantiateCharacterMesh();
		}
	}

	void ChangeChestArmorMesh()
	{
		if(GUI.Button(new Rect(0, 0, 120, 30), _chestArmorName))
		{
			_chestArmorIndex++;
			InstantiateChestArmor();
		}
	}
}
