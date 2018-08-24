using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/PunchCharge")]
public class PunchChargeState : State {
	private PlayerController _controller;
	public float moveSpeed = 2f;
	public float chargeTime = 3f;
	private float chargeTimer = 0;
	public float forcedReleaseTime = 2f;
	private float forcedReleaseTimer = 0;
	public Sprite phSprite;

	public override void Initialize (Controller owner){
		_controller = (PlayerController)owner;
	}

	public override void Enter (){
		_controller.moveSpeed = moveSpeed;
		_controller.SetSprite (phSprite);
		_controller.transform.localScale = new Vector3 (-_controller.transform.localScale.x, _controller.transform.localScale.y, _controller.transform.localScale.z);
		chargeTimer = 0;
		forcedReleaseTimer = 0;
	}

	public override void Update (){
		if (chargeTimer < chargeTime) {
			chargeTimer += Time.deltaTime;
			_controller.pHUD.SetPunchCharge (chargeTimer / chargeTime);
		} else if (forcedReleaseTimer < forcedReleaseTime){
			chargeTimer = chargeTime;
			forcedReleaseTimer += Time.deltaTime;
		} else
			Punch ();

		if (Input.GetKeyUp (KeyCode.J)) {
			Punch ();
		}
	}

	public override void FixedUpdate (){
		_controller.UpdateMovement ();
		_controller.TurnPlayer (Input.GetAxisRaw("Horizontal"), true);

	}

	private void Punch(){
		_controller.punchCharge = chargeTimer / chargeTime;
		_controller.TransitionTo<PunchState> ();
	}

}
