using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScreenHUD : MonoBehaviour {
	public Transform player;
	public Image sight;
	public float sightRadius = 2f;
	private Vector3 lastMousePos;
	[Range(0,1)]
	public float mouseSensitivity = 1f;
	private Vector3 aimDirection = Vector3.zero;
	public Image punchChargeFill;

	private void Start(){
		Cursor.visible = false;
	}

	private void Update(){
		AimAssistant ();
	}

	public void AimAssistant(){
		Vector3 mousePosDiff = new Vector3 (Input.mousePosition.x - lastMousePos.x, Input.mousePosition.y - lastMousePos.y) * mouseSensitivity;

		aimDirection = sight.transform.position + mousePosDiff - Camera.main.WorldToScreenPoint (player.position);
		aimDirection = aimDirection.normalized;
		sight.transform.position = Camera.main.WorldToScreenPoint (player.position) + aimDirection * sightRadius;

//		sight.transform.rotation = Quaternion.LookRotation (aimDirection, Vector3.left);

		lastMousePos = Input.mousePosition;
	}

	public Vector3 AimDireciton(){
		return aimDirection;
	}

	public void SetPunchCharge(float charge){
		punchChargeFill.fillAmount = Mathf.Clamp01 (charge);
	}

	public void ResetPunchCharge(){
		punchChargeFill.fillAmount = 0;
	}

}
