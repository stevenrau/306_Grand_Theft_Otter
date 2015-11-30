/*
* dash script for when the user presses the 'A' button to get a burst of speed for a short time.
* While a player is dashing, they may hit an enemy player to immobolize them for a short while
* and knock the pearl out of their hands. A player may not dash while they currently posses the pearl.
*
* Current Status : 
*
* TODO: knockback, damage flashing, throw pearl
*
* part of script made using the following help forum: 
* http://answers.unity3d.com/questions/892955/dashing-mechanic-using-rigidbodyaddforce.html
* 
*
*/



using UnityEngine;
using System.Collections;

public class dash : MonoBehaviour {

    //whether player is 'Ready', 'Dashing', or on 'Cooldown'
    public DashState dashState;
    //counts down from max dash time to 0 to reset to 'ready' state
    public float dashTimer;

    //max time between dashes
    private float maxDash = 2f;
    private float dashVelocity = 5000f;
    //public Vector2 savedVelocity;

	float dropPearlForce= 400;
    Rigidbody2D r_body;

    //getting scripts
    get_input dashInputScript;
    player_state playerStateScript;
    moving movingScript;
    throwing throwingScript;



	sound_player soundPlayer;
	public AudioClip dashSound;
	public AudioClip hitSound;
	//AudioSource splashSound;

    GameObject beaverSprite; //the child object of player that displays the beaver and animates it
    Animator animator; //the animator for the beaver sprite

    GameObject enemyPlayer;
	get_input enemyInputScript;
	player_state enemyStateScript;

    //dealing with velocity with player not moving
    private float maxSpeed = 2;
    //klooksgood value for getting beaver to dash with no y velocity and also make it onto platform from water
    private float yBoost = 1.2f;

    private float directionToDash;

    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }

    void Start()
    {
        r_body = GetComponent<Rigidbody2D>();

        dashInputScript = GetComponent<get_input>();
        playerStateScript = GetComponent<player_state>();
        movingScript = GetComponent<moving>();
        throwingScript = GetComponent<throwing>();
		
		soundPlayer = GameObject.FindGameObjectWithTag ("Sound_Player").GetComponent<sound_player>();

        //get reference to the animator located on the beaver_sprite child object
        beaverSprite = transform.GetChild(1).gameObject;
        animator = beaverSprite.GetComponent<Animator>();
    }

    void Update()
    {
        switch (dashState)
        {
            case DashState.Dashing:
                //show the player dashing animation
                animator.SetBool("is_dashing", true);
                

                dashTimer += Time.deltaTime * 5;
                if (dashTimer >= maxDash)
                {
                    //stop the player dashing animation
                    animator.SetBool("is_dashing", false);
                    //changing player state
                    playerStateScript.SetIsDashing(false);

                    dashTimer = maxDash;
                    //r_body.velocity = savedVelocity;
                    dashState = DashState.Cooldown;
                }
                break;

            case DashState.Cooldown:
                
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;

            case DashState.Ready:
                //if dash button is pressed, set velocity... 
                if (dashInputScript.GetDashButton())
                {
                    if (!playerStateScript.GetHasPearl() && !playerStateScript.GetIsHit())
                    {
                       // savedVelocity = r_body.velocity;


                        //find the angle to dash based on the angle player is facing
                        Vector3 dir;
                        if (movingScript.GetFacingRight())
                        {
                            dir = Quaternion.AngleAxis(playerStateScript.GetFacingAngle() - 90f, Vector3.forward) * Vector3.up;
                        }
                        else
                        {
                            dir = Quaternion.AngleAxis(playerStateScript.GetFacingAngle() - 270f, Vector3.forward) * Vector3.up;
                        }


                        //apply force to the player in that direction
                        GetComponent<Rigidbody2D>().AddForce(dir * dashVelocity);

                        //adding the dash burst to the player velocity
                        //r_body.AddForce(new Vector2(dashVelocity * 1f, dashVelocity * 1f));
                        //changing player state
                        playerStateScript.SetIsDashing(true);

                        dashState = DashState.Dashing;

                        //splashSound.Play();
						soundPlayer.PlayClip(dashSound, 1.0f);
                    }
                    
                }
                break;
            
        }
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //figure out what player/team this instance is, and check if it's colliding with the enemy team players
        if (col.gameObject.tag == "Player")
        {
            enemyPlayer = col.gameObject; //current player is beaverSprite

           // enemyInputScript = enemyPlayer.GetComponent<get_input>();
			enemyStateScript = enemyPlayer.GetComponent<player_state>();

            //only want players of opposite teams
            if (playerStateScript.GetTeamNumber()!= enemyStateScript.GetTeamNumber())
            {
                //TODO: case for both have dash on
                if (enemyStateScript.GetIsDashing() == true) //enemy has dash on
                {
                    //player becomes immobile and drops the pearl in the direction of impact
                    Damage();
					soundPlayer.PlayClip(hitSound, 1.0f);

                    //knockback the other player (This player)
                    //StartCoroutine(Knockback(beaverSprite, 1f, 350, beaverSprite.transform.position));
                }
                
            }
            
        }

    }

    void Damage()
    {
        playerStateScript.SetIsHit(true);

        movingScript.enabled = false; // disables movement, dashing, throwing
        throwingScript.enabled = false;
        animator.SetTrigger ("breathing_in"); // sets animator trigger so that suffocation animation is played

		//throw the pearl in the direction of enemy dash
        throwingScript.ThrowPearl(enemyStateScript.GetFacingAngle(), dropPearlForce);

		//become able to move again
        Invoke("RestartState", 1.5f);

        //player flashes red
        //playerHit.GetComponent<Animation>().Play("Player_RedFlash");
    }

    void RestartState()
    {
        playerStateScript.SetIsHit(false);

        movingScript.enabled = true; // disables movement, dashing, throwing
        throwingScript.enabled = true;

        animator.SetTrigger("revived");
    } 

    //knockback the player that was hit - either current player or enemy 
    IEnumerator Knockback(GameObject playerHit, float knockDur, float knockbackPwr, Vector3 knockbackDir)
    {
        //if playerHit has the pearl, throw the force away a bit

        //code for knocking playerHit backward
        float timer = 0;

        while (knockDur > timer)
        {
            timer += Time.deltaTime;

            playerHit.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockbackDir.x * -100, knockbackDir.y * knockbackPwr));

        }

        yield return 0;

    }

    
}


