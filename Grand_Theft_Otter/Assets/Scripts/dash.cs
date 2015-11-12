﻿using UnityEngine;
using System.Collections;

public class dash : MonoBehaviour {

    //whether player is 'Ready', 'Dashing', or on 'Cooldown'
    public DashState dashState;
    //counts down from max dash time to 0 to reset to 'ready' state
    public float dashTimer;
    //max time between dashes
    public float maxDash = 5f;

    public float dashVelocity = 4f;

    public Vector2 savedVelocity;

    Rigidbody2D r_body;

    //getting scripts
    get_input dashInputScript;

    AudioSource splashSound;

    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }

    void Start()
    {
        r_body = GetComponent<Rigidbody2D>();

        dashInputScript = GetComponent<get_input>();

        splashSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        switch (dashState)
        {
            case DashState.Ready:
                //if dash button is pressed, set velocity... 
                if (dashInputScript.GetDashButton())
                {
                    savedVelocity = r_body.velocity;
                    //r_body.AddForce(savedVelocity * 10f);
                    r_body.velocity = savedVelocity * dashVelocity;
                    //r_body.velocity = new Vector2(r_body.velocity.x * 10f, r_body.velocity.y * 10f);
                    dashState = DashState.Dashing;

                    splashSound.Play();

                    //want player to dash longer than the single frame
                    //StartCoroutine(Waiting()); 
                }
                break;
            case DashState.Dashing:
                dashTimer += Time.deltaTime * 3;
                if (dashTimer >= maxDash)
                {
                    dashTimer = maxDash;
                    r_body.velocity = savedVelocity;
                    dashState = DashState.Cooldown;
                }
                break;
            case DashState.Cooldown:
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }
        
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(5);

    }
}

