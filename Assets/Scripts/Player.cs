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
	RaycastHit2D hit;
	Rigidbody2D r;
	SpriteRenderer sr;

	private void Start()
	{
		r = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
	}

	void Update ()
	{
		r.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, r.velocity.y);
		if (!walking) StartCoroutine(WalkAnim());


		if (Input.GetAxis("Horizontal") > 0)
		{
			sr.flipX = false;
		}
		else if (Input.GetAxis("Horizontal") < 0)
		{
			sr.flipX = true;
		}
		else
		{
			sr.sprite = idleSprite;
			animationFrame = 0;
		}

		hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down);

		if(hit.distance < 1.9f) grounded = true;
		else grounded = false;

		if (grounded)
		{
			if (Input.GetKeyDown(jumpKey)) r.velocity = new Vector2(r.velocity.x, jumpForce);
		}
	}

	IEnumerator WalkAnim()
	{
		walking = true;
		sr.sprite = walkSprites[animationFrame];
		yield return new WaitForSeconds(0.075f);
		animationFrame++;
		if (animationFrame >= walkSprites.Length) animationFrame = 0;
		walking = false;
	}
}
