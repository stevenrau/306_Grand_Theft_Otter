using UnityEngine;
using System.Collections;

public class player_movement : MonoBehaviour {

    Rigidbody2D r_body; //the body of the player
    Animator animator; //the animator for the player

    bool facing_right;
    public float move_force;
    public float max_speed;

    GameObject pearlOffset; //this is a pearl that will indicate the direction it will be thrown

	// Use this for initialization
	void Start () {

        r_body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        facing_right = true;

        pearlOffset = transform.GetChild(0).gameObject; //get the offset pearl for aiming purposes

	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log("facing_right =" + facing_right);

        //move the player position 
        float h = Input.GetAxis("left_analog_horizontal");
        float v = Input.GetAxis("left_analog_vertical");
        r_body.AddForce(new Vector2(move_force * h, 0));
        r_body.AddForce(new Vector2(0, move_force * v));

        if (facing_right && h < 0)
        {
            Flip();
        }
        else if (!facing_right && h > 0)
        {
            Flip();
        }

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

        //point the beaver in the same direction as it is moving
        rotateBeaver();

        //set the offset pearl object to point in the direction it will be thrown
        rotatePearlOffset();
    }

    //point the beaver in the same direction as it is moving
    void rotateBeaver()
    {
        //get values of analog stick
        float x = Input.GetAxis("left_analog_horizontal");
        float y = Input.GetAxis("left_analog_vertical");
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
            if (facing_right)
            {
                aim_angle = (Mathf.Atan2(y, x) * Mathf.Rad2Deg);
            }
            else
            {
                aim_angle = (Mathf.Atan2(y, x) * Mathf.Rad2Deg)  + 180;
            }
            
            transform.rotation = Quaternion.AngleAxis(aim_angle, Vector3.forward);
        }
    }

    //point the pearl based on left analog stick
    void rotatePearlOffset()
    {
        //get values of analog stick
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
            if (facing_right)
            {
                aim_angle = (Mathf.Atan2(y, x) * Mathf.Rad2Deg) * -1 - 180;
            }
            else
            {
                aim_angle = (Mathf.Atan2(y, x) * Mathf.Rad2Deg)  -180 ;
            }
           
            pearlOffset.transform.rotation = Quaternion.AngleAxis(aim_angle, Vector3.forward);
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

}
