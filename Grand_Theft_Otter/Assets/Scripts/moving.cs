using UnityEngine;
using System.Collections;

public class moving : MonoBehaviour {

    Rigidbody2D rBody; //the body of the player
    Animator animator; //the animator for the beaver sprite

	float waterGravity = -0.1f;
	float landGravity = 1f;
	float airGravity = 3f;

	float platformSurface = 3.5f;

	float waterSurface = 3.3f;
	float breathingSurface = 3.1f;
	//float deckSurface = 4.1f;

    public float moveForce = 100f;
	//float waterMoveForce = 100f;
	//float landMoveForce = 100f;

    public float maxSpeed;
    public bool facingRight; //is the player facing right
    public float facingAngle; //the angle the beaver is looking( what way its head is pointing)

    GameObject beaverSprite; //the child object of player that displays the beaver and animates it

    float leftAnalogThresh; //the analog stick must be moved more than this to trigger moving animation

	// script
	get_input throwInputScript;
	player_state playerStateScript;
	

    // Use this for initialization
    void Start () {

        rBody = GetComponent<Rigidbody2D>();

        //get reference to the animator located on the beaver_sprite child object
		beaverSprite = transform.GetChild(1).gameObject;
        animator = beaverSprite.GetComponent<Animator>();

        throwInputScript = gameObject.GetComponent<get_input>();
		playerStateScript = gameObject.GetComponent<player_state>();
        
        facingAngle = 0.0f;

        facingRight = true; //facing right initially

        leftAnalogThresh = 0.001f;

		animator.SetBool ("on_land", true);
		animator.SetBool ("is_moving", false);
		playerStateScript.SetIsOnLand (false);
    }
	
	// Update is called once per frame
	void Update () {
        //move the player position 

        //for xbox controller
		float h = throwInputScript.GetMoveHorizontalAxis(); //mov_horiz
		float v = throwInputScript.GetMoveVerticalAxis();  //mov_vert

		//check if can breathe
		if (transform.position.y >= breathingSurface) 
		{
			playerStateScript.SetCanBreathe (true);
			beaverSprite.GetComponent<SpriteRenderer>().color = Color.green;
		} 
		else 
		{
			playerStateScript.SetCanBreathe (false);
			beaverSprite.GetComponent<SpriteRenderer>().color = Color.red;
		}

		//check if in water or at surface
		if (transform.position.y >= waterSurface) 
		{
			animator.SetBool ("on_land", true);
			if( transform.position.y >= platformSurface && playerStateScript.GetIsTouchingPlatform()) {

				rBody.gravityScale = landGravity;
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

		//check if walking on platform
		//if (transform.position.y >= platformSurface && playerStateScript.GetIsTouchingPlatform()) {
		//	moveForce = landMoveForce;
		//} 
		//else {
		//	moveForce = waterMoveForce;
		//}


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

	public float GetFacingAngle() {
		return facingAngle;
	}
}
 