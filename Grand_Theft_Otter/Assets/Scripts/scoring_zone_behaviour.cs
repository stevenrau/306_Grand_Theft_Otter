using UnityEngine;
using System.Collections;

public class scoring_zone_behaviour : MonoBehaviour {

    player_state playerStateScript;
	game_setup gameSetupScript;

	private int leftScore = 0;
	private int rightScore = 0;
	
	private int maxScore = 5;
	
	GameObject damRampLeft;
	GameObject damRampRight;


    void Start()
    {
        playerStateScript = gameObject.GetComponent<player_state>();
		gameSetupScript = GameObject.Find ("game_setup").GetComponent<game_setup> ();
		damRampLeft = GameObject.Find ("Dam_Ramp_Left");
		damRampRight = GameObject.Find ("Dam_Ramp_Right");
    }

    void OnTriggerEnter2D(Collider2D other)
	{
		 

		if (other.tag == "Pearl") 
		{
			pearl_behaviour pearlScript = other.gameObject.GetComponent<pearl_behaviour> ();
			pearlScript.ScoreAndAnimate ();

			if (gameObject.name == "left_score_zone")
			{
				IncrementLeftScore();
			}
			if (gameObject.name == "right_score_zone")
			{
				IncrementRightScore ();
			}
		} 
		else if (other.tag == "Player") 
		{
			collision_detection playerScript = other.gameObject.GetComponentInParent<collision_detection>(); 

			if (playerStateScript.GetHasPearl ()) 
			{
				playerScript.HidePearl ();

                playerStateScript.SetHasPearl(false);

				//Load the scored pearl object
				Instantiate (Resources.Load ("Pearl_Scored"), gameObject.transform.position, Quaternion.identity);

				//Wait a little bit before spawning a new pearl
				Invoke("CreateNewPearl", 1.5f);

				if (gameObject.name == "left_score_zone")
				{
					IncrementLeftScore();
				}
				if (gameObject.name == "right_score_zone")
				{
					IncrementRightScore ();
				}
			}
		}
	}

	//keep score and build the bridge further with each point scored
	public void IncrementLeftScore()
	{
		if (leftScore < maxScore) {
			leftScore++;
			//enable the next section of the bridge
			damRampLeft.transform.GetChild (leftScore - 1).gameObject.SetActive (true);
		}
		print ("Left score: " + leftScore);
	}
	
	public void IncrementRightScore()
	{
		if (rightScore < maxScore) {
			rightScore++;
			//enable the next section of the bridge
			damRampRight.transform.GetChild (rightScore - 1).gameObject.SetActive (true);
		}
		print ("Right score: " + rightScore);
	}

	void CreateNewPearl()
	{
		Instantiate (Resources.Load ("Pearl"));
	}
}
