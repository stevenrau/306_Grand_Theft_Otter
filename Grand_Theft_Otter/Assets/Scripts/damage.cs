using UnityEngine;
using System.Collections;

public class damage : MonoBehaviour {

	player_state playerStateScript;
	moving movingScript;
	throwing throwingScript;

	player_state enemyStateScript;

	float dropPearlForce= 400f;

	Animator animator;
	sound_player soundPlayer;
	public AudioClip hitSound;

	// Use this for initialization
	void Start () {
		//dashInputScript = GetComponent<get_input>();
		playerStateScript = GetComponent<player_state>();
		movingScript = GetComponent<moving>();
		throwingScript = GetComponent<throwing>();

		animator = transform.GetChild(1).gameObject.GetComponent<Animator> ();

		soundPlayer = GameObject.FindGameObjectWithTag ("Sound_Player").GetComponent<sound_player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Damage()
	{
		playerStateScript.SetIsHit(true);
		soundPlayer.PlayClip(hitSound, 0.8f);
		movingScript.enabled = false; // disables movement, dashing, throwing
		throwingScript.enabled = false;
		animator.SetTrigger ("breathing_in"); // sets animator trigger so that suffocation animation is played
		
		//throw the pearl in the direction of enemy dash
		throwingScript.ThrowPearl(playerStateScript.GetAimingAngle(), dropPearlForce);
		
		//become able to move again
		Invoke("RestartState", 1.5f);
		
		//player flashes red
		//playerHit.GetComponent<Animation>().Play("Player_RedFlash");
	}
	
	void RestartState()
	{
		playerStateScript.SetIsHit(false);
		
		movingScript.enabled = true; // disables movement, dashing, throwing
		throwingScript.enabled = true;
		
		animator.SetTrigger("revived");
	} 
}
