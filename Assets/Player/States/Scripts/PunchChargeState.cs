using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/PunchCharge")]
public class PunchChargeState : State {
	private PlayerController _controller;
	public float moveSpeed = 2f;

	public override void Initialize (Controller owner){
		_controller = (PlayerController)owner;
	}

	public override void Enter (){
		_controller.moveSpeed = moveSpeed;
	}

	public override void FixedUpdate (){
		_controller.UpdateMovement ();
	}

}
