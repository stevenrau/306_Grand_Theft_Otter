﻿using UnityEngine;
using System.Collections;

public class player_state : MonoBehaviour {

    // Is the player out of the water? should be set when
    bool isOnLand;

	// Is the player touching the air?
    bool canBreathe;


	// Use this for initialization
	void Start () {
	
		isOnLand = true;
		canBreathe = true;

	}
	
	// Update is called once per frame
	void Update () {


	}


	public void SetCanBreathe(bool newValue)
	{
		this.canBreathe = newValue;
	}

	public bool GetCanBreathe() {
			return this.canBreathe;
	}


    public void SetIsOnLand(bool newValue)
    {
        this.isOnLand = newValue;
    }

    public bool GetIsOnLand()
    {
        return this.isOnLand;
    }
}