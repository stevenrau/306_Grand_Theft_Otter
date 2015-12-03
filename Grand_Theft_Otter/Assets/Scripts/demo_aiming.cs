using UnityEngine;
using System.Collections;

//copied parts of aiming script for demo purposes
// just shows the aiming guide in a circle
// this script should only be present on one game object in the instruction pages
public class demo_aiming : MonoBehaviour {


	//the child object of the player that will indicate the direction the pearl will be thrown
	GameObject pearlOffset;
	
	//the component that will either show or hide the pearl on the beaver.
	SpriteRenderer pearlRenderer;
	
	//the animator that shows the aiming guide or not
	Animator anim;
	
	float throwAngle; // the angle the pearl will be thrown


	// Use this for initialization
	void Start () {
	
		pearlOffset = transform.GetChild(0).gameObject;
		
		pearlRenderer = pearlOffset.GetComponent<SpriteRenderer>();
		anim = pearlOffset.GetComponent<Animator> ();

		anim.SetBool ("Aiming", true);

		pearlRenderer.enabled = true;

		throwAngle = 0.0f;

		InvokeRepeating ("AutoRotate", 0f, 0.01f);
	}
	

	void AutoRotate(){
		DemoRotatePearlOffset ();
		throwAngle ++;
	}

	//point the pearl based on right analog stick
	void DemoRotatePearlOffset()
	{
			//point the aiming guide in the direction 
			pearlOffset.transform.rotation = Quaternion.AngleAxis (throwAngle, Vector3.forward);

	}
}
