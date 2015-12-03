using UnityEngine;
using System.Collections;

public class collision_detection : MonoBehaviour {

	GameObject pearlOffset; //the child object of the player that will indicate the direction the pearl will be thrown
	SpriteRenderer pearlRenderer; //the component that will either show or hide the pearl on the beaver.

	player_state playerStateScript;

	// when this number is 0, then the beaver is not touching a platform
	// works similar to a semaphore like a semaphore
	int numberOfPlatformsTouching;


	Animator statusAnim; // used to show effects on the beaver (exists on the parent object called beaver)

	//needed for collision with enemy / dashing beaver
	GameObject enemyPlayer;
	player_state enemyStateScript;
	damage damageScript;

	//to stop motion when colliding with jelly
	Rigidbody2D rBody;

    score_keeper scoreScript;


	// Use this for initialization
	void Start() {
		//get references to the child objects
		pearlOffset = transform.GetChild (0).gameObject;

		//get reference to the sprite renderer located on the pearlOffset child object
		pearlRenderer = pearlOffset.GetComponent<SpriteRenderer>();

		playerStateScript = gameObject.GetComponent<player_state>();

		numberOfPlatformsTouching = 0;

		damageScript = GetComponent<damage> ();

		rBody = GetComponent<Rigidbody2D> ();
		
        scoreScript = GameObject.FindGameObjectWithTag("Score_Keeper").GetComponent<score_keeper>();
        
		statusAnim = GetComponent<Animator> ();

    }

	void Update(){

		if (numberOfPlatformsTouching == 0) {
			playerStateScript.SetIsTouchingPlatform(false);
		} else {
			playerStateScript.SetIsTouchingPlatform(true);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (!playerStateScript.GetIsSuffocating ()) {
			//figure out what player/team this instance is, and check if it's colliding with the enemy team players
			if (col.gameObject.tag == "Player") {
				enemyPlayer = col.gameObject; //current player is beaverSprite
			
				// enemyInputScript = enemyPlayer.GetComponent<get_input>();
				enemyStateScript = enemyPlayer.GetComponent<player_state> ();
			
				//only want players of opposite teams
				if (playerStateScript.GetTeamNumber () != enemyStateScript.GetTeamNumber ()) {
					//TODO: case for both have dash on
					if (enemyStateScript.GetIsDashing () == true) { //enemy has dash on
						//player becomes immobile and drops the pearl in the direction of impact
						damageScript.Damage ();
					
						//knockback the other player (This player)
						//StartCoroutine(Knockback(beaverSprite, 1f, 350, beaverSprite.transform.position));
					}
				
				}
			
			}

		}

	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (!playerStateScript.GetIsSuffocating ()) {
			if (other.tag == "Jellyfish") {

				//stop moving 
				rBody.velocity = new Vector2 (0.0f, 0.0f);

				//play electric shock effects
				statusAnim.SetTrigger("shocked");

				//play electric shock sound
			
				damageScript.Damage ();
			}

			if (other.tag == "Pearl") {
			
				//  GameObject pearlOffset = player.transform.GetChild(0).gameObject;
				//pearlOffset.GetComponent<SpriteRenderer>().enabled = true;
				pearlRenderer.enabled = true;
				pearlOffset.GetComponent<SpriteRenderer> ().enabled = true;
			
				Destroy (other.gameObject);

				playerStateScript.SetHasPearl (true);
			}
		}
		
			if (other.tag == "Platform") {
				numberOfPlatformsTouching++;
				//playerStateScript.SetIsTouchingPlatform(true);
			
			}

        Debug.Log(scoreScript.getLeftScore());
        Debug.Log(scoreScript.getRightScore());
        Debug.Log(scoreScript.getMaxScore());

        if (other.tag == "Boat") // check if they have 5 platforms and if yes they win
        {
            if (other.isTrigger)
            {
                if (scoreScript.getLeftScore() == scoreScript.getMaxScore()) // team 1 set to win
                {
                    if (playerStateScript.GetTeamNumber() == "1")
                    {
                        Application.LoadLevel(3);
                    }
                    
                }
                if (scoreScript.getRightScore() == scoreScript.getMaxScore()) // team 2 set to win
                {
                    if (playerStateScript.GetTeamNumber() == "2")
                    {
                        Application.LoadLevel(4);
                    }
                    
                }
            }
            
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



	
	public void HidePearl()
	{
		//transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		pearlRenderer.enabled = false;
	}

}
