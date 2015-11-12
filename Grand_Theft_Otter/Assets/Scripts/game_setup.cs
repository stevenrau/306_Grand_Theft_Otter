using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class game_setup : MonoBehaviour {

	private int leftScore = 0;
	private int rightScore = 0;
	private int curLeftPointStructs;
	private int curRightPointStructs;

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
		GameObject waterBg = Instantiate (Resources.Load ("Water_BG")) as GameObject;

		/***************************************************************************************
		 * Load the Sky Background
		 * ************************************************************************************/
		GameObject sky = Instantiate (Resources.Load ("Night_Sky")) as GameObject;

		/***************************************************************************************
		 * Load the water surface edge
		 * ************************************************************************************/
		GameObject waterSurfaceEdge = Instantiate (Resources.Load ("Water_Surface_Edge")) as GameObject;

		/***************************************************************************************
		 * Load the seaweed barriers
		 * ************************************************************************************/
		/*GameObject seaweedLeft = Instantiate(Resources.Load("Seaweed")) as GameObject;
		GameObject seaweedRight = Instantiate(Resources.Load("Seaweed")) as GameObject;
        
		//seaweed_left.transform.Translate(new Vector2 (-0.2f, -0.1f));
		Transform left_transform = seaweedLeft.transform;
		seaweedRight.transform.position = new Vector3(Mathf.Abs (left_transform.position.x), left_transform.position.y, left_transform.position.z);
		seaweedRight.transform.localScale = new Vector2(-seaweedLeft.transform.localScale.x, seaweedLeft.transform.localScale.y);*/

		GameObject wallLeft = Instantiate(Resources.Load("Wall_Textured")) as GameObject;
		GameObject wallRight = Instantiate(Resources.Load("Wall_Textured")) as GameObject;
		
		//seaweed_left.transform.Translate(new Vector2 (-0.2f, -0.1f));
		Transform leftTransform = wallLeft.transform;
		wallRight.transform.position = new Vector3(Mathf.Abs (leftTransform.position.x), leftTransform.position.y, leftTransform.position.z);
		wallRight.transform.localScale = new Vector2(-wallLeft.transform.localScale.x, wallLeft.transform.localScale.y);


		/***************************************************************************************
		 * Load the otter dens
		 * ************************************************************************************/

		GameObject otterDenLeft = Instantiate(Resources.Load("Den")) as GameObject;
		GameObject otterDenRight = Instantiate(Resources.Load("Den")) as GameObject;
		leftTransform = otterDenLeft.transform;
		otterDenRight.transform.position = new Vector3(Mathf.Abs (leftTransform.position.x), leftTransform.position.y, leftTransform.position.z);
		otterDenRight.transform.localScale = new Vector2(-otterDenLeft.transform.localScale.x, otterDenLeft.transform.localScale.y);

		/***************************************************************************************
		 * Load the scoring zones
		 * ************************************************************************************/
		GameObject scoringZoneLeft = Instantiate (Resources.Load ("Scoring_Zone")) as GameObject;
		scoringZoneLeft.name = "left_score_zone";
		GameObject scoringZoneRight = Instantiate (Resources.Load ("Scoring_Zone")) as GameObject;
		scoringZoneRight.name = "right_score_zone";
		leftTransform = scoringZoneLeft.transform;
		scoringZoneRight.transform.position = new Vector3(Mathf.Abs (leftTransform.position.x), leftTransform.position.y, leftTransform.position.z);
		scoringZoneRight.transform.localScale = new Vector2(-scoringZoneLeft.transform.localScale.x, scoringZoneLeft.transform.localScale.y);

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
		GameObject beaver1 = Instantiate (Resources.Load ("Beaver_Player")) as GameObject;
        beaver1.name = "Beaver1";
		beaver1.transform.GetChild (1).gameObject.GetComponent<SpriteRenderer> ().color = Color.blue;
//		beaver1.GetComponent<player_movement>().setPlayerId ("1");
		beaver1.GetComponent<get_input>().SetPlayerID ("1");


		GameObject beaver2 = Instantiate (Resources.Load ("Beaver_Player")) as GameObject;
		beaver2.name = "Beaver2";
		beaver2.transform.GetChild (1).gameObject.GetComponent<SpriteRenderer> ().color = Color.green;

//		beaver2.GetComponent<player_movement>().setPlayerId ("2");
		beaver2.GetComponent<get_input>().SetPlayerID ("2");
		beaver2.transform.position = new Vector2 (4.0f, 0.0f);
        
		/*
        GameObject pearlOffset = Instantiate(Resources.Load("Pearl_Offset")) as GameObject;
        pearlOffset.transform.parent= beaver1.transform;
        pearlOffset.transform.position = new Vector3(beaver1.transform.position.x +0.25f, beaver1.transform.position.y-0.4f);
        
        pearlOffset.GetComponent<SpriteRenderer>().enabled = false;
        */
    }
	
	// Update is called once per frame
	void Update () {

		if (curLeftPointStructs < leftScore) {
			GameObject cur_struct = Instantiate (Resources.Load (structures [leftScore - 1])) as GameObject;

			curLeftPointStructs++;
		}

		else if (curRightPointStructs < rightScore) {
			GameObject cur_struct = Instantiate (Resources.Load (structures [rightScore - 1])) as GameObject;

			cur_struct.transform.position = new Vector3(Mathf.Abs (cur_struct.transform.position.x), cur_struct.transform.position.y, cur_struct.transform.position.z);
			cur_struct.transform.localScale = new Vector2(-cur_struct.transform.localScale.x, cur_struct.transform.localScale.y);
			
			curRightPointStructs++;
		}
	}

	public void IncrementLeftScore()
	{
		leftScore++;
	}

	public void IncrementRightScore()
	{
		rightScore++;
	}

	public void PrintLeftScore()
	{
		print ("Left score: " + leftScore);
	}

	public void PrintRightScore()
	{
		print ("Right score: " + rightScore);
	}
}
