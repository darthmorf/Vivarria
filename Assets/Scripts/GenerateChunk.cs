using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateChunk : MonoBehaviour {

	public GameObject DirtTile;
	public GameObject GrassTile;
	public GameObject StoneTile;
	public GameObject CoalTile;
	public GameObject IronTile;
	public GameObject GoldTile;
	public GameObject DiamondTile;

	public float chanceCoal;
	public float chanceIron;
	public float chanceGold;
	public float chanceDiamond;

	public int width;
	public float heightMultiplier;
	public int heightOffset;

	public float smoothness;

	[HideInInspector]
	public float seed;

	void Start () {
		Generate();
	}
	
	public void Generate () {
		for (int i = 0; i < width; i++)
		{
			int h = Mathf.RoundToInt(Mathf.PerlinNoise(seed, (i + transform.position.x) / smoothness) * heightMultiplier) + heightOffset;
			for (int j = 0; j < h; j++)
			{
				GameObject selectedTile;
				if( j < h - 4)
				{
					selectedTile = StoneTile;
				}
				else if (j < h - 1)
				{
					selectedTile = DirtTile;
				}
				else
				{
					selectedTile = GrassTile;
				}

				GameObject newTile = Instantiate(selectedTile, new Vector3(i, j), Quaternion.identity);
				newTile.transform.parent = this.gameObject.transform;
				newTile.transform.localPosition = new Vector3(i, j);
			}
		}
		GenerateResources();
	}

	public void GenerateResources()
	{
		foreach (GameObject t in GameObject.FindGameObjectsWithTag("TileStone"))
		{
			if (t.transform.parent == this.gameObject.transform)
			{
				float r = Random.Range(0f, 100f);
				GameObject selectedTile = null;

				if (r <= chanceDiamond)
				{
					selectedTile = DiamondTile;
				}
				else if (r <= chanceGold)
				{
					selectedTile = GoldTile;
				}
				else if (r <= chanceIron)
				{
					selectedTile = IronTile;
				}
				else if (r <= chanceCoal)
				{
					selectedTile = CoalTile;
				}

				if (selectedTile != null)
				{
					GameObject newResourceTile = Instantiate(selectedTile, t.transform.position, Quaternion.identity);
					newResourceTile.transform.parent = this.gameObject.transform;

					Destroy(t);
				}
			}
		}
	}
}
