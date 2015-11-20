using UnityEngine;
using System.Collections;

public class pearl_behaviour : MonoBehaviour {

	private GameObject spawnPoint; //The pearl spawn point
	private bool respawn = false;
	private Transform transform;    //The transform component for the pearl

	private Vector3 prevPos;       //The pearl's position from last frame (detect motion)
	private Vector2 curPos;        //The pearl's position in the current frame
	private GameObject beaver;	    //The beaver that spawned it

	private bool hasLeftBeaver = false;	//Check if pearl has exited beaver's collider

	Animator animator; 				//the animator for the pearl

	void Awake()
	{
		//Instantiate the spawn point and set it to the global var
		spawnPoint = GameObject.Find ("Pearl_Spawn");
		//spawnPoint = Instantiate(Resources.Load("Pearl_Spawn")) as GameObject;
	}

	// Use this for initialization
	void Start ()
	{
		transform = GetComponent<Transform> ();
		animator = GetComponent<Animator>();

		prevPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
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
			transform.position = spawnPoint.transform.position;
			animator.SetTrigger ("has_respawned");

			respawn = false;
		}

		// Check for motion and animate appropriately
		if ((prevPos.x != this.transform.position.x) || 
			(prevPos.y != this.transform.position.y)) {
			animator.SetBool ("in_motion", true);
		} else {
			animator.SetBool ("in_motion", false);
		}

		prevPos = this.transform.position;
	}

	void SetRespawnTrue()
	{
		respawn = true;
	}

	public void ScoreAndAnimate()
	{
		animator.SetTrigger ("score");

		Invoke ("SetRespawnTrue", 2.0f);
	}

	// returns the beaver that instantiated this pearl
	public GameObject GetBeaver()
	{
		if (this.beaver != null)
		{
			return this.beaver;
		}
		else
		{
			return null;
		}
	}

	// sets the pearl's instantiator
	public void SetBeaver(GameObject beav)
	{
		this.beaver = beav;
	}

}
