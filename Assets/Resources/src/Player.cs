﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class Player
{
	public static GameObject gameObject;

	public static float horizontalSpeed = 0.05f;
	public static int jumpStrength = 4;

	public Player(float x, float y)
	{
		gameObject = new GameObject("Player");
		gameObject.AddComponent<SpriteRenderer>();
		gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/player");

		gameObject.transform.position = new Vector2(x, y);

		gameObject.AddComponent<Rigidbody2D>();
		gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		gameObject.AddComponent<BoxCollider2D>();
	}
}