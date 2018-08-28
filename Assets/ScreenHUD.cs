using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScreenHUD : MonoBehaviour {
	public Transform player;
	public RectTransform sight;
	private Vector2 aimDirection = Vector2.zero;
	public Image punchChargeFill;

	private void Start(){
		Cursor.visible = false;
	}

	private void Update(){
		AimAssistant ();
		Debug.DrawRay (player.position, new Vector3 (aimDirection.x, aimDirection.y) * 10);
	}

	public void AimAssistant(){
		aimDirection = Camera.main.ScreenToWorldPoint (Input.mousePosition) - sight.transform.position;
		aimDirection = aimDirection.normalized;
		float angle = Mathf.Atan2 (aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		sight.localRotation = rotation;
		sight.position = player.position;
	}

	public Vector2 AimDireciton(){
		return aimDirection;
	}

	public void SetPunchCharge(float charge){
		punchChargeFill.fillAmount = Mathf.Clamp01 (charge);
	}

	public void ResetPunchCharge(){
		punchChargeFill.fillAmount = 0;
	}

}
