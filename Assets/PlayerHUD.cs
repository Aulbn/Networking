using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHUD : MonoBehaviour {
	public Transform player;
	public float followSpeed = 2f;
	public Image punchChargeFill;

	private void Start(){
		ResetPunchCharge ();
	}

	private void Update(){
		transform.position = Vector3.Lerp (transform.position, player.position, Time.deltaTime * followSpeed);
	}

	public void SetPunchCharge(float charge){
		punchChargeFill.fillAmount = Mathf.Clamp01 (charge);
	}

	public void ResetPunchCharge(){
		punchChargeFill.fillAmount = 0;
	}



}
