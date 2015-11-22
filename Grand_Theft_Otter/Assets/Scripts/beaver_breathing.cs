using UnityEngine;
using System.Collections;

public class beaver_breathing : MonoBehaviour {

	private float suffocate_time = 2f; // Time spent in the suffocation animation
	private float floating_speed = -1f; // the value the Gravity is set to when floating up 

	// the scripts that will be disabled when suffocated
	moving movement;
	dash dashing;
	throwing Throwing;
	Animator animator;
	private ParticleSystem Bubbles; // The particle System that emits bubbles
	Rigidbody2D rBody; // the ridgit body of the palyer
	private string Player_name; // player name (eg. "Beaver1)
	private int breath_count = 0; //counting subsequent breathing_in frames
	private int breath_threshhold = 4; // How many subsequent breathing_in frames until suffocating. 
	player_state playerStateScript;

	float boostVelocity = 1000f;

	GameObject beaverMouth; // the gameobject that flips left and right. Its children include, the beaver mouth


	void Start () {

		//get the state script
		playerStateScript = GetComponent<player_state> ();

		//get the
		GameObject beaverSprite = transform.GetChild (1).gameObject;
		beaverMouth = transform.GetChild (1).gameObject;

		// get Player name form patent object 
		Player_name = gameObject.transform.name;

		//gettign the scripts that will be disabled
		movement = GetComponent<moving> ();
		dashing = GetComponent<dash> ();
		Throwing = GetComponent<throwing> ();

		//getting the particle system
		Bubbles = beaverMouth.GetComponentInChildren<ParticleSystem> ();

		//setting the emmision to 0
		Bubbles.emissionRate = 0;


		// gettign the ridgit body of the Player
		rBody = GetComponent<Rigidbody2D> ();


		// getting the animator.
		animator = transform.GetChild (1).gameObject.GetComponent<Animator> ();
	}


	void Update () {
		//Check if the Bsave is at surface. Thsi is a temporary solution the OnTriggerEnter method is a little bit more elegant 
		// but will only work right when the mouth peace flips with the beaver sprite whoch it hopefully will. 
		
		if (playerStateScript.GetCanBreathe()) {
			animator.SetBool ("at_surface", true); // sets animator so that it transitions form foating to idle
			movement.enabled = true; // enable movement again (same for dashing and throwing) 
			dashing.enabled = true;
			Throwing.enabled = true;		
			} 

		else {
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
				print ("1 breathing in");
				breath_count += 1;
			}
			else {

			}
			if (Input.GetKey ("o")) {
				breath_count = 0;
				print ("1 breathing out");
				Bubbles.Emit (5);

				//increase player speed if they are breathing out
				//find the angle to throw based on the angle found for the pearl_offest
				Vector3 dir;
				if (movement.GetFacingRight())
				{
					dir = Quaternion.AngleAxis( (movement.GetFacingAngle() - 90f), Vector3.forward) * Vector3.up;
				}
				else
				{
					dir = Quaternion.AngleAxis((movement.GetFacingAngle() - 270f), Vector3.forward) * Vector3.up;
				}

				//apply force to the pearl in that direction
				GetComponent<Rigidbody2D>().AddForce(dir * boostVelocity);


			}
		}

		// Beaver 2
		if (Player_name == "Beaver2"){
			if (Input.GetKey ("k")){
				breath_count += 1;
			}
			else{

			}
			if (Input.GetKey ("l")) {
				breath_count = 0;
				Bubbles.Emit (3);
			}
		}

	// If breath count is greater than the Threshold beaver suffocates
		if (breath_count >= breath_threshhold)
		suffocate ();
	}


	void suffocate(){
		movement.enabled = false; // disables movement, dashing, throwing
		dashing.enabled = false;
		Throwing.enabled = false;
		animator.SetTrigger ("breathing_in"); // sets animator trigger so that suffocation animation is played
		Invoke ("floating", suffocate_time); // switch to floating after "suffocate_time" amount of seconds		
		}


	void floating() {
		animator.SetTrigger ("floating"); // set anomator trigger so that beaver transitions form sufoocation to floating animation
		Bubbles.Emit (1); // emmit 2 bursts of Bubblesl
		rBody.gravityScale = floating_speed; // sets the gravity scale fo the beaver to the value of floating speed so that the beaver floats up.
	}


}