using UnityEngine;
using System.Collections;

public class scoring_zone_behaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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
			else if (gameObject.name == "right_score_zone");
			{
				game_script.increment_right_score ();
				
				game_script.print_right_score();
			}
		} 
		else if (other.tag == "Player") 
		{
			player_movement player_script = other.gameObject.GetComponent<player_movement> ();

			if (player_script.get_has_pearl ()) 
			{
				player_script.hide_pearl ();

				player_script.set_has_pearl(false);

				GameObject scored_pearl = Instantiate (Resources.Load ("Pearl_Scored"), gameObject.transform.position, Quaternion.identity) as GameObject;

				Invoke("create_new_pearl", 1.5f);

				if (gameObject.name == "left_score_zone")
				{
					game_script.increment_left_score();

					game_script.print_left_score();
				}
				else if (gameObject.name == "right_score_zone");
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
