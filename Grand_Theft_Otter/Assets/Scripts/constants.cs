using UnityEngine;
using System.Collections;

//This script a place to put constants that may be needed by more than one script,
// so the value does not need to be updated in each script, only here.

// variables should be public and static to be referenced elsewhere

public class constants : MonoBehaviour {

    //counter for rolling through the instructions scenes
    public static int sceneCounter = 5; 

    //user game choices
    public static bool fourPlayers;
    public static bool hasTech;

    // beaver colors
    public static Color team1Color = new Color(0.6f,0.85f,0.6f,1f); // a little greenish;
	public static Color team2Color = new Color(0.85f,0.6f,0.6f,1f); //a little redish;

	public static Color player1Color = new Color(0.85f,1.0f,0.85f,1f); // a light green helmet;
	public static Color player2Color = new Color(1.0f,0.7f,0.7f,1f); // a light red helmet;
	public static Color player3Color = new Color(0.0f,0.7f,0.2f,1f); // a green helmet;
	public static Color player4Color = new Color(1.0f,0.2f,0.2f,1f); // a red helmet;

//	public static Color player1Color = new Color(1f,0f,0f,1f); // a little redish;
//	public static Color player2Color = new Color(0f,1f,0f,1f); // a little greenish;
//	public static Color player3Color = new Color(0f,0f,1f,1f); // a little blueish;
//	public static Color player4Color = new Color(1f,0.92f,0.016f,1f); // a little ?????;


	public static Color team1ColorWater = new Color(0.3f,0.45f,0.9f,1f); //darker green with blue for underwater (0.3f,0.5f,0.9f,1f)
	public static Color team2ColorWater= new Color(0.45f,0.3f,0.9f,1f); // darker red with blue for undewater (0.5f,0.3f,0.9f,1f)

	//the level boundaries
	public static float leftBoundary = -8.88f;
	public static float rightBoundary = 8.88f;
	public static float bottomBoundary = -4.8f;

	// this is the visual surface of the water
	public static float waterSurface = 3.3f;

	//allow player to breath when they are just slightly lower than the surface (above this y coordinate)
	public static float breathingSurface = 3.1f; 

	//gravity values
	public static float pearlWaterGravity = 0.2f;
	public static float pearlAirGravity = 3.0f;

	//different player moving speeds
	public static float moveForceNormal = 100.0f ;
	public static float moveForceSlow = 15.0f;

	//gravities in different locations
	public static float waterGravity = -0.1f; // gravity when in water
	public static float landGravity = 1f; //gravity when out of water and touching a platform
	public static float airGravity = 3f; // gravity when out of water, but not touching a platform


}
