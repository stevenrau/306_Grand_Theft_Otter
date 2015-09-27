using UnityEngine;
using System.Collections;

public class pearl_behaviour : MonoBehaviour {

	private GameObject spawn_point;  //The pearl spawn point
	private bool respawn = false;
	private Transform transform;    //The transform component for the pearl

	private Vector3 prev_pos;      //The pearl's position from last frame (detect motion)
	private Vector2 cur_pos;       //The pearl's position in the current frame

	Animator animator; //the animator for the pearl

	// Use this for initialization
	void Start () {
		//Instantiate the spawn point and set it to the global var
		spawn_point = Instantiate (Resources.Load ("Pearl_Spawn")) as GameObject;

		transform = GetComponent<Transform> ();
		animator = GetComponent<Animator>();

		prev_pos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
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

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {

            GameObject player = other.gameObject;

            GameObject pearlOffset = player.transform.GetChild(0).gameObject;
            pearlOffset.GetComponent<SpriteRenderer>().enabled = true;

            Destroy(gameObject);

			//respawn = true;
		}
	}

	void set_respawn_true()
	{
		respawn = true;
	}

	public void score_and_animate()
	{
		animator.SetTrigger ("score");

		Invoke ("set_respawn_true", 2.0f);
	}
}
