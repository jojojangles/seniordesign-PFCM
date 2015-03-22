using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		//buttons that I need:
		//New Character, Load Character, Save Character
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

		//display: Character Name, Player Name
		Character c = GameObject.FindGameObjectWithTag("stats").GetComponent<CharacterStatTracker>().curChar;
		GUI.Label(new Rect (5, 40, 100, 20), "Character: ");
		c.cname(GUI.TextField (new Rect(70,40,100,20), c.cname()));
		GUI.Label(new Rect (210, 40, 100, 20), "Player: " );
		c.pname(GUI.TextField (new Rect(255,40,100,20), c.pname()));
	}
}
