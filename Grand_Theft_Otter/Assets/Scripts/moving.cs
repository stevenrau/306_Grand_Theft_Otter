using UnityEngine;
using System.Collections;

public class moving : MonoBehaviour {

    Rigidbody2D rBody; //the body of the player
    Animator animator; //the animator for the beaver sprite

	float waterGravity = -0.1f; // gravity when in water
	float landGravity = 1f; //gravity when out of water and touching a platform
	float airGravity = 3f; // gravity when out of water, but not touching a platform

	float platformSurface = 4.0f; // the y coordinate when landGravity should be on, also walking animation
	//float waterSurface = 3.3f; // the y coordinate where heavy gravity applies
	//float breathingSurface = 3.1f; 

    public float moveForce = 100f; //determines the speed of the player when moving with the analog stick

    public float maxSpeed;
    public bool facingRight; //is the player facing right
    public float facingAngle; //the angle the beaver is looking( what way its head is pointing)

    GameObject beaverSprite; //the child object of player that displays the beaver and animates it
	GameObject beaverMouth;

    float leftAnalogThresh; //the analog stick must be moved more than this to trigger moving animation

	// other scripts
	get_input moveInputScript;
	player_state playerStateScript;
	

    // Use this for initialization
    void Start () {

        rBody = GetComponent<Rigidbody2D>();

        //get reference to the animator located on the beaver_sprite child object
		beaverSprite = transform.GetChild(1).gameObject;
        animator = beaverSprite.GetComponent<Animator>();

		//reference to the beaver mouth (to determine if can breath)
		beaverMouth = beaverSprite.transform.GetChild (1).gameObject;
	
        moveInputScript = gameObject.GetComponent<get_input>();
		playerStateScript = gameObject.GetComponent<player_state>();
        
        facingAngle = 0.0f;

        facingRight = true; //facing right initially

        leftAnalogThresh = 0.001f;

		animator.SetBool ("on_land", true);
		animator.SetBool ("is_moving", false);

		//team 2 faces left to start
		if (playerStateScript.GetTeamNumber () == "2") {
			Flip ();
		}
    }
	
	// Update is called once per frame
	void Update () {
        //move the player position 

        //for xbox controller
		float h = moveInputScript.GetMoveHorizontalAxis(); //mov_horiz
		float v = moveInputScript.GetMoveVerticalAxis();  //mov_vert

		//check if can breathe and set their color
		if (beaverMouth.transform.position.y >= constants.waterSurface) 
		{
			playerStateScript.SetCanBreathe (true);
			if("1" == playerStateScript.GetTeamNumber()){
				beaverSprite.GetComponent<SpriteRenderer>().color = constants.team1Color;
			}
			else{
				beaverSprite.GetComponent<SpriteRenderer>().color = constants.team2Color;
			}

		} 
		else 
		{
			playerStateScript.SetCanBreathe (false);

			if("1" == playerStateScript.GetTeamNumber()){
				beaverSprite.GetComponent<SpriteRenderer>().color = constants.team1ColorWater;
			}
			else{
				beaverSprite.GetComponent<SpriteRenderer>().color = constants.team2ColorWater;
			}


		}

		//check if in water or at surface
		if (transform.position.y >= constants.waterSurface) 
		{
			animator.SetBool ("on_land", false);

			//check if walking on platform
			//if( transform.position.y >= platformSurface && playerStateScript.GetIsTouchingPlatform()) {
			if(playerStateScript.GetIsTouchingPlatform()) 
			{
				rBody.gravityScale = landGravity;
				if(transform.position.y >= platformSurface)
				{
					animator.SetBool ("on_land", true);
					v = 0; //do not allow vertical movement when on the platform
				}

			}
			else{
				rBody.gravityScale = airGravity;
			}
		} 
		else 
		{
			rBody.gravityScale = waterGravity;
			animator.SetBool ("on_land", false);
		}


        //set animation if stick is moved enough
		if ((Mathf.Abs(h) > leftAnalogThresh) || (Mathf.Abs(v) > leftAnalogThresh))
        {
            //show the player moving animation
            animator.SetBool("is_moving", true);

            //move the player in the direction of the input
            rBody.AddForce(new Vector2(moveForce * h, 0));
            rBody.AddForce(new Vector2(0, moveForce * v));

        }
        else
        {
            animator.SetBool("is_moving", false);
        }

        // ******** 
        // * Had to take out the following to let the dash do it's thing
        // ***********
        ////limit to max speed in x
        //if (rBody.velocity.x > maxSpeed)
        //{
        //    rBody.velocity = new Vector2(maxSpeed, rBody.velocity.y);
        //}
        //else if (rBody.velocity.x < maxSpeed * -1)
        //{
        //    rBody.velocity = new Vector2(maxSpeed * -1, rBody.velocity.y);
        //}

        ////limit to max speed in y
        //if (rBody.velocity.y > maxSpeed)
        //{
        //    rBody.velocity = new Vector2(rBody.velocity.x, maxSpeed);
        //}
        //else if (rBody.velocity.y < maxSpeed * -1)
        //{
        //    rBody.velocity = new Vector2(rBody.velocity.x, maxSpeed * -1);
        //}

        //point the beaver_sprite in the same direction as it is moving
        RotateBeaver(h, v);

        //set the offset pearl object to point in the direction it will be thrown
        //also sets throw angle

        
        //flip the orientation of the sprite if direction was changed
        if ((facingRight && h < 0) || (!facingRight && h > 0))
        {
            Flip();
        }
        
      
    }

    //point the beaver in the same direction as it is moving
    void RotateBeaver(float x, float y)
    {
        // cancel all input below the threshold
        if (Mathf.Abs(x) < leftAnalogThresh) { x = 0.0f; }
        if (Mathf.Abs(y) < leftAnalogThresh) { y = 0.0f; }


        // CALCULATE ANGLE AND ROTATE
        if (x != 0.0f || y != 0.0f)
        {
            if (facingRight)
            {
                facingAngle = (Mathf.Atan2(y, x) * Mathf.Rad2Deg);

            }
            else
            {
                facingAngle = (Mathf.Atan2(y, x) * Mathf.Rad2Deg) + 180;

            }

			playerStateScript.SetFacingAngle(facingAngle);

            //beaver_sprite.transform.rotation = Quaternion.AngleAxis(facing_angle, Vector3.forward);
            transform.rotation = Quaternion.AngleAxis(facingAngle, Vector3.forward);
        }
    }

    

    //face the player right or left based on its horizontal speed
    void Flip()
    {
        animator.SetTrigger("turn");

        facingRight = !facingRight;

        Vector3 tmp = beaverSprite.transform.localScale;
        tmp.x = tmp.x * -1;
        beaverSprite.transform.localScale = tmp;
    }

    public bool GetFacingRight()
    {
        return facingRight;
    }

	//public float GetFacingAngle() {
	//	return facingAngle;
	//}
}
 