﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Air")]
public class AirState : State {
	private PlayerController _controller;
	public float airSpeed = 10f;
	public Sprite phSprite;

	public override void Initialize (Controller owner){
		_controller = (PlayerController)owner;
	}

	public override void Enter (){
		_controller.SetSprite (phSprite);
	}

	public override void Update (){
		if (_controller.IsGrounded ()) {
			_controller.TransitionTo<GroundedState> ();
		}
	}

	public override void FixedUpdate (){
		_controller.rb.AddForce (new Vector2 (Input.GetAxis ("Horizontal") * airSpeed, 0));
		_controller.rb.velocity = new Vector2 (Mathf.Clamp (_controller.rb.velocity.x, -_controller.moveSpeed, _controller.moveSpeed), _controller.rb.velocity.y);
		_controller.TurnPlayer (Input.GetAxisRaw ("Horizontal"), false);
	}

}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      