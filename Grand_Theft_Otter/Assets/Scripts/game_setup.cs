using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class game_setup : MonoBehaviour {



	// Use this for initialization
	void Start () {
	}

	void Awake()
	{
		Debug.Log ("Screen Dimensions: "+ Screen.width + " x " + Screen.height);

        /***************************************************************************************
		 * Load the Pause Menu
		 * ************************************************************************************/
        GameObject pauseMenu = Instantiate(Resources.Load("pause_menu")) as GameObject;
        pauseMenu.name = "pause_menu";
        //pauseMenu.SetActive(false); 

        /***************************************************************************************
		 * Load the Boat
		 * ************************************************************************************/
        GameObject boat = Instantiate(Resources.Load("Boat")) as GameObject;
        boat.name = "Boat";

        /***************************************************************************************
		 * Load the Score Keeper
		 * ************************************************************************************/
        GameObject scoreKeeper = Instantiate (Resources.Load ("Score_Keeper")) as GameObject;
		scoreKeeper.name = "Score_Keeper";
		
		/***************************************************************************************
		 * Load the Sound Player
		 * ************************************************************************************/
		GameObject soundPlayer = Instantiate (Resources.Load ("Sound_Player")) as GameObject;
		soundPlayer.name = "Sound_Player";

		/***************************************************************************************
		 * Load the Water Background
		 * ************************************************************************************/
		GameObject waterBg = Instantiate (Resources.Load ("Water_BG")) as GameObject;

		/***************************************************************************************
		 * Load the Sky Background
		 * ************************************************************************************/
		GameObject sky = Instantiate (Resources.Load ("Day_Sky_Clear")) as GameObject;

		/***************************************************************************************
		 * Load the Clouds
		 * ************************************************************************************/
		Instantiate (Resources.Load ("Cloud_1"));
		Instantiate (Resources.Load ("Cloud_2"));
		Instantiate (Resources.Load ("Cloud_3"));
		Instantiate (Resources.Load ("Cloud_4"));
		Instantiate (Resources.Load ("Cloud_5"));
		Instantiate (Resources.Load ("Cloud_6"));
		Instantiate (Resources.Load ("Cloud_7"));
		

		/***************************************************************************************
		 * Load the pearl spawn point
		 * ************************************************************************************/
		GameObject pearlSpawner = Instantiate (Resources.Load ("Pearl_Spawn")) as GameObject;
		pearlSpawner.name = "Pearl_Spawn";
		GameObject pearlSpawner2 = Instantiate (Resources.Load ("Pearl_Spawn")) as GameObject;
		pearlSpawner2.name = "Pearl_Spawn";
		pearlSpawner2.transform.position = new Vector2 (pearlSpawner.transform.position.x, pearlSpawner.transform.position.y + 4.0f);


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
		 * Load the coloured flags
		 * ************************************************************************************/
		GameObject greenFlag = Instantiate(Resources.Load("Green_Flag")) as GameObject;
		GameObject redFlag = Instantiate(Resources.Load("Red_Flag")) as GameObject;

		/***************************************************************************************
		 * Load the scoring zones ( now in the clams)
		 * ************************************************************************************/
		/*
		GameObject scoringZoneLeft = Instantiate (Resources.Load ("Scoring_Zone")) as GameObject;
		scoringZoneLeft.name = "left_score_zone";
		GameObject scoringZoneRight = Instantiate (Resources.Load ("Scoring_Zone")) as GameObject;
		scoringZoneRight.name = "right_score_zone";
		leftTransform = scoringZoneLeft.transform;
		scoringZoneRight.transform.position = new Vector3(Mathf.Abs (leftTransform.position.x), leftTransform.position.y, leftTransform.position.z);
		scoringZoneRight.transform.localScale = new Vector2(-scoringZoneLeft.transform.localScale.x, scoringZoneLeft.transform.localScale.y);
		*/

		/***************************************************************************************
		 * Load the clams
		 * ************************************************************************************/

		//Load the five left clams
		GameObject clamLeft1 = Instantiate (Resources.Load ("Left_Clam")) as GameObject;
		clamLeft1.name = "clam_left_1";

		Transform clamTransform = clamLeft1.transform;
		GameObject clamLeft2 = Instantiate (Resources.Load ("Left_Clam")) as GameObject;
		clamLeft2.name = "clam_left_2";
		clamLeft2.transform.position = new Vector3(clamTransform.position.x, clamTransform.position.y - 1.25f, clamTransform.position.x);
		GameObject clamLeft3 = Instantiate (Resources.Load ("Left_Clam")) as GameObject;
		clamLeft3.name = "clam_left_3";
		clamLeft3.transform.position = new Vector3(clamTransform.position.x, clamTransform.position.y - 2.5f, clamTransform.position.x);
		GameObject clamLeft4 = Instantiate (Resources.Load ("Left_Clam")) as GameObject;
		clamLeft4.name = "clam_left_4";
		clamLeft4.transform.position = new Vector3(clamTransform.position.x, clamTransform.position.y - 3.75f, clamTransform.position.x);
		GameObject clamLeft5 = Instantiate (Resources.Load ("Left_Clam")) as GameObject;
		clamLeft5.name = "clam_left_5";
		clamLeft5.transform.position = new Vector3(clamTransform.position.x, clamTransform.position.y - 5f, clamTransform.position.x);


		//Load the five right clams, using the first left clam as the initial reference
		GameObject clamRight1 = Instantiate (Resources.Load ("Right_Clam")) as GameObject;
		clamRight1.name = "clam_right_1";
		
		clamTransform = clamRight1.transform;
		GameObject clamRight2 = Instantiate (Resources.Load ("Right_Clam")) as GameObject;
		clamRight2.name = "clam_right_2";
		clamRight2.transform.position = new Vector3(clamTransform.position.x, clamTransform.position.y - 1.25f, clamTransform.position.x);
		clamRight2.transform.localScale = new Vector2(clamTransform.localScale.x, clamTransform.localScale.y);
		GameObject clamRight3 = Instantiate (Resources.Load ("Right_Clam")) as GameObject;
		clamRight3.name = "clam_right_3";
		clamRight3.transform.position = new Vector3(clamTransform.position.x, clamTransform.position.y - 2.5f, clamTransform.position.x);
		clamRight3.transform.localScale = new Vector2(clamTransform.localScale.x, clamTransform.localScale.y);
		GameObject clamRight4 = Instantiate (Resources.Load ("Right_Clam")) as GameObject;
		clamRight4.name = "clam_right_4";
		clamRight4.transform.position = new Vector3(clamTransform.position.x, clamTransform.position.y - 3.75f, clamTransform.position.x);
		clamRight4.transform.localScale = new Vector2(clamTransform.localScale.x, clamTransform.localScale.y);
		GameObject clamRight5 = Instantiate (Resources.Load ("Right_Clam")) as GameObject;
		clamRight5.name = "clam_right_5";
		clamRight5.transform.position = new Vector3(clamTransform.position.x, clamTransform.position.y - 5f, clamTransform.position.x);
		clamRight5.transform.localScale = new Vector2(clamTransform.localScale.x, clamTransform.localScale.y);

		/***************************************************************************************
		 * Load the floor
		 * ************************************************************************************/
		GameObject floor = Instantiate (Resources.Load ("Floor")) as GameObject;

		/***************************************************************************************
		 * Load the obstacles
		 * ************************************************************************************/
		if (constants.fourPlayers) {
			GameObject obstacleBottomRight = Instantiate (Resources.Load ("Obstacle_Bottom_Right")) as GameObject;
			GameObject obstacleBottomLeft = Instantiate (Resources.Load ("Obstacle_Bottom_Left")) as GameObject;
		
			Vector3 posTR = obstacleBottomRight.transform.position;
			posTR.y += 1.5f;
			obstacleBottomRight.transform.position = posTR;
			Vector3 posTL = obstacleBottomLeft.transform.position;
			posTL.y += 1.5f;
			obstacleBottomLeft.transform.position = posTL;

			GameObject obstacleEllipse = Instantiate (Resources.Load ("Obstacle_Ellipse")) as GameObject;
//			GameObject obstacleCurve = Instantiate (Resources.Load ("Obstacle_Curve")) as GameObject;
		} else {
			GameObject obstacleTopRight = Instantiate (Resources.Load ("Obstacle_Top_Right")) as GameObject;
			GameObject obstacleTopLeft = Instantiate (Resources.Load ("Obstacle_Top_Left")) as GameObject;
			GameObject obstacleBottomRight = Instantiate (Resources.Load ("Obstacle_Bottom_Right")) as GameObject;
			GameObject obstacleBottomLeft = Instantiate (Resources.Load ("Obstacle_Bottom_Left")) as GameObject;
			GameObject obstacleEllipse = Instantiate (Resources.Load ("Obstacle_Ellipse")) as GameObject;
			GameObject obstacleCurve = Instantiate (Resources.Load ("Obstacle_Curve")) as GameObject;
		}

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

        //Color team1Color = new Color(0.6f,0.8f,0.6f,1f); //a little greenish
        //Color team2Color = new Color(0.8f,0.55f,0.55f,1f); //a little redish


		if (constants.fourPlayers) {
			if (constants.hasTech) {
				GameObject beaver1 = createBeaver ("1", startPos1, "Beaver_Player", constants.team1Color, constants.player1Color);
				GameObject beaver2 = createBeaver ("2", startPos2, "Beaver_Player", constants.team2Color, constants.player2Color);
				GameObject beaver3 = createBeaver ("3", startPos3, "Beaver_Player_Scuba", constants.team1Color, constants.player3Color);
				GameObject beaver4 = createBeaver ("4", startPos4, "Beaver_Player_Scuba", constants.team2Color, constants.player4Color);
			} else {
				GameObject beaver1 = createBeaver ("1", startPos1, "Beaver_Player_Scuba", constants.team1Color, constants.player1Color);
				GameObject beaver2 = createBeaver ("2", startPos2, "Beaver_Player_Scuba", constants.team2Color, constants.player2Color);
				GameObject beaver3 = createBeaver ("3", startPos3, "Beaver_Player_Scuba", constants.team1Color, constants.player3Color);
				GameObject beaver4 = createBeaver ("4", startPos4, "Beaver_Player_Scuba", constants.team2Color, constants.player4Color);
			}
		} else {
			if (constants.hasTech) {

				GameObject beaver1 = createBeaver ("1", startPos1, "Beaver_Player", constants.team1Color, constants.player1Color);
				GameObject beaver2 = createBeaver ("2", startPos2, "Beaver_Player", constants.team2Color, constants.player2Color);
			} else {
				GameObject beaver1 = createBeaver ("1", startPos1, "Beaver_Player_Scuba", constants.team1Color, constants.player1Color);
				GameObject beaver2 = createBeaver ("2", startPos2, "Beaver_Player_Scuba", constants.team2Color, constants.player2Color);
			}

		}

//        if (constants.fourPlayers) // 4 player game *with tech* !?
//        {
//
//            GameObject beaver1 = Instantiate(Resources.Load("Beaver_Player")) as GameObject;
//            beaver1.name = "Beaver1";
//            beaver1.GetComponent<get_input>().SetPlayerID("1");
//            beaver1.transform.position = new Vector2(startPos1.x, startPos1.y);
//            beaver1.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = constants.team1Color;
//
//
//            GameObject beaver2 = Instantiate(Resources.Load("Beaver_Player")) as GameObject;
//            beaver2.name = "Beaver2";
//            beaver2.GetComponent<get_input>().SetPlayerID("2");
//            beaver2.transform.position = new Vector2(startPos2.x, startPos2.y);
//            beaver2.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = constants.team2Color;
//
//
//            //player 3
//            GameObject beaver3 = Instantiate(Resources.Load("Beaver_Player")) as GameObject;
//            beaver3.name = "Beaver3";
//            beaver3.GetComponent<get_input>().SetPlayerID("1"); //should be 3
//            beaver3.transform.position = new Vector2(startPos3.x, startPos3.y);
//            beaver3.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = constants.team1Color;
//
//            //player 4
//            GameObject beaver4 = Instantiate(Resources.Load("Beaver_Player")) as GameObject;
//            beaver4.name = "Beaver4";
//            beaver4.GetComponent<get_input>().SetPlayerID("2"); //should be 4
//            beaver4.transform.position = new Vector2(startPos4.x, startPos4.y);
//            beaver4.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = constants.team2Color;
//        }
//        else //2 player *with tech* !?
//        {
//            GameObject beaver1 = Instantiate(Resources.Load("Beaver_Player")) as GameObject;
//            beaver1.name = "Beaver1";
//            beaver1.GetComponent<get_input>().SetPlayerID("1");
//            beaver1.transform.position = new Vector2(startPos1.x, startPos1.y);
//            beaver1.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = constants.team1Color;
//
//
//            GameObject beaver2 = Instantiate(Resources.Load("Beaver_Player")) as GameObject;
//            beaver2.name = "Beaver2";
//            beaver2.GetComponent<get_input>().SetPlayerID("2");
//            beaver2.transform.position = new Vector2(startPos2.x, startPos2.y);
//            beaver2.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = constants.team2Color;
//        }
    }

	GameObject createBeaver(string playerID, Vector2 startPos, string type, Color teamColor, Color playerColor) {
		GameObject newBeaver = Instantiate (Resources.Load (type)) as GameObject;
		string name = "Beaver" + playerID;
		newBeaver.name = name;
		//newBeaver.GetComponent<get_input> ().SetPlayerID (playerID);
		newBeaver.GetComponent<player_state> ().SetPlayerID (playerID);

		newBeaver.transform.position = new Vector2 (startPos.x, startPos.y);
		newBeaver.transform.GetChild (1).gameObject.GetComponent<SpriteRenderer> ().color = teamColor;
		newBeaver.transform.GetChild(1).GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = playerColor;
		return newBeaver;
	}

	// Update is called once per frame
	void Update () {
	}



}
