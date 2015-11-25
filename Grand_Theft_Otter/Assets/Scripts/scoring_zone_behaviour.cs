using UnityEngine;
using System.Collections;

public class scoring_zone_behaviour : MonoBehaviour {

	// Clam animator
	Animator animator;

	private int leftScore = 0;
	private int rightScore = 0;
	
	private int maxScore = 5;
	
	GameObject damRampLeft;
	GameObject damRampRight;


    void Start()
    {
		animator = gameObject.GetComponentInParent<Animator>();
		damRampLeft = GameObject.Find ("Dam_Ramp_Left");
		damRampRight = GameObject.Find ("Dam_Ramp_Right");
    }

    void OnTriggerEnter2D(Collider2D other)
	{
		 

		if (other.tag == "Pearl") 
		{
			Destroy(other.gameObject);

			if (gameObject.tag == "Left_Clam")
			{
				IncrementLeftScore();
			}
			else if (gameObject.tag == "Right_Clam")
			{
				IncrementRightScore ();
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
					IncrementLeftScore();
				}
				else if (gameObject.tag == "Right_Clam")
				{
					IncrementRightScore ();
				}

				animator.SetTrigger("scored");

				//Wait a little bit before spawning a new pearl
				Invoke("CreateNewPearl", 1.5f);
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
