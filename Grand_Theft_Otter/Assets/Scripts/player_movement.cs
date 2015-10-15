﻿using UnityEngine;
using System.Collections;

public class player_movement : MonoBehaviour {

    Rigidbody2D r_body; //the body of the player

	GameObject pearl_offset; //the child object of the player that will indicate the direction the pearl will be thrown
	SpriteRenderer pearl_renderer; //the component that will either show or hide the pearl on the beaver.

	GameObject beaver_sprite; //the child object of player that displays the beaver and animates it
    Animator animator; //the animator for the beaver sprite


    bool facing_right; //is the player facing right
    public float move_force;
    public float max_speed;
    public float throw_force;

	private bool has_pearl = false;
	private bool isPC = false;

    
    float facing_angle; //the angle the beaver is looking( what way its head is pointing)
    float throw_angle; // the angle the pearl will be thrown

	float L_analog_threshold; //the analog stick must be moved more than this to trigger moving animation
	float R_analog_threshold;
	

	// Use this for initialization
	void Start () {

		//get references to the child objects
		pearl_offset = transform.GetChild(0).gameObject;
		beaver_sprite = transform.GetChild(1).gameObject;

        r_body = GetComponent<Rigidbody2D>();

		//get reference to the animator located on the beaver_sprite child object
        animator = beaver_sprite.GetComponent<Animator>();

		//get reference to the sprite renderer located on the pearl_offset child object
		pearl_renderer = pearl_offset.GetComponent<SpriteRenderer>();
		
        throw_angle = 0.0f;
        facing_angle = 0.0f;

        facing_right = true; //facing right initially

		L_analog_threshold = 0.001f;
		R_analog_threshold = 0.20f;
	}

    // Update is called once per frame
    void Update() {

        //move the player position 

        //for xbox controller
       // float h = Input.GetAxis("left_analog_horizontal");
        //float v = Input.GetAxis("left_analog_vertical");


        //for key board
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        //set animation if stick is moved enough
		if ((Mathf.Abs(h) > L_analog_threshold) || (Mathf.Abs(v) > L_analog_threshold)  ) {

			//show the player moving animation
			animator.SetBool ("is_moving", true);

			//move the player in the direction of the input
			r_body.AddForce(new Vector2(move_force * h, 0));
			r_body.AddForce(new Vector2(0, move_force * v));

		} else {
			animator.SetBool ("is_moving", false);
		}

        //limit to max speed in x
        if (r_body.velocity.x > max_speed)
        {
            r_body.velocity = new Vector2(max_speed, r_body.velocity.y);
        }
        else if (r_body.velocity.x < max_speed * -1)
        {
            r_body.velocity = new Vector2(max_speed * -1, r_body.velocity.y);
        }

        //limit to max speed in y
        if (r_body.velocity.y > max_speed)
        {
            r_body.velocity = new Vector2(r_body.velocity.x, max_speed);
        }
        else if (r_body.velocity.y < max_speed * -1)
        {
            r_body.velocity = new Vector2(r_body.velocity.x, max_speed * -1);
        }

        //point the beaver_sprite in the same direction as it is moving
        rotateBeaver(h, v);

		//set the offset pearl object to point in the direction it will be thrown
		//also sets throw angle
		if (isPC) {
			rotatePearlOffset (Input.GetAxis ("right_analog_horizontal"), Input.GetAxis ("right_analog_vertical"));
		} else {
			rotatePearlOffset (Input.GetAxis ("right_analog_horizontalMac"), Input.GetAxis ("right_analog_verticalMac"));

		}


		//flip the orientation of the sprite if direction was changed
		if ( (facing_right && h < 0) || (!facing_right && h > 0) )
		{
			Flip();
		}

      

        //check for throw
		if (isPC) {
			if (Input.GetButton ("r_shoulder")) {
				throwPearl (); //will throw in the direction the pearl is currently pointing
			}
		} else {
			if (Input.GetButton ("r_shoulderMac")) {
				throwPearl (); //will throw in the direction the pearl is currently pointing
			}
		}
    }

    //point the beaver in the same direction as it is moving
    void rotateBeaver(float x, float y)
    {
        // cancel all input below the threshold
        if (Mathf.Abs(x) < L_analog_threshold) { x = 0.0f; }
        if (Mathf.Abs(y) < L_analog_threshold) { y = 0.0f; }

        
        // CALCULATE ANGLE AND ROTATE
        if (x != 0.0f || y != 0.0f)
        {
            if (facing_right)
            {
               facing_angle = (Mathf.Atan2(y, x) * Mathf.Rad2Deg);
            }
            else
            {
                facing_angle = (Mathf.Atan2(y, x) * Mathf.Rad2Deg)  + 180;
            }
            
			//beaver_sprite.transform.rotation = Quaternion.AngleAxis(facing_angle, Vector3.forward);
            transform.rotation = Quaternion.AngleAxis(facing_angle, Vector3.forward);
        }
    }

    //point the pearl based on right analog stick
    void rotatePearlOffset(float x, float y)
    {
   

        // cancel all input below this 
		if (Mathf.Abs(x) < R_analog_threshold) { x = 0.0f; }
        if (Mathf.Abs(y) < R_analog_threshold) { y = 0.0f; }

        // CALCULATE ANGLE AND ROTATE
        if (x != 0.0f || y != 0.0f)
		{
			if(isPC) {
				throw_angle = ((Mathf.Atan2(y, x) * Mathf.Rad2Deg) *-1 ) - 180;	// for PC
			} else {
				throw_angle = ((Mathf.Atan2(y, x) * Mathf.Rad2Deg)-90);			// for MAC
			}
			pearl_offset.transform.rotation = Quaternion.AngleAxis(throw_angle, Vector3.forward);
        }
    }

    //face the player right or left based on its horizontal speed
    void Flip()
    {
        animator.SetTrigger("turn");

        facing_right = !facing_right;

        Vector3 tmp = beaver_sprite.transform.localScale;
        tmp.x = tmp.x * -1;
        beaver_sprite.transform.localScale = tmp;
    }


	public void set_has_pearl(bool pearl)
	{
		has_pearl = pearl;
	}

	public bool get_has_pearl()
	{
		return has_pearl;
	}

	public void hide_pearl()
	{
		//transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		pearl_renderer.enabled = false;
	}


    // disables the rendering of the offset pearl, generates a new pearl, and adds a force to the new pearl
    void throwPearl()
    {
       
        //if the beaver is currently holding a pearl
        if(pearl_renderer.enabled)
        {
			has_pearl = false;
            //remove it from his grasp visually
            pearl_renderer.enabled = false;

			//disable beaver's collider temporarily
			gameObject.GetComponent<BoxCollider2D>().enabled = false;

			//turn it back on in a second. This is NOT THE FINAL SOLUTION
			Invoke("enableCollider", 1.0f);

            //throw the pearl in the right direction
            GameObject thrownPearl = Instantiate(Resources.Load("Pearl")) as GameObject;

			//the pearl starts on the player
            thrownPearl.transform.position = new Vector2(transform.position.x, transform.position.y);


			//find the angle to throw based on the angle found for the pearl_offest
			Vector3 dir = Quaternion.AngleAxis(throw_angle, Vector3.forward) *Vector3.up;
            
			//apply force to the pearl in that direction
			thrownPearl.GetComponent<Rigidbody2D>().AddForce(dir * throw_force);
           
        }
         
    

    }

	void enableCollider(){
		gameObject.GetComponent<BoxCollider2D>().enabled = true;
	}

}
