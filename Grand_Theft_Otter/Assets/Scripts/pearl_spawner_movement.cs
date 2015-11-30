using UnityEngine;
using System.Collections;

public class pearl_spawner_movement : MonoBehaviour {

	Rigidbody2D rBody;
	private float moveForce;

	private float rightLimit = 6.0f;
	private float leftLimit = -6.0f;
	private float bottomLimit = -3.0f;
	private float topLimit = 2.0f;


	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody2D> ();

		moveForce = 200.0f;

		InvokeRepeating ("ApplyForce", 5, 3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ApplyForce(){

		Vector2 newForce;

		//move back into valid area
		if (transform.position.x >= rightLimit) {
			newForce = new Vector2(-moveForce, 0);
		} 
		else if (transform.position.x <= leftLimit) {
			newForce = new Vector2(moveForce, 0);
		} 
		else if (transform.position.y >= topLimit) {
			newForce = new Vector2(0, -moveForce);
		} 
		else if (transform.position.y <= bottomLimit) {
			newForce = new Vector2(0, moveForce);
		} 
		else {
			//otherwise move randomly
			newForce = new Vector2 (Random.Range(-1.0f, 1.0f) * moveForce, Random.Range(-1.0f, 1.0f) * moveForce);
		}
		rBody.AddForce (newForce);

	}
}
