//using UnityEngine;
//using System.Collections;

//public class moving : MonoBehaviour {

//    Rigidbody2D r_body; //the body of the player
//    Animator animator; //the animator for the beaver sprite

//    public float move_force;
//    public float max_speed;
//    bool facing_right; //is the player facing right
//    float facing_angle; //the angle the beaver is looking( what way its head is pointing)

//    GameObject beaver_sprite; //the child object of player that displays the beaver and animates it

//    public GameObject throwInputScript;

//    float L_analog_threshold; //the analog stick must be moved more than this to trigger moving animation
//    float R_analog_threshold;

//    // Use this for initialization
//    void Start () {

//        r_body = GetComponent<Rigidbody2D>();
//        //get reference to the animator located on the beaver_sprite child object
//        animator = beaver_sprite.GetComponent<Animator>();

//        throwInputScript = transform.GetComponent<get_input>();
        
//        facing_angle = 0.0f;

//        facing_right = true; //facing right initially

//        L_analog_threshold = 0.001f;
//        R_analog_threshold = 0.20f;
//    }
	
//	// Update is called once per frame
//	void Update () {
//        //move the player position 

//        //for xbox controller


//        float h = Input.GetAxis(throwInputScript.GetHorizontal); //mov_horiz
//        float v = Input.GetAxis(throwInputScript.GetVertical); //mov_vert


//        //for key board
//        //float h = Input.GetAxis("Horizontal");
//        //float v = Input.GetAxis("Vertical");


//        //set animation if stick is moved enough
//        if ((Mathf.Abs(h) > L_analog_threshold) || (Mathf.Abs(v) > L_analog_threshold))
//        {

//            //show the player moving animation
//            animator.SetBool("is_moving", true);

//            //move the player in the direction of the input
//            r_body.AddForce(new Vector2(move_force * h, 0));
//            r_body.AddForce(new Vector2(0, move_force * v));

//        }
//        else
//        {
//            animator.SetBool("is_moving", false);
//        }

//        //limit to max speed in x
//        if (r_body.velocity.x > max_speed)
//        {
//            r_body.velocity = new Vector2(max_speed, r_body.velocity.y);
//        }
//        else if (r_body.velocity.x < max_speed * -1)
//        {
//            r_body.velocity = new Vector2(max_speed * -1, r_body.velocity.y);
//        }

//        //limit to max speed in y
//        if (r_body.velocity.y > max_speed)
//        {
//            r_body.velocity = new Vector2(r_body.velocity.x, max_speed);
//        }
//        else if (r_body.velocity.y < max_speed * -1)
//        {
//            r_body.velocity = new Vector2(r_body.velocity.x, max_speed * -1);
//        }

//        //point the beaver_sprite in the same direction as it is moving
//        rotateBeaver(h, v);

//        //set the offset pearl object to point in the direction it will be thrown
//        //also sets throw angle

        
//        //flip the orientation of the sprite if direction was changed
//        if ((facing_right && h < 0) || (!facing_right && h > 0))
//        {
//            Flip();
//        }
        
      
//    }

//    //point the beaver in the same direction as it is moving
//    void rotateBeaver(float x, float y)
//    {
//        // cancel all input below the threshold
//        if (Mathf.Abs(x) < L_analog_threshold) { x = 0.0f; }
//        if (Mathf.Abs(y) < L_analog_threshold) { y = 0.0f; }


//        // CALCULATE ANGLE AND ROTATE
//        if (x != 0.0f || y != 0.0f)
//        {
//            if (facing_right)
//            {
//                facing_angle = (Mathf.Atan2(y, x) * Mathf.Rad2Deg);
//            }
//            else
//            {
//                facing_angle = (Mathf.Atan2(y, x) * Mathf.Rad2Deg) + 180;
//            }

//            //beaver_sprite.transform.rotation = Quaternion.AngleAxis(facing_angle, Vector3.forward);
//            transform.rotation = Quaternion.AngleAxis(facing_angle, Vector3.forward);
//        }
//    }

//    //face the player right or left based on its horizontal speed
//    void Flip()
//    {
//        animator.SetTrigger("turn");

//        facing_right = !facing_right;

//        Vector3 tmp = beaver_sprite.transform.localScale;
//        tmp.x = tmp.x * -1;
//        beaver_sprite.transform.localScale = tmp;
//    }
//}
