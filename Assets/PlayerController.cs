using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : Controller {
	public LayerMask CollisionLayers;
	public float punchCd = 2;
	public float uppercutCd = 2;
	public float stunCd = 2;

	[HideInInspector]
	public Rigidbody2D rb;
	[HideInInspector]
	public BoxCollider2D col;
	[HideInInspector]
	public SpriteRenderer spriteRenderer;
	[HideInInspector]
	public float punchCharge;
	[HideInInspector]
	public float moveSpeed;

	private Vector2 cursorPos;

	public PlayerHUD pHUD;
	public ScreenHUD sHUD;

	protected override void Initialize (){
		rb = GetComponent<Rigidbody2D> ();
		col = GetComponent<BoxCollider2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	public void UpdateMovement(){
		float horizontal = Input.GetAxis ("Horizontal");
		rb.velocity = new Vector2 (horizontal * moveSpeed, rb.velocity.y);
	}

	public void TurnPlayer(float variable, bool inverted){
		if (!inverted ? variable < 0 : variable > 0)
			transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
		else if (!inverted ? variable > 0 : variable < 0)
			transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
	}

	public void SetSprite(Sprite sprite){
		if (sprite != null)
			spriteRenderer.sprite = sprite;
	}

	public int Direction(){
		if (transform.lossyScale.x <= 0)
			return 1;
		else
			return -1;
	}

	public void TurnHorizontally(){
		transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y,transform.localScale.z);
	}

	public bool IsGrounded(){
		Vector2 position = new Vector2 (col.bounds.center.x, col.bounds.center.y - col.bounds.extents.y);
		List<RaycastHit2D> hits = Physics2D.BoxCastAll(position, col.size, 0.0f, Vector2.down, 0.1f, CollisionLayers).ToList();
		RaycastHit2D[] groundHits = Physics2D.BoxCastAll(position, col.size, 0.0f,
			Vector2.down, 0.1f, CollisionLayers);
		hits.AddRange(groundHits);
		for (int i = 0; i < hits.Count; i++)
		{
			RaycastHit2D safetyHit = Physics2D.Linecast(position, hits[i].point,
				CollisionLayers);
			if (safetyHit.collider != null) hits[i] = safetyHit;
		}

		if (hits.ToArray().Length == 0)
			return false;
		else
			return true;
	}
}
