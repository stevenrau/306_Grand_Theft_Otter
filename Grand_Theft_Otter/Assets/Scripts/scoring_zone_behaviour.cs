using UnityEngine;
using System.Collections;

public class scoring_zone_behaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Pearl") {
			pearl_behaviour pearl_script = other.gameObject.GetComponent<pearl_behaviour>();

			pearl_script.score_and_animate();
		}
	}
}
