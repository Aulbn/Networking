using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerControllerOld : MonoBehaviour {

	[Header("Movement")]
	private float moveSpeed;
	[SerializeField]
	private float runSpeed = 1f;
	[SerializeField]
	private float airSpeed = 1f;
	[SerializeField]
	private float jumpForce = 1f;
	[SerializeField]
	private float groundRayLength = 0.2f;
	[Header("Abilities")]
	[SerializeField]
	private float fistChargeSpeed = 1f;
	[SerializeField]
	private float chargeMoveSpeed = 2f;
	public float fistCharge = 0;

	private Vector2 velocity;
	private bool grounded = false;

	private Rigidbody2D rb;
	private BoxCollider2D col;

	private void Start(){
		rb = GetComponent<Rigidbody2D> ();
		col = GetComponent<BoxCollider2D> ();
	}

	private void Update(){
		if (Input.GetKey (KeyCode.J) && grounded) {
			if (fistCharge < 10f)
				fistCharge += fistChargeSpeed * Time.deltaTime;
			else
				fistCharge = 10f;
		} else if (fistCharge > 0) {
			//PUNCH!!
			fistCharge = 0;
		}
	}

	private void FixedUpdate(){
		//Movement
		float horizontal = Input.GetAxis ("Horizontal");
		moveSpeed = grounded ? runSpeed : airSpeed;
		if (fistCharge > 0)
			moveSpeed = chargeMoveSpeed;
		rb.velocity = new Vector2 (horizontal * moveSpeed, rb.velocity.y);

		//Jumping
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (grounded) {
				rb.AddForce (new Vector2 (0, jumpForce), ForceMode2D.Impulse);
				grounded = false;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D colis){
		foreach (ContactPoint2D point in colis.contacts) {
			if (point.collider.CompareTag ("Ground"))
				grounded = true;
		}
	}

}
