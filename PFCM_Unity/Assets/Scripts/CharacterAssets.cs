using UnityEngine;
using System.Collections;

public class CharacterAssets : MonoBehaviour {
	public GameObject[] characterMesh;
	public GameObject[] chestArmorMesh;

	public GameObject[] raceHeads;
	public GameObject[] humanArmor;
	public GameObject[] elfArmor;
	public GameObject[] orcArmor;
	public GameObject[] dwarfArmor;
	public GameObject[] halflingArmor;

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
