using UnityEngine;
using System.Collections;

public class pearl_scored_beahviour : MonoBehaviour {

	private bool respawn = false;
	Animator animator; //the animator for the pearl

	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		animator.SetTrigger ("score");
		
		Invoke ("DeletePearlScored", 2.0f);	
	}

	void DeletePearlScored()
	{
		Destroy(gameObject);
	}
}
