using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	GameObject pearl_offset; //the child object of the player that will indicate the direction the pearl will be thrown
	public SpriteRenderer pearl_renderer; //the component that will either show or hide the pearl on the beaver.

	private bool has_pearl = false;


	// Use this for initialization
	void Start() {
		//get references to the child objects
		pearl_offset = transform.GetChild (0).gameObject;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		print (other.tag);
		if (other.tag == "Pearl") {
			
			//  GameObject pearlOffset = player.transform.GetChild(0).gameObject;
			//pearlOffset.GetComponent<SpriteRenderer>().enabled = true;
			pearl_renderer.enabled = true;
			pearl_offset.GetComponent<SpriteRenderer>().enabled = true;
			
			Destroy(other.gameObject);
			
			set_has_pearl(true);
		}
	}

	public void set_has_pearl(bool pearl)
	{
		has_pearl = pearl;
	}
	
	public bool get_has_pearl()
	{
		return has_pearl;
	}
	
	public void hide_pearl()
	{
		//transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		pearl_renderer.enabled = false;
	}

	void MakeInteractable() {
		transform.FindChild("beaver_pearl_trigger").gameObject.layer = LayerMask.NameToLayer("Interactable");
	}
}
