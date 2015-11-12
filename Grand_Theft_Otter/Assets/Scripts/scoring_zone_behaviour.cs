using UnityEngine;
using System.Collections;

public class scoring_zone_behaviour : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		game_setup game_script = GameObject.Find ("game_setup").GetComponent<game_setup> ();

		if (other.tag == "Pearl") 
		{
			pearl_behaviour pearl_script = other.gameObject.GetComponent<pearl_behaviour> ();
			pearl_script.score_and_animate ();

			if (gameObject.name == "left_score_zone")
			{
				game_script.increment_left_score();
				
				game_script.print_left_score();
			}
			if (gameObject.name == "right_score_zone")
			{
				game_script.increment_right_score ();
				
				game_script.print_right_score();
			}
		} 
		else if (other.tag == "Player") 
		{
			collision_detection player_script = other.gameObject.GetComponent<collision_detection> ();

			if (player_script.GetHasPearl ()) 
			{
				player_script.HidePearl ();

				player_script.SetHasPearl(false);

				//Load the scored pearl object
				Instantiate (Resources.Load ("Pearl_Scored"), gameObject.transform.position, Quaternion.identity);

				//Wait a little bit before spawning a new pearl
				Invoke("create_new_pearl", 1.5f);

				if (gameObject.name == "left_score_zone")
				{
					game_script.increment_left_score();

					game_script.print_left_score();
				}
				if (gameObject.name == "right_score_zone")
				{
					game_script.increment_right_score ();

					game_script.print_right_score();
				}
			}
		}
	}

	void create_new_pearl()
	{
		Instantiate (Resources.Load ("Pearl"));
	}
}
