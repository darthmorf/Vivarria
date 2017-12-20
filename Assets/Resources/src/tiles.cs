using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Tiles
{
	public static GameObject gameObject;
	public static float tileSize = 0.21f;

	public static void GenerateTiles(float x, float y, int width, int height)
	{
		Vector2 startingPos;
		startingPos.x = x;
		startingPos.y = y;

		//create a parent GameObject for the tiles and position it
		gameObject = new GameObject("tiles");
		gameObject.transform.position = startingPos;

		//generate tileGameObjects
		for (int i = 0; i < height; i++ )
		{
			for (int j = 0; j < width; j++)
			{
				Dirt tile = new Dirt(x, y, gameObject);
				x += tileSize;
			}
			x = startingPos.x;
			y -= tileSize;
		}

		//set the size of the tile box collider
		Vector2 colliderSize = gameObject.AddComponent<BoxCollider2D>().size;
		colliderSize.x = width * tileSize;
		colliderSize.y = height * tileSize;
		gameObject.GetComponent<BoxCollider2D>().size = colliderSize;

		//set the position of the box collider
		Vector2 colliderPos;
		colliderPos.x = (width * tileSize / 2) - tileSize / 2;
		colliderPos.y = (height * tileSize / -2) + tileSize / 2;
		gameObject.GetComponent<BoxCollider2D>().offset = colliderPos;
	}
}

class Tile
{
	public static GameObject gameObject;

	public Tile(float x, float y, string asset, GameObject parent)
	{
		gameObject = new GameObject("tile");
		gameObject.transform.parent = parent.transform;

		//load sprite
		gameObject.AddComponent<SpriteRenderer>();
		gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(asset);

		//add RigidBody and stop all physics movement
		gameObject.AddComponent<Rigidbody2D>();
		gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

		//set position
		Vector2 transform = gameObject.transform.position;
		transform.x = x;
		transform.y = y;
		gameObject.transform.position = transform;
	}
}

class Dirt : Tile
{
	public Dirt(float x, float y, GameObject parent) : base(x, y, "Sprites/Dirt0" + UnityEngine.Random.Range(1, 4), parent) { }
}

