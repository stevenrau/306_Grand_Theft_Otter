using UnityEngine;
using System.Collections;

public class pearl_behaviour : MonoBehaviour {

	private GameObject spawn_point; //The pearl spawn point
	private bool respawn = false;
	private Transform transform;    //The transform component for the pearl

	private Vector3 prev_pos;       //The pearl's position from last frame (detect motion)
	private Vector2 cur_pos;        //The pearl's position in the current frame
	private GameObject beaver;	    //The beaver that spawned it

	private bool has_left_beaver = false;	//Check if pearl has exited beaver's collider

	Animator animator; 				//the animator for the pearl

	// Use this for initialization
	void Start () {



		transform = GetComponent<Transform> ();
		animator = GetComponent<Animator>();

		prev_pos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		// draw ray cast, if going to collide then reduce velocity.

        if (transform.position.x < -8.5)
        {
            Transform temp = transform;
            transform.position = new Vector2(-4,temp.position.y);
               
        }

        if (transform.position.x > 8.5)
        {
            Transform temp = transform;
            transform.position = new Vector2(4, temp.position.y);

        }


        if (respawn) {
			transform.position = spawn_point.transform.position;
			animator.SetTrigger ("has_respawned");

			respawn = false;
		}

		// Check for motion and animate appropriately
		if ((prev_pos.x != this.transform.position.x) || 
			(prev_pos.y != this.transform.position.y)) {
			animator.SetBool ("in_motion", true);
		} else {
			animator.SetBool ("in_motion", false);
		}

		prev_pos = this.transform.position;
	}

/*
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {

			/
			 * This should fix the beaver's collider issue.
			 * The idea is: spawn the pearl inside the beaver that is throwing.
			 * While the pearl is inside the beaver's collider don't collide with the beaver.
			 * But once the pearl has left the Beaver then maybe have a cool down
			 * till the beaver can pick up the pearl again.
			 * At any time any other beaver can pick up the pearl.
			 * TODO: once the pearl leaves than flip has_left_beaver to true.
			 / 
			//		if(has_left_beaver || other != this.getBeaver()){}
            GameObject player = other.gameObject;

            GameObject pearlOffset = player.transform.GetChild(0).gameObject;
            pearlOffset.GetComponent<SpriteRenderer>().enabled = true;

            Destroy(gameObject);

			player_movement player_script = other.gameObject.GetComponent<player_movement>();
			player_script.set_has_pearl(true);
			//respawn = true;



		}
	}
*/
	void set_respawn_true()
	{
		respawn = true;
	}

	public void score_and_animate()
	{
		animator.SetTrigger ("score");

		Invoke ("set_respawn_true", 2.0f);
	}

    void Awake()
    {
        //Instantiate the spawn point and set it to the global var
        spawn_point = Instantiate(Resources.Load("Pearl_Spawn")) as GameObject;
    }

	// returns the beaver that instantiated this pearl
	public GameObject getBeaver() {
		if (this.beaver != null) {
			return this.beaver;
		} else {
			return null;
		}
	}

	// sets the pearl's instantiator
	public void setBeaver(GameObject beav) {
		this.beaver = beav;
	}

}
