using Assets.Resources.src;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		//create player class (and therefore gameobject)
		Player player = new Player();
		//set the camera parent to the player so it follows the player
		GetComponent<Camera>().transform.parent = Player.gameObject.transform;

		Tiles.GenerateTiles(0, -0.45f, 100, 100);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Keyboard Events
		if (Input.GetKey("a")) //Move Left
		{
			Vector3 transform = Player.gameObject.transform.position;
			transform.x -= Player.horizontalSpeed;
			Player.gameObject.transform.position = transform;
		}

		if (Input.GetKey("d")) //Move Right
		{
			Vector3 transform = Player.gameObject.transform.position;
			transform.x += Player.horizontalSpeed;
			Player.gameObject.transform.position = transform;
		}

		if (Input.GetKeyDown("space"))
		{
			Player.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Player.jumpStrength), ForceMode2D.Impulse);
		}
	}
}
