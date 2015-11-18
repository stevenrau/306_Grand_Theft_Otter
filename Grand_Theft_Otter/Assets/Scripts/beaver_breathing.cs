using UnityEngine;
using System.Collections;

public class beaver_breathing : MonoBehaviour {

	private bool is_under_water = true;
	private float T;
	private float suffocate_time = 2f; // Time spent in the suffocation animation
	private float floating_speed = -1f; // the value the Gravity is set to when floating up 
	private float normal_gravity = 0.2f; // normal gravity scale of beaver

	// the scripts that will be disabled when suffocated
	moving movement;
	dash dashing;
	throwing Throwing;
	Animator animator;
	private ParticleSystem Bubbles; // The particle System that emits bubbles
	Rigidbody2D rBody; // the ridgit body of the palyer
	private string Player_name; // player name (eg. "Beaver1)
	private int breath_count = 0; //counting subsequent breathing_in frames
	private int breath_threshhold = 10; // How many subsequent breathing_in frames until suffocating. 
	static bool at_surface; //bool that indicates if beaver is at surface




	void Start () {
		// get Player name form patent object 
		Player_name = gameObject.transform.parent.name;

		//gettign the scripts that will be disabled
		movement = GetComponentInParent<moving> ();
		dashing = GetComponentInParent<dash> ();
		Throwing = GetComponentInParent<throwing> ();

		//getting the particle system
		Bubbles = GetComponentInChildren<ParticleSystem>();
		//setting the emmision to 0
		Bubbles.emissionRate = 0;


		// gettign the ridgit body of the Player
		rBody = GetComponentInParent<Rigidbody2D>();

		// getting the animator. The "foreach" is nessesary because the animator is not in the partent but in the sibling object "beaver"
		// and I didnt find an easier way to get an component form a sibling. 
		foreach (Transform child in transform.parent) {
			if (child.name == "Beaver")
				animator = child.GetComponent<Animator> ();
		}
	}
/*   
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "sky") {
			at_surface = true;
			animator.SetBool ("at_surface", true); // sets animator so that it transitions form foating to idle
			movement.enabled = true;
			dashing.enabled = true;
			Throwing.enabled = true;		
			rBody.gravityScale = normal_gravity; // Changes gravity back to noraml (from floating)
		} 
	}


	void OnTriggerExit2D (Collider2D another)
	{
		if (another.gameObject.tag == "water") {
			at_surface = false;
			animator.SetBool ("at_surface", false);
		} 
	}
*/

	void Update () {
		//Check if the Bsave is at surface. Thsi is a temporary solution the OnTriggerEnter method is a little bit more elegant 
		// but will only work right when the mouth peace flips with the beaver sprite whoch it hopefully will. 
		if (rBody.position.y > 2.8) {
			at_surface = true;
			animator.SetBool ("at_surface", true); // sets animator so that it transitions form foating to idle
			movement.enabled = true; // enable movement again (same for dashing and throwing) 
			dashing.enabled = true;
			Throwing.enabled = true;		
			rBody.gravityScale = normal_gravity; // Changes gravity back to noraml (from floating)
			} 

		else {
			at_surface = false;
			animator.SetBool ("at_surface", false); 
			}


		// If the beave is under water: chekc for breathing input
		if (at_surface == false) {
			check_breathing ();
			}
			
	}



	void check_breathing()
	{
	//checking the player_name to only react to the right button presses
		// Beaver 1
		if (Player_name == "Beaver1"){
			if (Input.GetKey ("i"))
				breath_count += 1;
			else 
				breath_count = 0;
			if (Input.GetKey ("o")) 
				Bubbles.Emit (2);
		}

		// Beaver 2
		if (Player_name == "Beaver2"){
			if (Input.GetKey ("k"))
				breath_count += 1;
			else
				breath_count = 0;
			if (Input.GetKey ("l")) 
				Bubbles.Emit (2);
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
		Bubbles.Emit (2); // emmit 2 bursts of Bubbles
		rBody.gravityScale = floating_speed; // sets the gravity scale fo the beaver to the value of floating speed so that the beaver floats up.
	}


}