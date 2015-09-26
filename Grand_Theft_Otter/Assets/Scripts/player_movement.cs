using UnityEngine;
using System.Collections;

public class player_movement : MonoBehaviour {

    Rigidbody2D r_body; //the body of the player
    Animator animator; //the animator for the player

    public float move_force;
    public float max_speed;
    float facing_angle;

	// Use this for initialization
	void Start () {

        r_body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        

	}
	
	// Update is called once per frame
	void Update () {

        float h = Input.GetAxis("left_analog_horizontal");

        float v = Input.GetAxis("left_analog_vertical");

        r_body.AddForce(new Vector2(move_force * h, 0));
        r_body.AddForce(new Vector2(0, move_force * v));

        //set animation
        if ( (h > 0.01) || (v > 0.01))
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
            r_body.velocity = new Vector2( r_body.velocity.x, max_speed*-1);
        }


        // ROTATE A GUN OBJECT AROUND THE Z-AXIS
        // BASED ON THE ANGLE OF THE RIGHT ANALOG STICK
        float x = Input.GetAxis("right_analog_horizontal");
        float y = Input.GetAxis("right_analog_vertical");
        float aim_angle = 0.0f;
        
        // USED TO CHECK OUTPUT
        //Debug.Log(" horz: " + x + "   vert: " + y);

        // CANCEL ALL INPUT BELOW THIS FLOAT
        float R_analog_threshold = 0.20f;

        if (Mathf.Abs(x) < R_analog_threshold) { x = 0.0f; }

        if (Mathf.Abs(y) < R_analog_threshold) { y = 0.0f; }

        // CALCULATE ANGLE AND ROTATE
        if (x != 0.0f || y != 0.0f)
        {

            aim_angle = (Mathf.Atan2(y, x) * Mathf.Rad2Deg) *-1 -180;

            // ANGLE GUN
            transform.rotation = Quaternion.AngleAxis(aim_angle, Vector3.forward);
        }


    }
}
