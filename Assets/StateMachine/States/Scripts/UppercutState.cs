using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Uppercut")]
public class UppercutState : State {
	private PlayerController _controller;
	public float jumpHeight = 5f;
	public float travelSpeed = 2f;
	private float travelTime;
	private float travelTimer = 0;
	public float exitForce = 2f;
	private Vector3 startPos;
	public Sprite phSprite;

	public override void Initialize (Controller owner){
		_controller = (PlayerController)owner;
	}

	public override void Enter (){
		_controller.SetSprite (phSprite);
		travelTimer = 0;
		travelTime = 2 / travelSpeed;
		startPos = _controller.transform.position;
//		_controller.TransitionTo<JumpState> ();
	}

	public override void Update (){
		if (travelTimer < travelTime) {
			travelTimer += Time.deltaTime;
			_controller.transform.position = Vector3.Lerp (startPos, new Vector3 (startPos.x, startPos.y + jumpHeight), travelTimer/travelTime);
		} else {
			_controller.rb.velocity = new Vector2 (0, _controller.rb.velocity.y);
			_controller.rb.AddForce (Vector2.up * exitForce, ForceMode2D.Impulse);
			_controller.TransitionTo<AirState> ();
		}
	}
}
