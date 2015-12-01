using UnityEngine;
using System.Collections;

//This script a place to put constants that may be needed by more than one script,
// so the value does not need to be updated in each script, only here.

// variables should be public and static to be referenced elsewhere

public class constants : MonoBehaviour {

    //user game choices
    public static bool fourPlayers;
    public static bool hasTech;

    // beaver colors
    public static Color team1Color = new Color(0.6f,0.8f,0.6f,1f); // a little greenish;
	public static Color team2Color = new Color(0.8f,0.6f,0.6f,1f); //a little redish;

	public static Color player1Color = new Color(0.8f,0.4f,0.4f,1f); // a little redish;
	public static Color player2Color = new Color(0.4f,0.8f,0.4f,1f); // a little greenish;
	public static Color player3Color = new Color(0.1f,0.4f,0.9f,1f); // a little blueish;
	public static Color player4Color = new Color(1f,0.8f,0.3f,1f); // a little ?????;
//	public static Color player1Color = new Color(1f,0f,0f,1f); // a little redish;
//	public static Color player2Color = new Color(0f,1f,0f,1f); // a little greenish;
//	public static Color player3Color = new Color(0f,0f,1f,1f); // a little blueish;
//	public static Color player4Color = new Color(1f,0.92f,0.016f,1f); // a little ?????;


	public static Color team1ColorWater = new Color(0.3f,0.5f,1.0f,1f);
	public static Color team2ColorWater= new Color(0.5f,0.3f,1.0f,1f);

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




}
