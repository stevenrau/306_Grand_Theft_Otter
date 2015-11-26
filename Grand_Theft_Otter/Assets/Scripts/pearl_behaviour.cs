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
	Rigidbody2D rBody; //the rigid body of this pearl

	//used to check if in water or in air, also to check if out of bounds
	Vector2 pos;

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
		rBody = GetComponent<Rigidbody2D> ();

		prevPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
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

		pos = transform.position;

		//change the gravity of the pearl if it has left the water
		if (pos.y >= constants.waterSurface) {
			rBody.gravityScale = constants.pearlAirGravity;
		} 
		else {
			rBody.gravityScale = constants.pearlWaterGravity;
		}


		//respawn the pearl if somehow it got out of the play area
		if (pos.x > constants.rightBoundary || pos.x < constants.leftBoundary || pos.y < constants.bottomBoundary) {
			SetRespawnTrue();
		}
	}

	void SetRespawnTrue()
	{
		respawn = true;
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


	void OnCollisionEnter2D(Collision2D col){

		if (col.gameObject.tag == "Wall") {
			print ("collison with wall");
		}
	}


}
