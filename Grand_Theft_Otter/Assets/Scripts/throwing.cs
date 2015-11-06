using UnityEngine;
using System.Collections;

public class throwing : MonoBehaviour {

    //the component that will either show or hide the pearl on the beaver.
    public SpriteRenderer pearlRenderer;

	GameObject pearlOffset; //the child object of the player that will indicate the direction the pearl will be thrown

   // private bool has_pearl = false;

  //  private string throw_bumper;

    //getting scripts
    get_input throwInputScript;
    aiming aimingDirScript;
	collision_detection colDetectScript;

 //  float throw_angle; // the angle the pearl will be thrown
    public float throw_force;

    void Start () {

		//get references to the child object
		pearlOffset = transform.GetChild(0).gameObject;

		//get reference to the sprite renderer located on the pearlOffset child object
		pearlRenderer = pearlOffset.GetComponent<SpriteRenderer>();

        throwInputScript = gameObject.GetComponent<get_input>();
        aimingDirScript = gameObject.GetComponent<aiming>();
		colDetectScript = transform.GetComponent<collision_detection> ();

	}
	
	// Update is called once per frame
	void Update () {

        //check for throw
		if(throwInputScript.GetThrowingButton())
        {
            //print(throw_bumper);
            throwPearl(); //will throw in the direction the pearl is currently pointing
        }
    }

    // disables the rendering of the offset pearl, generates a new pearl, and adds a force to the new pearl
    void throwPearl()
    {

        //if the beaver is currently holding a pearl
        if (pearlRenderer.enabled)
        {
			colDetectScript.SetHasPearl(false);

            //remove it from his grasp visually
			colDetectScript.HidePearl();

            // thrower doesn't interatct with pearl for given time
            transform.FindChild("beaver_pearl_trigger").gameObject.layer = LayerMask.NameToLayer("Non_Interactable");

            Invoke("MakeInteractable", 0.5f);

            //throw the pearl in the right direction
            GameObject thrownPearl = Instantiate(Resources.Load("Pearl")) as GameObject;

            //remember which beaver threw this pearl
            thrownPearl.GetComponent<pearl_behaviour>().setBeaver(this.transform.gameObject);

            //the pearl starts on the player
            thrownPearl.transform.position = new Vector2(transform.position.x, transform.position.y);

            //find the angle to throw based on the angle found for the pearl_offest
			Vector3 dir = Quaternion.AngleAxis(aimingDirScript.GetThrowAngle(), Vector3.forward) * Vector3.up;

            //apply force to the pearl in that direction
            thrownPearl.GetComponent<Rigidbody2D>().AddForce(dir * throw_force);
        }
        

    }

	void MakeInteractable() {
		transform.FindChild("beaver_pearl_trigger").gameObject.layer = LayerMask.NameToLayer("Interactable");
	}
}
