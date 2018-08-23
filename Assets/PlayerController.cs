using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : Controller {
	public LayerMask CollisionLayers;
	public float moveSpeed;

	[HideInInspector]
	public Rigidbody2D rb;
	[HideInInspector]
	public BoxCollider2D col;

	private void Start(){
		rb = GetComponent<Rigidbody2D> ();
		col = GetComponent<BoxCollider2D> ();
	}

	public void UpdateMovement(){
		float horizontal = Input.GetAxis ("Horizontal");
		rb.velocity = new Vector2 (horizontal * moveSpeed, rb.velocity.y);
	}

	public bool IsGrounded(){
//		Vector2 position = transform.position + (Vector3) col.offset;
//		List<RaycastHit2D> hits = Physics2D.BoxCastAll(position, col.size, 0.0f, Vector2.down, 0.1f, CollisionLayers).ToList();
//		RaycastHit2D[] groundHits = Physics2D.BoxCastAll(position, col.size, 0.0f,
//			Vector2.down, 0.1f, CollisionLayers);
//		hits.AddRange(groundHits);
//		for (int i = 0; i < hits.Count; i++)
//		{
//			RaycastHit2D safetyHit = Physics2D.Linecast(position, hits[i].point,
//				CollisionLayers);
//			if (safetyHit.collider != null) hits[i] = safetyHit;
//		}
//
//		if (hits.ToArray().Length == 0)
//			return false;
//		else
			return true;
	}

}
