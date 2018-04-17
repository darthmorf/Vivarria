using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroll : MonoBehaviour {

	public float speedX;
	public float speedY;
	Vector3 oldPos;
	GameObject player;

	private void Start()
	{
		player = GameObject.Find("Player");
		oldPos = player.transform.position;
	}

	// Update is called once per frame
	void Update () {

		float deltaX = player.transform.position.x - oldPos.x;
		float deltaY = player.transform.position.y - oldPos.y;

		Vector3 currentPos = transform.position;
		currentPos.x = currentPos.x + deltaX * speedX;
		currentPos.y = currentPos.y + deltaY * speedY;

		transform.position = currentPos;
		oldPos = player.transform.position;
	}
}
