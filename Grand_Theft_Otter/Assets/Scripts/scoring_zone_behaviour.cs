using UnityEngine;
using System.Collections;

public class scoring_zone_behaviour : MonoBehaviour {

	// Clam animator
	Animator animator;

	// the script that keeps track of scores for the round
	score_keeper scoreKeeperScript;

	//the position the pearl will respawn at
	GameObject spawnPoint;

	void Start()
	{

		animator = gameObject.GetComponentInParent<Animator>();
		scoreKeeperScript = GameObject.Find ("Score_Keeper").GetComponent<score_keeper> ();
		spawnPoint = GameObject.Find ("Pearl_Spawn");
	}

    void OnTriggerEnter2D(Collider2D other)
	{
		 
		if (other.tag == "Pearl") 
		{
			Destroy(other.gameObject);

			if (gameObject.tag == "Left_Clam")
			{
				scoreKeeperScript.IncrementLeftScore();
			}
			else if (gameObject.tag == "Right_Clam")
			{
				scoreKeeperScript.IncrementRightScore ();
			}

			animator.SetTrigger("scored");

			//Wait a little bit before spawning a new pearl
			Invoke("CreateNewPearl", 1.5f);
		} 
		else if (other.tag == "Player") 
		{
			player_state playerStateScript = other.gameObject.GetComponentInParent<player_state>();
			collision_detection playerCollisionScript = other.gameObject.GetComponentInParent<collision_detection>(); 

			if (playerStateScript.GetHasPearl ()) 
			{
				playerCollisionScript.HidePearl ();

                playerStateScript.SetHasPearl(false);

				if (gameObject.tag == "Left_Clam")
				{
					scoreKeeperScript.IncrementLeftScore();
				}
				else if (gameObject.tag == "Right_Clam")
				{
					scoreKeeperScript.IncrementRightScore ();
				}

				animator.SetTrigger("scored");

				//Wait a little bit before spawning a new pearl
				Invoke("CreateNewPearl", 1.5f);
			}
		}
	}



	void CreateNewPearl()
	{
		GameObject newPearl = Instantiate (Resources.Load ("Pearl"), spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
		newPearl.GetComponent<Rigidbody2D> ().AddForce(new Vector2(0, -200.0f)); //shoot the pearl down from the spawnpoint
	}
}
