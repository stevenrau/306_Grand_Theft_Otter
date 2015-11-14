using UnityEngine;
using System.Collections;

public class player_state : MonoBehaviour {

    // Is the player out of the water? should be set when
    bool isOnLand;
    bool canBreath;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setisOnLand(bool newValue)
    {
        this.isOnLand = newValue;
    }

    public bool getisOnLand()
    {
        return this.isOnLand;
    }
}
