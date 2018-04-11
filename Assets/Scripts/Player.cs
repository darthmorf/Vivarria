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
	bool tileCooldown = false;
	int animationFrame = 0;
	float airSpeedModifier = 1f;
	RaycastHit2D hit;
	Rigidbody2D rb;
	SpriteRenderer sr;
	LayerMask tiles;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		tiles = 1 << LayerMask.NameToLayer("Tiles");
	}

	void Update ()
	{
		Jump();
		MoveHorizontal();
		AutoStep();
		TileBreak();
	}

	void Jump()
	{
		hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, 10f, tiles);

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

	void TileBreak()
	{
		if (Input.GetMouseButton(0) && !tileCooldown)
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			RaycastHit2D tileclick = Physics2D.Raycast(transform.position, mousePos - transform.position, 5f, tiles);
			if (tileclick.collider != null && tileclick.collider.gameObject.layer == 9)
			{
				Destroy(tileclick.collider.gameObject);
				StartCoroutine(KillTileCooldown());
			}
		}
	}

	void AutoStep()
	{
		float rayDistance = 0.8f;
		RaycastHit2D rightBottom = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1f), Vector2.right, rayDistance, tiles);
		RaycastHit2D rightMid = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, rayDistance, tiles);
		RaycastHit2D rightTop = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1f), Vector2.right, rayDistance, tiles);
		RaycastHit2D rightFar = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 2f), Vector2.right, rayDistance * 2, tiles);

		RaycastHit2D leftBottom = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1f), Vector2.left, rayDistance, tiles);
		RaycastHit2D leftMid = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, rayDistance, tiles);
		RaycastHit2D leftTop = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1f), Vector2.left, rayDistance, tiles);
		RaycastHit2D leftFar = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 2f), Vector2.left, rayDistance * 2, tiles);

		bool collidedRight = (rightBottom.collider != null && rightMid.collider == null && rightTop.collider == null && rightFar.collider == null && rightBottom.collider.gameObject.layer == 9);
		bool collidedLeft = (leftBottom.collider != null && leftMid.collider == null && leftTop.collider == null && leftFar.collider == null && leftBottom.collider.gameObject.layer == 9);

		if (collidedRight || collidedLeft)
		{
			Vector2 vec = transform.position;
			vec.y += 1.2f;

			if (collidedRight) vec.x += 0.3f;
			else vec.x -= 0.3f;

			transform.position = vec;
		}
	}

	IEnumerator KillTileCooldown()
	{
		tileCooldown = true;
		yield return new WaitForSeconds(0.25f);
		tileCooldown = false;
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
