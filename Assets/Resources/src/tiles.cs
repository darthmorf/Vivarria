using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ClipperLib;

class Tiles
{
	public static GameObject gameObject;
	public static List<List<IntPoint>> paths = new List<List<IntPoint>>();
	public const float scalingFactor = 1000.0f;

	public static void GenerateTiles(float x, float y, int width, int height)
	{
		Vector2 startingPos = new Vector2(x, y);
		float tileHeight = 0.0f;

		//create a parent GameObject for the tiles and position it
		gameObject = new GameObject("tiles");
		gameObject.transform.position = startingPos;

		//generate tileGameObjects
		for (int i = 0; i < height; i++ )
		{
			for (int j = 0; j < width; j++)
			{
				Dirt tile = new Dirt(x, y, gameObject);
				paths.Add(tile.path);
				x += tile.gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
				tileHeight = tile.gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
			}
			x = startingPos.x;
			y -= tileHeight;
		}

		updateCollisionBox();
	}

	public static void updateCollisionBox() //takes the list of collision polygons and constructs a master polygon, then master collision polygon
	{
		List<List<IntPoint>> combinedPaths = new List<List<IntPoint>>();

		Clipper c = new Clipper();
		//add polygons to clipper
		c.AddPaths(paths, PolyType.ptSubject, true);
		//simplify polygons
		c.Execute(ClipType.ctUnion, combinedPaths, PolyFillType.pftNonZero);

		PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();

		List<Vector2> vectorPoints = new List<Vector2>();
		//convert Clipper IntPoint lists into Unity Vector2 Array
		foreach (List<IntPoint> path in combinedPaths)
		{
			foreach (IntPoint point in path)
			{
				Vector2 p = new Vector2(point.X / scalingFactor, point.Y / scalingFactor);
				vectorPoints.Add(p);
			}
		}
		//update collider size
		collider.SetPath(0, vectorPoints.ToArray());

		//for some reason, which I can't discern, the collision box is always offset by this amount so we'll do a dirty fix and correct it for now
	}
}

class Tile
{
	public GameObject gameObject;
	public List<IntPoint> path = new List<IntPoint>();

	public Tile(float x, float y, string asset, GameObject parent)
	{
		gameObject = new GameObject("tile");
		gameObject.transform.parent = parent.transform;

		//load sprite
		SpriteRenderer spriteRendr = gameObject.AddComponent<SpriteRenderer>();
		spriteRendr.sprite = Resources.Load<Sprite>(asset);

		//add RigidBody and stop all physics movement
		Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
		rb.constraints = RigidbodyConstraints2D.FreezeAll;

		//set position
		Vector2 transform = gameObject.transform.position;
		transform.x = x;
		transform.y = y;
		gameObject.transform.position = transform;														 
																										 
		//create path that defines collision bounds														 
		path.Add(new IntPoint((int)(spriteRendr.bounds.min.x * Tiles.scalingFactor), (int)(spriteRendr.bounds.min.y * Tiles.scalingFactor)));	 
		path.Add(new IntPoint((int)(spriteRendr.bounds.max.x * Tiles.scalingFactor), (int)(spriteRendr.bounds.min.y * Tiles.scalingFactor)));	 
		path.Add(new IntPoint((int)(spriteRendr.bounds.max.x * Tiles.scalingFactor), (int)(spriteRendr.bounds.max.y * Tiles.scalingFactor)));	 
		path.Add(new IntPoint((int)(spriteRendr.bounds.min.x * Tiles.scalingFactor), (int)(spriteRendr.bounds.max.y * Tiles.scalingFactor)));
		path.Add(new IntPoint((int)(spriteRendr.bounds.min.x * Tiles.scalingFactor), (int)(spriteRendr.bounds.min.y * Tiles.scalingFactor)));
	}
}

class Dirt : Tile
{
	public Dirt(float x, float y, GameObject parent) : base(x, y, "Sprites/Dirt0" + UnityEngine.Random.Range(1, 4), parent) { }
}

