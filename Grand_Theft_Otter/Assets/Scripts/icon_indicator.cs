using UnityEngine;
using System.Collections;

public class icon_indicator : MonoBehaviour {

	SpriteRenderer beaverIcon;
	Animator iconAnim;
	GameObject player;
	string breatheIn;
	string breatheOut;

	player_state playerStateScript;


	// Use this for initialization
	void Start () {
		beaverIcon = GetComponent<SpriteRenderer> ();
		iconAnim = GetComponent<Animator>();

		if (gameObject.name == "breath_indicator1") {
			playerStateScript = GameObject.Find ("Beaver1").GetComponent<player_state> ();
			player = GameObject.Find ("Beaver1").gameObject;
			breatheIn = "i";
			breatheOut= "o";
		} else {
			playerStateScript = GameObject.Find ("Beaver2").GetComponent<player_state> ();
			player = GameObject.Find ("Beaver2").gameObject;
			breatheIn = "k";
			breatheOut= "l";
		}

		if (!constants.hasTech) {
			beaverIcon.enabled = false;
			transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
		}	
	}
	
	// Update is called once per frame
	void Update () {

//		iconAnim.ResetTrigger ("breathing_in");
//		iconAnim.ResetTrigger ("breathing_out");
//		iconAnim.ResetTrigger ("breath_held");

		// above water
		if (playerStateScript.GetCanBreathe ()) {
			iconAnim.SetBool ("isDead", false);
			iconAnim.SetBool ("underwater", false);
		} else {
			iconAnim.SetBool ("underwater", true);
		}

		// underwater and is dead
		if (!playerStateScript.GetCanBreathe () && playerStateScript.GetIsSuffocating ()) {
			iconAnim.SetTrigger("is_dead");
			iconAnim.SetBool("isDead", true);
		}

		if(!playerStateScript.GetCanBreathe() ){
			iconAnim.SetTrigger("breath_held");
		}

		// underwater
		if (!playerStateScript.GetIsSuffocating ()) {

			if (Input.GetKey (breatheIn)) {
				iconAnim.SetTrigger ("breathing_in");
			}
			
			if (Input.GetKey (breatheOut)) {
				iconAnim.SetTrigger ("breathing_out");
			}
		}

	}
}
