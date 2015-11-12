using UnityEngine;
using System.Collections;

public class aiming : MonoBehaviour {

	//the child object of the player that will indicate the direction the pearl will be thrown
	GameObject pearlOffset;
	//the component that will either show or hide the pearl on the beaver.
	SpriteRenderer pearlRenderer;

	float throwAngle; // the angle the pearl will be thrown

	float aimingThreshold;

	// script
	get_input inputScript;
	


	// Use this for initialization
	void Start () {

		inputScript = gameObject.GetComponent<get_input>();

		//get references to the child pearl
		pearlOffset = transform.GetChild(0).gameObject;

		//get reference to the sprite renderer located on the pearlOffset child object
		pearlRenderer = pearlOffset.GetComponent<SpriteRenderer>();
		
		throwAngle = 0.0f;

		aimingThreshold = 0.20f;
	}

	// Update is called once per frame
	void Update() {
		RotatePearlOffset (inputScript.GetAimHorizontalAxis(), inputScript.GetAimVerticalAxis());
	}

	//point the pearl based on right analog stick
	void RotatePearlOffset(float x, float y)
	{
		// cancel all input below this 
		if (Mathf.Abs(x) < aimingThreshold) { x = 0.0f; }
		if (Mathf.Abs(y) < aimingThreshold) { y = 0.0f; }
		
		// CALCULATE ANGLE AND ROTATE
		if (x != 0.0f || y != 0.0f)
		{
			if(inputScript.PlatformIsPC()) {
				throwAngle = ((Mathf.Atan2(y, x) * Mathf.Rad2Deg) *-1 ) - 180;	// for PC
			} else {
				throwAngle = ((Mathf.Atan2(y, x) * Mathf.Rad2Deg)-90);			// for MAC
			}
			pearlOffset.transform.rotation = Quaternion.AngleAxis(throwAngle, Vector3.forward);
		}
	}

	public float GetThrowAngle() {
		return throwAngle;
	}
}
