using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Punch")]
public class PunchState : State {
	private PlayerController _controller;
	public float punchLength = 2f;
	public MinMaxFloat punchSpeed;
	private float _punchSpeed;
	private float punchTimer = 0;
	private Vector3 startPos;
	public Sprite phSprite;
	private Vector3 dir;

	public override void Initialize (Controller owner){
		_controller = (PlayerController)owner;
	}

	public override void Enter (){
		_controller.SetSprite (phSprite);
		startPos = _controller.transform.position;
		punchTimer = 0;
		_punchSpeed = Mathf.Lerp (punchSpeed.Min, punchSpeed.Max, _controller.punchCharge);
		dir = _controller.sHUD.AimDireciton ();
		_controller.TurnPlayer (dir.x / Mathf.Abs (dir.x), true);
	}

	public override void Update (){
		if (punchTimer < _punchSpeed) {
			punchTimer += Time.deltaTime;
			_controller.transform.position = Vector3.Lerp (startPos, dir * punchLength * _controller.punchCharge, punchTimer/_punchSpeed);
		}
		else {
			_controller.TurnHorizontally ();
			_controller.TransitionTo<GroundedState> ();
		}

	}
}