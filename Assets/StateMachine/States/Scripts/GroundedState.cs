using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Ground")]
public class GroundedState : State {
	private PlayerController _controller;
	public float moveSpeed = 6f;
	public float jumpForce = 5f;
	public Sprite phSprite;

	public override void Initialize (Controller owner){
		_controller = (PlayerController)owner;
	}

	public override void Enter (){
		_controller.moveSpeed = moveSpeed;
		_controller.SetSprite (phSprite);
		_controller.pHUD.ResetPunchCharge ();
	}

	public override void Update (){
		if (Input.GetKey (KeyCode.J)) {
			_controller.TransitionTo<PunchChargeState> ();
		}
		if (!_controller.IsGrounded())
			_controller.TransitionTo<JumpState> ();
	}

	public override void FixedUpdate (){
		_controller.UpdateMovement ();
		_controller.TurnPlayer (Input.GetAxisRaw("Horizontal"), false);

		if (Input.GetButtonDown ("Jump") && _controller.IsGrounded()) {
			Debug.Log ("jump: " + jumpForce);
			_controller.rb.AddForce (Vector2.up * jumpForce, ForceMode2D.Impulse);
		}
	}
}
