﻿using UnityEngine;
using System.Collections;

public class player_state : MonoBehaviour {

    // Is the player out of the water? should be set when
    bool isOnLand;

	// Is the player touching the air?
    bool canBreathe;

    // Is the player dashing?
    bool isDashing;

	// Is the player touching the wooden platforms?
	bool isTouchingPlatform;

    // Does player have pearl?
    private bool hasPearl;

    // Is player hit?
    private bool isHit;

    // Use this for initialization
    void Start () {
	
		isOnLand = true;
		canBreathe = true;
        isDashing = false;
        hasPearl = false;
        isHit = false;

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

    public void SetIsDashing(bool newValue)
    {
        isDashing = newValue;
    }

    public bool GetIsDashing()
    {
        return isDashing;
    }

    public void SetHasPearl(bool pearl)
    {
        hasPearl = pearl;
    }

    public bool GetHasPearl()
    {
        return hasPearl;
    }

    public void SetIsHit(bool hit)
    {
        isHit = hit;
    }

    public bool GetIsHit()
    {
        return isHit;
    }


	public void SetIsTouchingPlatform(bool touching)
	{
		isTouchingPlatform = touching;
	}
	
	public bool GetIsTouchingPlatform()
	{
		return isTouchingPlatform;
	}

}
