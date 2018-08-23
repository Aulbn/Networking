using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Ground")]
public class GroundedState : State {
	private PlayerController _controller;
	public float moveSpeed = 6f;
	public float jumpForce = 5f;

	public override void Initialize (Controller owner){
		_controller = (PlayerController)owner;
	}

	public override void Enter (){
		_controller.moveSpeed = moveSpeed;
	}

	public override void Update (){
		Debug.Log (_controller.IsGrounded ());
	}

	public override void FixedUpdate (){
		_controller.UpdateMovement ();

		if (Input.GetButtonDown ("Jump")) {
			Debug.Log ("jump: " + jumpForce);
			_controller.rb.AddForce (Vector2.up * jumpForce, ForceMode2D.Impulse);
//			_controller.TransitionTo<> ();
		}
	}
}
