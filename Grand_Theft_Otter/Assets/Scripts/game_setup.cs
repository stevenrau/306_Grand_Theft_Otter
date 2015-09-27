using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class game_setup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void Awake()
	{
		Debug.Log (Screen.width);
		Debug.Log (Screen.height);

		/***************************************************************************************
		 * Load the seaweed barriers
		 * ************************************************************************************/
		GameObject seaweed_left = Instantiate(Resources.Load("Seaweed")) as GameObject;
		GameObject seaweed_right = Instantiate(Resources.Load("Seaweed")) as GameObject;
		//seaweed_left.transform.Translate(new Vector2 (-0.2f, -0.1f));
		Transform left_transform = seaweed_left.transform;
		seaweed_right.transform.position = new Vector3(Mathf.Abs (left_transform.position.x), left_transform.position.y, left_transform.position.z);
		seaweed_right.transform.localScale = new Vector2(-seaweed_left.transform.localScale.x, seaweed_left.transform.localScale.y);


		/***************************************************************************************
		 * Load the otter dens
		 * ************************************************************************************/

		GameObject otter_den_left = Instantiate(Resources.Load("Den")) as GameObject;
		GameObject otter_den_right = Instantiate(Resources.Load("Den")) as GameObject;
		left_transform = otter_den_left.transform;
		otter_den_right.transform.position = new Vector3(Mathf.Abs (left_transform.position.x), left_transform.position.y, left_transform.position.z);
		otter_den_right.transform.localScale = new Vector2(-otter_den_left.transform.localScale.x, otter_den_left.transform.localScale.y);

		/***************************************************************************************
		 * Load the floor
		 * ************************************************************************************/
		GameObject floor = Instantiate (Resources.Load ("Floor")) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
