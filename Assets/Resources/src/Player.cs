﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class Player
{
	public static GameObject gameObject;

	public static float horizontalSpeed = 0.05f;

	static Player()
	{
		gameObject = new GameObject("Player");
		gameObject.AddComponent<SpriteRenderer>();
		gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/player");
	}
}