using UnityEngine;
using System.Collections;

public class demo_throwing : MonoBehaviour {

	throwing throwScript;
	collision_detection colDetectScript;

	//the component that will either show or hide the pearl on the beaver.
	SpriteRenderer pearlRenderer;
	
	GameObject pearlOffset; //the child object of the player that will indicate the direction the pearl will be thrown
	GameObject beaverSprite;

	// Use this for initialization
	void Start () {
		throwScript = GetComponent<throwing> ();
		pearlOffset = transform.GetChild(0).gameObject;
		beaverSprite = transform.GetChild (1).gameObject;
		pearlRenderer = pearlOffset.GetComponent<SpriteRenderer>();
		colDetectScript = transform.GetComponent<collision_detection> ();

		InvokeRepeating ("DemoThrow", 0.05f, 2.0f);
	}

	void DemoThrow(){
		DemoThrowPearl (0.0f, 300.0f);
	}

	public void 
		DemoThrowPearl(float angle, float force)
	{
		
		//if the beaver is currently holding a pearl
		if (pearlRenderer.enabled)
		{
			//soundPlayer.PlayClip(throwSound, 1.0f);
			//playerStateScript.SetHasPearl(false);
			
			//remove it from his grasp visually
			colDetectScript.HidePearl();
			
			// thrower doesn't interatct with pearl for given time
			beaverSprite.transform.FindChild("beaver_pearl_trigger").gameObject.layer = LayerMask.NameToLayer("Non_Interactable");
			
			Invoke("MakeInteractable", 0.5f);
			
			//throw the pearl in the right direction
			GameObject thrownPearl = Instantiate(Resources.Load("Pearl")) as GameObject;
			
			//remember which beaver threw this pearl
			thrownPearl.GetComponent<pearl_behaviour>().SetBeaver(this.transform.gameObject);
			
			//the pearl starts on the player
			thrownPearl.transform.position = new Vector2(transform.position.x, transform.position.y);
			
			//find the angle to throw based on the angle found for the pearl_offest
			//Vector3 dir = Quaternion.AngleAxis(aimingDirScript.GetThrowAngle(), Vector3.forward) * Vector3.up;
			Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up;
			
			//apply force to the pearl in that direction
			thrownPearl.GetComponent<Rigidbody2D>().AddForce(dir * force);
		}
		
		
	}
	
	void MakeInteractable()
	{
		beaverSprite.transform.FindChild("beaver_pearl_trigger").gameObject.layer = LayerMask.NameToLayer("Interactable");
	}
}
