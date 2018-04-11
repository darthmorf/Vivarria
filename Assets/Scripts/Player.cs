using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed;
	public float jumpForce;

	public KeyCode jumpKey = KeyCode.Space;

	public Sprite[] walkSprites;
	public Sprite idleSprite;

	bool grounded;
	bool walking = false;
	int animationFrame = 0;
	float airSpeedModifier = 1f;
	RaycastHit2D hit;
	Rigidbody2D rb;
	SpriteRenderer sr;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
	}

	void Update ()
	{
		DoJump();
		MoveHorizontal();
	}

	void DoJump()
	{
		LayerMask Tiles = 1 << LayerMask.NameToLayer("Tiles");
		hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, 10f, Tiles);

		if (hit.distance < 1.9f)
		{
			grounded = true;
		}
		else
		{
			grounded = false;
		}

		if (grounded)
		{
			if (Input.GetKeyDown(jumpKey))
			{
				rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			}
			airSpeedModifier = 1f;
		}
		else airSpeedModifier = 0.75f;
	}

	void MoveHorizontal()
	{
		rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * airSpeedModifier, rb.velocity.y);
		if (!walking) StartCoroutine(WalkAnim());

		if (Input.GetAxis("Horizontal") == 0)
		{
			sr.sprite = idleSprite;
			animationFrame = 0;
		}
		else
		{
			sr.flipX = (Input.GetAxis("Horizontal") < 0);
		}
	}

	IEnumerator WalkAnim()
	{
		walking = true;
		sr.sprite = walkSprites[animationFrame];
		yield return new WaitForSeconds(0.05f);
		animationFrame++;
		if (animationFrame >= walkSprites.Length) animationFrame = 0;
		walking = false;
	}
}
