using UnityEngine;
using System.Collections;

public class collision_detection : MonoBehaviour {

	GameObject pearlOffset; //the child object of the player that will indicate the direction the pearl will be thrown
	SpriteRenderer pearlRenderer; //the component that will either show or hide the pearl on the beaver.

	private bool hasPearl = false;


	// Use this for initialization
	void Start() {
		//get references to the child objects
		pearlOffset = transform.GetChild (0).gameObject;

		//get reference to the sprite renderer located on the pearlOffset child object
		pearlRenderer = pearlOffset.GetComponent<SpriteRenderer>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Pearl") {
			
			//  GameObject pearlOffset = player.transform.GetChild(0).gameObject;
			//pearlOffset.GetComponent<SpriteRenderer>().enabled = true;
			pearlRenderer.enabled = true;
			pearlOffset.GetComponent<SpriteRenderer>().enabled = true;
			
			Destroy(other.gameObject);
			
			SetHasPearl(true);
		}
	}

	public void SetHasPearl(bool pearl)
	{
		hasPearl = pearl;
	}
	
	public bool GetHasPearl()
	{
		return hasPearl;
	}
	
	public void HidePearl()
	{
		//transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		pearlRenderer.enabled = false;
	}

}
