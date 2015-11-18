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
    public float maxDash = 2f;
    private float dashVelocity = 5000f;
    public Vector2 savedVelocity;

    Rigidbody2D r_body;

    //getting scripts
    get_input dashInputScript;
    player_state checkPlayerStateScript;
    moving movingScript;

    AudioSource splashSound;

    GameObject beaverSprite; //the child object of player that displays the beaver and animates it
    Animator animator; //the animator for the beaver sprite

    GameObject enemyPlayer;

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
        checkPlayerStateScript = GetComponent<player_state>();
        movingScript = GetComponent<moving>();

        splashSound = GetComponent<AudioSource>();

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

                    dashTimer = maxDash;
                    r_body.velocity = savedVelocity;
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

                    savedVelocity = r_body.velocity;

                    
                    //for xbox controller
                    float h = dashInputScript.GetMoveHorizontalAxis(); //mov_horiz
                    float v = dashInputScript.GetMoveVerticalAxis();  //mov_vert

                    //dash the player in the direction of the input
                    
                    if (h < 0.02f && v < 0.02f)
                    {
                        //find the angle to throw based on the angle found for the pearl_offest
                        Vector3 dir = Quaternion.AngleAxis(movingScript.GetFacingAngle(), Vector3.forward) * Vector3.up;

                        //apply force to the pearl in that direction
                        r_body.AddForce(dir * dashVelocity);
                    }
                    else
                    {
                        r_body.AddForce(new Vector2(dashVelocity * h, dashVelocity * v));
                    }

                    //dashing in the case where velocity in one or more axis' velocity is small
                    //if (r_body.velocity.x < 0.3f && r_body.velocity.x > 0.3f * -1)
                    //{
                    //    //set it to an arbitrary speed
                    //    if (r_body.velocity.x < 0) //velocity in the negative
                    //    {
                    //        r_body.velocity = new Vector2(maxSpeed * -1, r_body.velocity.y);
                    //    }   
                    //    else
                    //    {
                    //        r_body.velocity = new Vector2(maxSpeed, r_body.velocity.y);
                    //    }
                        
                    //}

                    //if (r_body.velocity.y < 0.3f && r_body.velocity.y > 0.3f * -1)
                    //{
                    //    //set it to an arbitrary speed
                    //    if (r_body.velocity.y < 0) //velocity in the negative
                    //    {
                    //        r_body.velocity = new Vector2(r_body.velocity.y, maxSpeed * -1);
                    //    }
                    //    else
                    //    {
                    //        r_body.velocity = new Vector2(r_body.velocity.y, maxSpeed);
                    //    }

                    //}
                    // Took out the code for small Y value because gravity was affecting the scaling 
                    //if ((r_body.velocity.y < 0.01f && r_body.velocity.y > 0.01f * -1) && !(r_body.velocity.x > 0.3f && r_body.velocity.x < 0.3f * -1))
                    //{
                    //    //set it to an arbitrary speed ** 1.2 klooksgood in the water and gets beaver to jump onto platform
                    //    r_body.velocity = new Vector2(r_body.velocity.x, yBoost);

                    //}

                    //adding the dash burst to the player velocity 
                    //r_body.AddForce(savedVelocity * 10f);
                    //r_body.velocity = r_body.velocity * dashVelocity;
                    //r_body.velocity = new Vector2(r_body.velocity.x + 10f, r_body.velocity.y + 10f);
                    dashState = DashState.Dashing;

                    splashSound.Play();
                    
                }
                break;
            
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //figure out what player/team this instance is, and check if it's colliding with the enemy team players
        if (col.tag == "Player")
        {
            enemyPlayer = col.gameObject; //current player is beaverSprite

            //only want players of opposite teams
            if (dashInputScript.GetTeam(dashInputScript.GetPlayerID())
                != enemyPlayer.GetComponent<get_input>().GetTeam(dashInputScript.GetPlayerID()))
            {
                
                //TODO: case for both have dash on
                if(checkPlayerStateScript.GetIsDashing() == true) //this player has dash on
                {
                    //get enemy to blink red... could also pass int for taking breath away
                    //Damage(enemyPlayer); 

                    //knockback the other player (enemy)
                    StartCoroutine(Knockback(enemyPlayer, 1f, 350, enemyPlayer.transform.position));
                }
                else if (enemyPlayer.GetComponent<player_state>().GetIsDashing() == true) //enemy has dash on
                {
                    //get player to blink red... could also pass int for taking breath away
                    //Damage(beaverSprite);

                    //knockback the other player (This player)
                    StartCoroutine(Knockback(beaverSprite, 1f, 350, beaverSprite.transform.position));
                }
                
            }
            
        }

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

    //player flashes red when hit by opposing dashing player
    public void Damage(GameObject playerHit)
    {
        //player flashes red
        //playerHit.GetComponent<Animation>().Play("Player_RedFlash");

    }
}


