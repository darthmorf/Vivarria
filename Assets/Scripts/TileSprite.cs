using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSprite : MonoBehaviour {

	public Sprite[] sprites;

	void Start () {
		int spriteIndex = Random.Range(0, sprites.Length - 1);
		gameObject.GetComponent<SpriteRenderer>().sprite = sprites[spriteIndex];
	}

}
