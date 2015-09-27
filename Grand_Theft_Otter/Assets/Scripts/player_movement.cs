using UnityEngine;
using System.Collections;

public class player_movement : MonoBehaviour {

    Rigidbody2D r_body; //the body of the player
    Animator animator; //the animator for the player

    bool facing_right;
    public float move_force;
    public float max_speed;
    public float throw_force;

    private float throw_x, throw_y;

	private bool has_pearl = false;

    GameObject pearlOffset; //this is a pearl that will indicate the direction it will be thrown
    float facing_angle; //the angle the beaver is looking( what way its head is pointing)
    float throw_angle; // the angle the pearl will be thrown

	// Use this for initialization
	void Start () {

        r_body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        throw_angle = 0.0f;
        facing_angle = 0.0f;

        facing_right = true;
        throw_force = 600;

        pearlOffset = transform.GetChild(0).gameObject; //get the offset pearl for aiming purposes

	}

    // Update is called once per frame
    void Update() {

        //move the player position 
        float h = Input.GetAxis("left_analog_horizontal");
        float v = Input.GetAxis("left_analog_vertical");
        r_body.AddForce(new Vector2(move_force * h, 0));
        r_body.AddForce(new Vector2(0, move_force * v));

        

        //set animation
        if ((h > 0.01) || (v > 0.01))
        {
            animator.SetBool("is_moving", true);
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

        //point the beaver in the same direction as it is moving
        rotateBeaver(h, v);


        /////////

        //set the offset pearl object to point in the direction it will be thrown
        //also sets throw angle
        rotatePearlOffset(Input.GetAxis("right_analog_horizontal"), Input.GetAxis("right_analog_vertical"));

        //check for throw
        if (Input.GetButton("r_shoulder"))
        {
            throwPearl(); //will throw in the direction the pearl is currently facing
        }

        if (facing_right && h < 0)
        {
            Flip();
        }
        else if (!facing_right && h > 0)
        {
            Flip();
        }

    }

    //point the beaver in the same direction as it is moving
    void rotateBeaver(float x, float y)
    {
        // CANCEL ALL INPUT BELOW THIS FLOAT
        float L_analog_threshold = 0.50f;
      
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
            
            transform.rotation = Quaternion.AngleAxis(facing_angle, Vector3.forward);
        }
    }

    //point the pearl based on left analog stick
    void rotatePearlOffset(float x, float y)
    {
        //set the direction to throw
        throw_x = x;
        throw_y = y;

        // USED TO CHECK OUTPUT
        //Debug.Log(" horz: " + x + "   vert: " + y);

        // CANCEL ALL INPUT BELOW THIS FLOAT
        float L_analog_threshold = 0.20f;

        if (Mathf.Abs(x) < L_analog_threshold) { x = 0.0f; }

        if (Mathf.Abs(y) < L_analog_threshold) { y = 0.0f; }

        // CALCULATE ANGLE AND ROTATE
        if (x != 0.0f || y != 0.0f)
        {
            if (facing_right)
            {
                throw_angle = (Mathf.Atan2(y, x) * Mathf.Rad2Deg) * -1 - 180;
            }
            else
            {
                throw_angle = (Mathf.Atan2(y, x) * Mathf.Rad2Deg)  -180 ;
            }
           
            pearlOffset.transform.rotation = Quaternion.AngleAxis(throw_angle, Vector3.forward);
        }
    }

    //face the player right or left based on its horizontal speed
    void Flip()
    {
        animator.SetTrigger("turn");

        facing_right = !facing_right;

        Vector3 tmp = transform.localScale;
        tmp.x = tmp.x * -1;
        transform.localScale = tmp;
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
		transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().enabled = false;
	}


    // disables the rendering of the offset pearl, generates a new pearl, and adds a force to the new pearl
    void throwPearl()
    {
       
        //if the beaver is currently holding a pearl
        if(pearlOffset.GetComponent<SpriteRenderer>().enabled)
        {
            //remove it from his grasp visually
            GameObject offsetPearl = transform.GetChild(0).gameObject;
            offsetPearl.GetComponent<SpriteRenderer>().enabled = false;

            //throw the pearl in the right direction
            GameObject thrownPearl = Instantiate(Resources.Load("Pearl")) as GameObject;
            

            Vector3 dir;
            if (facing_right)
            {
                thrownPearl.transform.position = new Vector2(transform.position.x + 1, transform.position.y);
                //dir = Quaternion.AngleAxis(throw_angle, Vector3.forward) * Vector3.up;

                if(throw_x < 0)
                {
                    throw_x *= -1;
                }
                else
                {

                }

                if (throw_y < 0)
                {
                    
                }
                else
                {
                    throw_y *= -1;
                }

            }
            else
            {
                thrownPearl.transform.position = new Vector2(transform.position.x - 1, transform.position.y);
                //dir = Quaternion.AngleAxis(throw_angle -180, Vector3.forward) * Vector3.up;

                if (throw_x < 0)
                {
                    
                }
                else
                {
                    throw_x *= 1;
                }

                if(throw_y < 0)
                {
                    throw_y *= -1;
                }
                else
                {

                }
                //throw_x *= 1;
                
                
            }
           
            thrownPearl.GetComponent<Rigidbody2D>().AddForce(new Vector2(throw_x * throw_force, throw_y* throw_force)); 
        }
         
    

    }

}
