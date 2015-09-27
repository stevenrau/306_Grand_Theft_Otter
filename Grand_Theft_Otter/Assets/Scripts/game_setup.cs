using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class game_setup : MonoBehaviour {

	private int left_score = 0;
	private int right_score = 0;
	private int cur_left_point_structs;
	private int cur_right_point_structs;

	string[] structures;

	// Use this for initialization
	void Start () {
		structures = new string[5];
		structures [0] = "first_point";
		structures [1] = "second_point";
		structures [2] = "third_point";
		structures [3] = "fourth_point";
		structures [4] = "fifth_point";
	}

	void Awake()
	{
		Debug.Log (Screen.width);
		Debug.Log (Screen.height);

		/***************************************************************************************
		 * Load the Water Background
		 * ************************************************************************************/
		GameObject water_bg = Instantiate (Resources.Load ("Water_BG")) as GameObject;

		/***************************************************************************************
		 * Load the Sky Background
		 * ************************************************************************************/
		GameObject sky = Instantiate (Resources.Load ("Night_Sky")) as GameObject;

		/***************************************************************************************
		 * Load the water surface edge
		 * ************************************************************************************/
		GameObject water_surface_edge = Instantiate (Resources.Load ("Water_Surface_Edge")) as GameObject;

		/***************************************************************************************
		 * Load the seaweed barriers
		 * ************************************************************************************/
		GameObject seaweed_left = Instantiate(Resources.Load("Seaweed")) as GameObject;
		GameObject seaweed_right = Instantiate(Resources.Load("Seaweed")) as GameObject;
        
		//seaweed_left.transform.Translate(new Vector2 (-0.2f, -0.1f));
		Transform left_transform = seaweed_left.transform;
		seaweed_right.transform.position = new Vector3(Mathf.Abs (left_transform.position.x), left_transform.position.y, left_transform.position.z);
		seaweed_right.transform.localScale = new Vector2(-seaweed_left.transform.localScale.x, seaweed_left.transform.localScale.y);


		/***************************************************************************************
		 * Load the otter dens
		 * ************************************************************************************/

		GameObject otter_den_left = Instantiate(Resources.Load("Den")) as GameObject;
		GameObject otter_den_right = Instantiate(Resources.Load("Den")) as GameObject;
		left_transform = otter_den_left.transform;
		otter_den_right.transform.position = new Vector3(Mathf.Abs (left_transform.position.x), left_transform.position.y, left_transform.position.z);
		otter_den_right.transform.localScale = new Vector2(-otter_den_left.transform.localScale.x, otter_den_left.transform.localScale.y);

		/***************************************************************************************
		 * Load the scoring zones
		 * ************************************************************************************/
		GameObject scoring_zone_left = Instantiate (Resources.Load ("Scoring_Zone")) as GameObject;
		scoring_zone_left.name = "left_score_zone";
		GameObject scoring_zone_right = Instantiate (Resources.Load ("Scoring_Zone")) as GameObject;
		scoring_zone_right.name = "right_score_zone";
		left_transform = scoring_zone_left.transform;
		scoring_zone_right.transform.position = new Vector3(Mathf.Abs (left_transform.position.x), left_transform.position.y, left_transform.position.z);
		scoring_zone_right.transform.localScale = new Vector2(-scoring_zone_left.transform.localScale.x, scoring_zone_left.transform.localScale.y);

		/***************************************************************************************
		 * Load the floor
		 * ************************************************************************************/
		GameObject floor = Instantiate (Resources.Load ("Floor")) as GameObject;

		/***************************************************************************************
		 * Load the pearl
		 * ************************************************************************************/
		GameObject pearl = Instantiate (Resources.Load ("Pearl")) as GameObject;

		/***************************************************************************************
		 * Load the beavers (otters)
		 * ************************************************************************************/
		GameObject beaver1 = Instantiate (Resources.Load ("Beaver")) as GameObject;
        beaver1.name = "Beaver1";

        GameObject pearlOffset = Instantiate(Resources.Load("Pearl_Offset")) as GameObject;
        pearlOffset.transform.parent= beaver1.transform;
        pearlOffset.transform.position = new Vector3(beaver1.transform.position.x +0.25f, beaver1.transform.position.y-0.4f);
        
        pearlOffset.GetComponent<SpriteRenderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

		if (cur_left_point_structs < left_score) {
			GameObject cur_struct = Instantiate (Resources.Load (structures [left_score - 1])) as GameObject;

			cur_left_point_structs++;
		}
		else if (cur_right_point_structs < right_score) {
			GameObject cur_struct = Instantiate (Resources.Load (structures [right_score - 1])) as GameObject;

			cur_struct.transform.position = new Vector3(Mathf.Abs (cur_struct.transform.position.x), cur_struct.transform.position.y, cur_struct.transform.position.z);
			cur_struct.transform.localScale = new Vector2(-cur_struct.transform.localScale.x, cur_struct.transform.localScale.y);
			
			cur_right_point_structs++;
		}
	}

	public void increment_left_score()
	{
		left_score++;
	}

	public void increment_right_score()
	{
		right_score++;
	}

	public void print_left_score()
	{
		print (left_score);
	}

	public void print_right_score()
	{
		print (right_score);
	}
}
