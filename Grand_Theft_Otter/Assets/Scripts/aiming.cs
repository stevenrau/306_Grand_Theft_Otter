using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	GameObject inputScript;

	//the child object of the player that will indicate the direction the pearl will be thrown
	GameObject pearlOffset;
	//the component that will either show or hide the pearl on the beaver.
	public SpriteRenderer pearlRenderer;

	float facingAngle; //the angle the beaver is looking( what way its head is pointing)
	float throwAngle; // the angle the pearl will be thrown


	// Use this for initialization
	void Start () {

		inputScript = transform.GetComponent<get_input> ();

		//get references to the child pearl
		pearlOffset = transform.GetChild(0).gameObject;

		//get reference to the sprite renderer located on the pearlOffset child object
		pearlRenderer = pearlOffset.GetComponent<SpriteRenderer>();
		
		throwAngle = 0.0f;
		facingAngle = 0.0f;
	}

	// Update is called once per frame
	void Update() {
		//rotatePearlOffset (Input.GetAxis (aim_horz), Input.GetAxis (aim_vert));
		rotatePearlOffset (Input.GetAxis (inputScript.GetAimHorizontal()), Input.GetAxis (GetAimVertical()));
	}

	//point the pearl based on right analog stick
	void rotatePearlOffset(float x, float y)
	{
		
		
		// cancel all input below this 
		if (Mathf.Abs(x) < R_analog_threshold) { x = 0.0f; }
		if (Mathf.Abs(y) < R_analog_threshold) { y = 0.0f; }
		
		// CALCULATE ANGLE AND ROTATE
		if (x != 0.0f || y != 0.0f)
		{
			if(inputScript.IsPC()) {
				throwAngle = ((Mathf.Atan2(y, x) * Mathf.Rad2Deg) *-1 ) - 180;	// for PC
			} else {
				throwAngle = ((Mathf.Atan2(y, x) * Mathf.Rad2Deg)-90);			// for MAC
			}
			pearlOffset.transform.rotation = Quaternion.AngleAxis(throwAngle, Vector3.forward);
		}
	}
}
