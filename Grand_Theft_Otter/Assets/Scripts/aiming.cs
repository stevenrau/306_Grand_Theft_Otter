﻿using UnityEngine;
using System.Collections;

public class aiming : MonoBehaviour {

	//the child object of the player that will indicate the direction the pearl will be thrown
	GameObject pearlOffset;

	//the component that will either show or hide the pearl on the beaver.
	SpriteRenderer pearlRenderer;

	//the animator that shows the aiming guide or not
	Animator anim;

	float throwAngle; // the angle the pearl will be thrown

	//do not detect input smaller than this to aviod unwanted changes when not touching the stick
	float aimingThreshold;

	//the script that listens for input from the right analog stick
	get_input inputScript;
	player_state playerStateScript;
	moving movingScript;


	// Use this for initialization
	void Start () {
		inputScript = gameObject.GetComponent<get_input>();
		playerStateScript = gameObject.GetComponent<player_state>();
		movingScript = gameObject.GetComponent<moving>();
		pearlOffset = transform.GetChild(0).gameObject;

		pearlRenderer = pearlOffset.GetComponent<SpriteRenderer>();
		anim = pearlOffset.GetComponent<Animator> ();
		
		throwAngle = 0.0f;
		aimingThreshold = 0.001f; // if this value is too high the aiming tends to snap to the NSEW directions (0.2 was too high)

	}

	// Update is called once per frame
	void Update() {
		RotatePearlOffset (inputScript.GetAimHorizontalAxis(), inputScript.GetAimVerticalAxis());
	}

	//point the pearl based on right analog stick
	void RotatePearlOffset(float x, float y)
	{


		// cancel all input below this 
		if (Mathf.Abs(x) < aimingThreshold) { x = 0.0f; }
		if (Mathf.Abs(y) < aimingThreshold) { y = 0.0f; }
		
		// the player is aiming the pearl
		if (x != 0.0f || y != 0.0f) {

			//show the aiming guide
			anim.SetBool ("Aiming", true);

			if (inputScript.PlatformIsPC ()) {
				throwAngle = ((Mathf.Atan2 (y, x) * Mathf.Rad2Deg) * -1) - 180;	// for PC
			}
			else {
				throwAngle = ((Mathf.Atan2 (y, x) * Mathf.Rad2Deg) - 90);			// for MAC
			}

			//point the aiming guide in the direction 
			pearlOffset.transform.rotation = Quaternion.AngleAxis (throwAngle, Vector3.forward);
		} 

		else {
			//not aiming, do not show aiming guide on pearl offset, next trow will be in facing dir
			anim.SetBool ("Aiming", false);

			if(movingScript.GetFacingRight()){
			throwAngle = playerStateScript.GetFacingAngle() - 90;
			}
			else{
				throwAngle = playerStateScript.GetFacingAngle() + 90;
			}
		}

		playerStateScript.SetAimingAngle (throwAngle);


	
	}

	public float GetThrowAngle() {
		return throwAngle;
	}
}
