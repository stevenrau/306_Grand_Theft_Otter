using UnityEngine;
using System.Collections;

//This script a place to put constants that may be needed by more than one script,
// so the value does not need to be updated in each script, only here.

// variables should be public and static to be referenced elsewhere

public class constants : MonoBehaviour {

	//allow player to breath when they are just slightly lower than the surface (above this y coordinate)
	public static float breathingSurface = 3.1f; 


}
