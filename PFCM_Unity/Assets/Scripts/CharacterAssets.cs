using UnityEngine;
using System.Collections;

public class CharacterAssets : MonoBehaviour {
	public GameObject[] characterMesh;
	public GameObject[] chestArmorMesh;

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
