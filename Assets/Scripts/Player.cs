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
		if (r.velocity.x != 0 && !walking)
		{
			StartCoroutine(WalkAnim());
		}
		if (sr.sprite != idleSprite && r.velocity.x == 0)
		{
			sr.sprite = idleSprite;
			animationFrame = 0;
		}


		r.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, r.velocity.y);

		bool startedGoingRight = (Input.GetAxis("Horizontal") > 0 && transform.localScale.x < 0);
		bool startedGoingLeft  = (Input.GetAxis("Horizontal") < 0 && transform.localScale.x > 0);

		if (startedGoingRight || startedGoingLeft)
		{
			Vector3 scale = transform.localScale;
			scale.x = scale.x * -1;
			transform.localScale = scale;
		}

		hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down);
		if(hit.distance < 1f)
		{
			grounded = true;
		}
		else
		{
			grounded = false;
		}

		Debug.Log("grounded: " + grounded);

		if (grounded)
		{
			if (Input.GetKeyDown(jumpKey))
			{
				r.velocity = new Vector2(r.velocity.x, jumpForce);
			}
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
