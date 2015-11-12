using UnityEngine;
using System.Collections;

public class scoring_zone_behaviour : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		game_setup gameScript = GameObject.Find ("game_setup").GetComponent<game_setup> ();

		if (other.tag == "Pearl") 
		{
			pearl_behaviour pearlScript = other.gameObject.GetComponent<pearl_behaviour> ();
			pearlScript.ScoreAndAnimate ();

			if (gameObject.name == "left_score_zone")
			{
				gameScript.IncrementLeftScore();
				
				gameScript.PrintLeftScore();
			}
			if (gameObject.name == "right_score_zone")
			{
				gameScript.IncrementRightScore ();
				
				gameScript.PrintRightScore();
			}
		} 
		else if (other.tag == "Player") 
		{
			collision_detection playerScript = other.gameObject.GetComponent<collision_detection> ();

			if (playerScript.GetHasPearl ()) 
			{
				playerScript.HidePearl ();

				playerScript.SetHasPearl(false);

				//Load the scored pearl object
				Instantiate (Resources.Load ("Pearl_Scored"), gameObject.transform.position, Quaternion.identity);

				//Wait a little bit before spawning a new pearl
				Invoke("CreateNewPearl", 1.5f);

				if (gameObject.name == "left_score_zone")
				{
					gameScript.IncrementLeftScore();

					gameScript.PrintLeftScore();
				}
				if (gameObject.name == "right_score_zone")
				{
					gameScript.IncrementRightScore ();

					gameScript.PrintRightScore();
				}
			}
		}
	}

	void CreateNewPearl()
	{
		Instantiate (Resources.Load ("Pearl"));
	}
}
