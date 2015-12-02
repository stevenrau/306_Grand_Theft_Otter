using UnityEngine;
using System.Collections;

public class beaver_breathing : MonoBehaviour {

	private float suffocate_time = 2f; // Time spent in the suffocation animation
	private float floating_speed = -1f; // the value the Gravity is set to when floating up 
	//private bool isSuffocating;

	// the scripts that will be disabled when suffocated
	moving movingScript;
	dash dashScript;
	throwing throwingScript;
	collision_detection colDetectScript;

	Animator animator;
	private ParticleSystem Bubbles; // The particle System that emits bubbles
	Rigidbody2D rBody; // the ridgit body of the palyer
	private string Player_name; // player name (eg. "Beaver1)
	private int breath_count = 0; //counting subsequent breathing_in frames
	private int breath_threshhold = 4; // How many subsequent breathing_in frames until suffocating. 
	player_state playerStateScript;

	float boostVelocity = 1000f;
	float dropPearlForce = 100f;

	GameObject beaverMouth; // the gameobject that flips left and right. Its children include, the beaver mouth

	GameObject beaverPearlCollider; //will be disabled when floating

	sound_player soundPlayer;
	public AudioClip Bubble_sound;


	void Start () {

		//get the state script
		playerStateScript = GetComponent<player_state> ();

		//get the
		GameObject beaverSprite = transform.GetChild (1).gameObject;
		beaverMouth = transform.GetChild (1).gameObject;

		beaverPearlCollider = beaverSprite.transform.GetChild (0).gameObject;

		// get Player name form patent object 
		Player_name = gameObject.transform.name;


		//gettign the scripts that will be disabled
		movingScript = GetComponent<moving> ();
		dashScript = GetComponent<dash> ();
		throwingScript = GetComponent<throwing> ();
		colDetectScript = GetComponent<collision_detection> ();

		//getting the particle system
		Bubbles = beaverMouth.GetComponentInChildren<ParticleSystem> ();

		//setting the emmision to 0
		Bubbles.emissionRate = 0;

		//initially not suffocating
		//isSuffocating = false;
		playerStateScript.SetIsSuffocating (false);

		// gettign the ridgit body of the Player
		rBody = GetComponent<Rigidbody2D> ();


		// getting the animator.
		animator = transform.GetChild (1).gameObject.GetComponent<Animator> ();

		soundPlayer = GameObject.FindGameObjectWithTag ("Sound_Player").GetComponent<sound_player>();

	}


	void Update () {
		//Check if the Bsave is at surface. Thsi is a temporary solution the OnTriggerEnter method is a little bit more elegant 
		// but will only work right when the mouth peace flips with the beaver sprite whoch it hopefully will. 
		if (playerStateScript.GetIsSuffocating() && transform.position.y > 2.4f) {
			rBody.gravityScale = constants.waterGravity;
		}
		//if (transform.position.y >= constants.breathingSurface) {
		if (playerStateScript.GetCanBreathe () && playerStateScript.GetIsSuffocating ()) {

			//set gravity back
			rBody.gravityScale = constants.airGravity;

			//isSuffocating = false;
			playerStateScript.SetIsSuffocating (false);
			//print("isSuffocating set false");

			// reset the beaver to be in the normal layer where they can interact with obstacles
			transform.FindChild ("Beaver").gameObject.layer = LayerMask.NameToLayer ("Non_Interactable");
			beaverMouth.transform.FindChild ("beaver_mouth").gameObject.layer = LayerMask.NameToLayer ("Non_Interactable");

			animator.SetBool ("at_surface", true); // sets animator so that it transitions form foating to idle
			//movingScript.enabled = true; // enable movement again (same for dashing and throwing) 
			movingScript.SetMoveForce (constants.moveForceNormal);
			dashScript.enabled = true;
			throwingScript.enabled = true;	
			colDetectScript.enabled = true;
			beaverPearlCollider.GetComponent<Collider2D> ().enabled = true;
		}
		else if (!playerStateScript.GetIsSuffocating() && !playerStateScript.GetCanBreathe()){
			animator.SetBool ("at_surface", false);
			check_breathing ();
		}
	}



	void check_breathing()
	{
	//checking the player_name to only react to the right button presses
		// Beaver 1
		if (Player_name == "Beaver1"){
			if (Input.GetKey ("i")){
				//print ("1 breathing in");
				breath_count += 1;
			}
		
			if (Input.GetKey ("o")) {
				breath_count = 0;
				//print ("1 breathing out");
				Bubbles.Emit (5);
				soundPlayer.PlayClip(Bubble_sound, 1.0f);

				if(!playerStateScript.GetIsSuffocating()){
					ApplySpeedBoost(); //boost of speed if breahting out
				}
			}
		}

		// Beaver 2
		if (Player_name == "Beaver2"){
			if (Input.GetKey ("k")){
				breath_count += 1;
			}
			if (Input.GetKey ("l")) {
				breath_count = 0;
				Bubbles.Emit (5);
				soundPlayer.PlayClip(Bubble_sound, 1.0f);

				if(!playerStateScript.GetIsSuffocating()){
					ApplySpeedBoost(); //boost of speed if breahting out
				}
			}
		}

	// If breath count is greater than the Threshold beaver suffocates
		if (breath_count >= breath_threshhold)
		suffocate ();
	}

	void ApplySpeedBoost(){

		//increase player speed if they are breathing out
		//find the angle to apply force based on the angle the beaver is facing
		Vector3 dir;
		if (movingScript.GetFacingRight ()) {
			dir = Quaternion.AngleAxis ((playerStateScript.GetFacingAngle () - 90f), Vector3.forward) * Vector3.up;
		} else {
			dir = Quaternion.AngleAxis ((playerStateScript.GetFacingAngle () - 270f), Vector3.forward) * Vector3.up;
		}
		
		//apply force to the pearl in that direction
		GetComponent<Rigidbody2D> ().AddForce (dir * boostVelocity);
	}


	void suffocate(){

		//isSuffocating = true;
		playerStateScript.SetIsSuffocating (true);
		//print("isSuffocating set true");
		breath_count = 0;

		// the beaver should pass through obstacles when they are suffocating
		transform.FindChild("Beaver").gameObject.layer = LayerMask.NameToLayer("Suffocating"); //The beaver collider
		beaverMouth.transform.FindChild("beaver_mouth").gameObject.layer = LayerMask.NameToLayer("Suffocating"); // The mouth collider

		//drop the pearl, 180 = downwards, 
		throwingScript.ThrowPearl (180.0f, dropPearlForce);
		playerStateScript.SetHasPearl (false);

		//movingScript.enabled = false; // disables movement, dashing, throwing
		movingScript.SetMoveForce (constants.moveForceSlow);
		dashScript.enabled = false;
		throwingScript.enabled = false;
		colDetectScript.enabled = false;
		beaverPearlCollider.GetComponent<Collider2D>().enabled = false;
		animator.SetTrigger ("breathing_in"); // sets animator trigger so that suffocation animation is played
		Invoke ("floating", suffocate_time); // switch to floating after "suffocate_time" amount of seconds		
		}


	void floating() {
		animator.SetTrigger ("floating"); // set anomator trigger so that beaver transitions form sufoocation to floating animation
		Bubbles.Emit (1); // emmit 2 bursts of Bubblesl
		rBody.gravityScale = floating_speed; // sets the gravity scale fo the beaver to the value of floating speed so that the beaver floats up.
	}


}