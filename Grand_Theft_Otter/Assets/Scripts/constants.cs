using UnityEngine;
using System.Collections;

//This script a place to put constants that may be needed by more than one script,
// so the value does not need to be updated in each script, only here.

// variables should be public and static to be referenced elsewhere

public class constants : MonoBehaviour {

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
