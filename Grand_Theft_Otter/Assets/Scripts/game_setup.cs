using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class game_setup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
		GameObject scoring_zone_right = Instantiate (Resources.Load ("Scoring_Zone")) as GameObject;
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
	
	}
}
