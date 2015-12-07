using UnityEngine;
using System.Collections;

public class scoring_zone_behaviour : MonoBehaviour {

	private Color leftScoredColor = new Color(0.6f, 0.95f, 0.6f, 1f); // Darkened green
	private Color rightScoredColor = new Color(0.95f, 0.6f, 0.6f, 1f); //Darkened red

	// Clam animator
	Animator animator;

	// Clam sprite that gets animated
	SpriteRenderer clamSprite;

	sound_player soundPlayer;
	public AudioClip scoreSound1;
	public AudioClip scoreSound2;

	// the script that keeps track of scores for the round
	score_keeper scoreKeeperScript;

	//the position the pearl will respawn at
	GameObject[] spawnPoints;

	void Start()
	{
		clamSprite = gameObject.GetComponentInParent<SpriteRenderer>();
		animator = gameObject.GetComponentInParent<Animator>();
		scoreKeeperScript = GameObject.Find ("Score_Keeper").GetComponent<score_keeper> ();


		soundPlayer = GameObject.Find ("Sound_Player").GetComponent<sound_player>();

		spawnPoints = GameObject.FindGameObjectsWithTag("Jellyfish");
         
	}

    void OnTriggerEnter2D(Collider2D other)
	{
		 
		if (other.tag == "Pearl") 
		{
			Destroy(other.gameObject);
			if (Random.value > 0.5) {
				soundPlayer.PlayClip(scoreSound1, 1.0f);

			}
			else { 

				soundPlayer.PlayClip(scoreSound2, 1.0f);

			}

			if (gameObject.tag == "Left_Clam")
			{
				scoreKeeperScript.IncrementLeftScore();

				clamSprite.material.color = leftScoredColor;
			}
			else if (gameObject.tag == "Right_Clam")
			{
				scoreKeeperScript.IncrementRightScore ();

				clamSprite.material.color = rightScoredColor;
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

					clamSprite.material.color = leftScoredColor;
				}
				else if (gameObject.tag == "Right_Clam")
				{
					scoreKeeperScript.IncrementRightScore ();

					clamSprite.material.color = rightScoredColor;
				}

				animator.SetTrigger("scored");

				//Wait a little bit before spawning a new pearl
				Invoke("CreateNewPearl", 1.5f);
			}
		}
	}



	void CreateNewPearl()
	{
		Vector2 spawnPos;
		float roll = Random.value;

		//spawn pearl at a random jellyfish
		if (roll > 0.5) {
			spawnPos = spawnPoints[0].transform.position;
		}
		else { 
			spawnPos = spawnPoints[1].transform.position;
		}
		
		GameObject newPearl = Instantiate (Resources.Load ("Pearl"), spawnPos, Quaternion.identity) as GameObject;
		newPearl.GetComponent<Rigidbody2D> ().AddForce(new Vector2(0, -200.0f)); //shoot the pearl down from the spawnpoint
	}
}
