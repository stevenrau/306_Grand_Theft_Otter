﻿using UnityEngine;
using System.Collections;

public class collision_detection : MonoBehaviour {

	GameObject pearlOffset; //the child object of the player that will indicate the direction the pearl will be thrown
	SpriteRenderer pearlRenderer; //the component that will either show or hide the pearl on the beaver.

	player_state playerStateScript;

	// when this number is 0, then the beaver is not touching a platform
	// works similar to a semaphore like a semaphore
	int numberOfPlatformsTouching;

	// Use this for initialization
	void Start() {
		//get references to the child objects
		pearlOffset = transform.GetChild (0).gameObject;

		//get reference to the sprite renderer located on the pearlOffset child object
		pearlRenderer = pearlOffset.GetComponent<SpriteRenderer>();

		playerStateScript = gameObject.GetComponent<player_state>();

		numberOfPlatformsTouching = 0;

	}

	void Update(){

		if (numberOfPlatformsTouching == 0) {
			playerStateScript.SetIsTouchingPlatform(false);
		} else {
			playerStateScript.SetIsTouchingPlatform(true);
		}
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Pearl") {
			
			//  GameObject pearlOffset = player.transform.GetChild(0).gameObject;
			//pearlOffset.GetComponent<SpriteRenderer>().enabled = true;
			pearlRenderer.enabled = true;
			pearlOffset.GetComponent<SpriteRenderer>().enabled = true;
			
			Destroy(other.gameObject);

            playerStateScript.SetHasPearl(true);
		}
   
		if (other.tag == "Platform") {
			numberOfPlatformsTouching++;
			//playerStateScript.SetIsTouchingPlatform(true);
			
		}

	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Platform") {
				//playerStateScript.SetIsTouchingPlatform(true);
		
		}
		
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Platform") {
			numberOfPlatformsTouching--;
			//playerStateScript.SetIsTouchingPlatform(false);

		}
		
	}



	void OnCollisionEnter2D(Collision2D other)
	{
		//if (other.tag == "Platform") {
			//print("platform collider enter");
			//playerStateScript.SetIsTouchingPlatform(true);
		//}
		
	}

	void OnCollisionExit2D(Collision2D other)
	{
		//if (other.tag == "Platform") {
			//print("platform collider exit");
			//playerStateScript.SetIsTouchingPlatform(false);
		//}

	}

	
	public void HidePearl()
	{
		//transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		pearlRenderer.enabled = false;
	}

}
