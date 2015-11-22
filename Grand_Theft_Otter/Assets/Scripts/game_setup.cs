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
		 * Load the Sound Player
		 * ************************************************************************************/
		GameObject soundPlayer = Instantiate (Resources.Load ("Sound_Player")) as GameObject;

		/***************************************************************************************
		 * Load the Water Background
		 * ************************************************************************************/
		GameObject waterBg = Instantiate (Resources.Load ("Water_BG")) as GameObject;

		/***************************************************************************************
		 * Load the Sky Background
		 * ************************************************************************************/
		GameObject sky = Instantiate (Resources.Load ("Night_Sky")) as GameObject;

		/***************************************************************************************
		 * Load the Breathable area
		 * ************************************************************************************/
		GameObject breathableArea = Instantiate (Resources.Load ("Breathable_area")) as GameObject;

		/***************************************************************************************
		 * Load the pearl spawn point
		 * ************************************************************************************/
		GameObject pearlSpawner = Instantiate (Resources.Load ("Pearl_Spawn")) as GameObject;
		pearlSpawner.name = "Pearl_Spawn";


		/***************************************************************************************
		 * Load the walls
		 * ************************************************************************************/
		/*GameObject seaweedLeft = Instantiate(Resources.Load("Seaweed")) as GameObject;
		GameObject seaweedRight = Instantiate(Resources.Load("Seaweed")) as GameObject;
        
		//seaweed_left.transform.Translate(new Vector2 (-0.2f, -0.1f));
		Transform left_transform = seaweedLeft.transform;
		seaweedRight.transform.position = new Vector3(Mathf.Abs (left_transform.position.x), left_transform.position.y, left_transform.position.z);
		seaweedRight.transform.localScale = new Vector2(-seaweedLeft.transform.localScale.x, seaweedLeft.transform.localScale.y);*/

		GameObject wallLeft = Instantiate(Resources.Load("Wall_Purple_Rock")) as GameObject;
		GameObject wallRight = Instantiate(Resources.Load("Wall_Purple_Rock")) as GameObject;
		
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
		 * Load the dam ramps
		 * ************************************************************************************/
		GameObject damRampLeft = Instantiate(Resources.Load("Dam_Ramp")) as GameObject;
		GameObject damRampRight = Instantiate(Resources.Load("Dam_Ramp")) as GameObject;
		damRampLeft.name = "Dam_Ramp_Left";
		damRampRight.name = "Dam_Ramp_Right";

		leftTransform = damRampLeft.transform;
		damRampRight.transform.position = new Vector3(Mathf.Abs (leftTransform.position.x), leftTransform.position.y, leftTransform.position.z);
		damRampRight.transform.localScale = new Vector2(-leftTransform.localScale.x, leftTransform.localScale.y);

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
		 * Load the clams
		 * ************************************************************************************/

		//Load the five left clams
		GameObject clamLeft1 = Instantiate (Resources.Load ("Clam")) as GameObject;
		clamLeft1.name = "clam_left_1";

		Transform clamTransform = clamLeft1.transform;
		GameObject clamLeft2 = Instantiate (Resources.Load ("Clam")) as GameObject;
		clamLeft2.name = "clam_left_2";
		clamLeft2.transform.position = new Vector3(clamTransform.position.x, clamTransform.position.y - 1.25f, clamTransform.position.x);
		GameObject clamLeft3 = Instantiate (Resources.Load ("Clam")) as GameObject;
		clamLeft3.name = "clam_left_3";
		clamLeft3.transform.position = new Vector3(clamTransform.position.x, clamTransform.position.y - 2.5f, clamTransform.position.x);
		GameObject clamLeft4 = Instantiate (Resources.Load ("Clam")) as GameObject;
		clamLeft4.name = "clam_left_4";
		clamLeft4.transform.position = new Vector3(clamTransform.position.x, clamTransform.position.y - 3.75f, clamTransform.position.x);
		GameObject clamLeft5 = Instantiate (Resources.Load ("Clam")) as GameObject;
		clamLeft5.name = "clam_left_5";
		clamLeft5.transform.position = new Vector3(clamTransform.position.x, clamTransform.position.y - 5f, clamTransform.position.x);


		//Load the five right clams, using the first left clam as the initial reference
		GameObject clamRight1 = Instantiate (Resources.Load ("Clam")) as GameObject;
		clamRight1.name = "clam_left_1";
		clamRight1.transform.position = new Vector3(-clamTransform.position.x, clamTransform.position.y, clamTransform.position.x);
		clamRight1.transform.localScale = new Vector2(-clamTransform.localScale.x, clamTransform.localScale.y);
		
		clamTransform = clamRight1.transform;
		GameObject clamRight2 = Instantiate (Resources.Load ("Clam")) as GameObject;
		clamRight2.name = "clam_left_2";
		clamRight2.transform.position = new Vector3(clamTransform.position.x, clamTransform.position.y - 1.25f, clamTransform.position.x);
		clamRight2.transform.localScale = new Vector2(clamTransform.localScale.x, clamTransform.localScale.y);
		GameObject clamRight3 = Instantiate (Resources.Load ("Clam")) as GameObject;
		clamRight3.name = "clam_left_3";
		clamRight3.transform.position = new Vector3(clamTransform.position.x, clamTransform.position.y - 2.5f, clamTransform.position.x);
		clamRight3.transform.localScale = new Vector2(clamTransform.localScale.x, clamTransform.localScale.y);
		GameObject clamRight4 = Instantiate (Resources.Load ("Clam")) as GameObject;
		clamRight4.name = "clam_left_4";
		clamRight4.transform.position = new Vector3(clamTransform.position.x, clamTransform.position.y - 3.75f, clamTransform.position.x);
		clamRight4.transform.localScale = new Vector2(clamTransform.localScale.x, clamTransform.localScale.y);
		GameObject clamRight5 = Instantiate (Resources.Load ("Clam")) as GameObject;
		clamRight5.name = "clam_left_5";
		clamRight5.transform.position = new Vector3(clamTransform.position.x, clamTransform.position.y - 5f, clamTransform.position.x);
		clamRight5.transform.localScale = new Vector2(clamTransform.localScale.x, clamTransform.localScale.y);

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
		GameObject startObject1 = Instantiate (Resources.Load ("Player_start_1")) as GameObject;
		GameObject startObject2 = Instantiate (Resources.Load ("Player_start_2")) as GameObject;
		GameObject startObject3 = Instantiate (Resources.Load ("Player_start_3")) as GameObject;
		GameObject startObject4 = Instantiate (Resources.Load ("Player_start_4")) as GameObject;

		Vector2 startPos1 = startObject1.transform.position;
		Vector2 startPos2 = startObject2.transform.position;
		Vector2 startPos3 = startObject3.transform.position;
		Vector2 startPos4 = startObject4.transform.position;

		Color team1Color = new Color(0.6f,0.8f,0.6f,1f); //a little greenish
		Color team2Color = new Color(0.8f,0.55f,0.55f,1f); //a little redish

		GameObject beaver1 = Instantiate (Resources.Load ("Beaver_Player")) as GameObject;
        beaver1.name = "Beaver1";
		beaver1.GetComponent<get_input>().SetPlayerID ("1");
		beaver1.transform.position = new Vector2 (startPos1.x, startPos1.y);
		beaver1.transform.GetChild (1).gameObject.GetComponent<SpriteRenderer> ().color = team1Color;


		GameObject beaver2 = Instantiate (Resources.Load ("Beaver_Player")) as GameObject;
		beaver2.name = "Beaver2";
		beaver2.GetComponent<get_input>().SetPlayerID ("2");
		beaver2.transform.position = new Vector2 (startPos2.x, startPos2.y);
		beaver2.transform.GetChild (1).gameObject.GetComponent<SpriteRenderer> ().color = team2Color;
		
		/*
		//TODO change the prefab type of these other players
		//player 3
		GameObject beaver3 = Instantiate (Resources.Load ("Beaver_Player")) as GameObject; 
		beaver3.name = "Beaver3";
		beaver3.GetComponent<get_input>().SetPlayerID ("3");
		beaver3.transform.position = new Vector2 (startPos3.x, startPos3.y);
		beaver3.transform.GetChild (1).gameObject.GetComponent<SpriteRenderer> ().color = team1Color;

		//player 4
		GameObject beaver4 = Instantiate (Resources.Load ("Beaver_Player")) as GameObject;
		beaver4.name = "Beaver4";
		beaver4.GetComponent<get_input>().SetPlayerID ("4");
		beaver4.transform.position = new Vector2 (startPos4.x, startPos4.y);
		beaver4.transform.GetChild (1).gameObject.GetComponent<SpriteRenderer> ().color = team2Color;
		*/

    }
	
	// Update is called once per frame
	void Update () {
	}



}
