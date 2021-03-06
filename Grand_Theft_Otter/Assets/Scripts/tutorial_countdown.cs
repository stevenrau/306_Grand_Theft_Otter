﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class tutorial_countdown : MonoBehaviour {

    Text txt;
	public Text txtp1;
	public Text txtp2;
	public Text txtp3;
	public Text txtp4;

	string resetTxt;
    string endTxt;
    public static int countdown;

    bool player1Ready;
    bool player2Ready;
    bool player3Ready;
    bool player4Ready;
    bool everyoneReady;

    // Use this for initialization
    void Start()
    {
        player1Ready = false;
        player2Ready = false;
        player3Ready = false;
        player4Ready = false;
        everyoneReady = false;

        txt = gameObject.GetComponent<Text>();
        resetTxt = "";
        endTxt = "All Players Ready. Game Starting In... ";

		txtp1.text = resetTxt;
		txtp2.text = resetTxt;
		txtp3.text = resetTxt;
		txtp4.text = resetTxt;

        countdown = 3;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!everyoneReady)
        {

            if (Input.GetButtonDown("tutorialReady_1"))
            {
//                txt.text = resetTxt;
                player1Ready = true;
//                txt.text = "Player 1 Ready";
				txtp1.text = "Player 1 Ready";
            }
            if (Input.GetButtonDown("tutorialReady_2"))
            {
//                txt.text = resetTxt;
                player2Ready = true;
//                txt.text = "Player 2 Ready";
				txtp2.text = "Player 2 Ready";

            }

            if (constants.fourPlayers)
            {
                if (Input.GetButtonDown("tutorialReady_3"))
                {
//                    txt.text = resetTxt;
                    player3Ready = true;
//                    txt.text = "Player 3 Ready";
					txtp3.text = "Player 3 Ready";

                }
                if (Input.GetButtonDown("tutorialReady_4"))
                {
//                    txt.text = resetTxt;
                    player4Ready = true;
//                    txt.text = "Player 4 Ready";
					txtp4.text = "Player 4 Ready";

                }
                if (player1Ready && player2Ready && player3Ready && player4Ready)
                {
                    everyoneReady = true;
                    txt.text = resetTxt;
                    //txt.text = "All Players Ready. Game Starting In... ";
                    Invoke("startCountDown", 0.5f);
                }
            }
            else
            {
                if (player1Ready && player2Ready)
                {
                    everyoneReady = true;
                    txt.text = resetTxt;
                    //txt.text = "All Players Ready. Game Starting In... ";
                    Invoke("startCountDown", 0.1f);
                }

            }

			if(Input.GetKeyDown("space")){
				everyoneReady = true;
				txtp1.text = "Player 1 Ready";
				txtp2.text = "Player 2 Ready";
				if(constants.fourPlayers) {
					txtp3.text = "Player 3 Ready";
					txtp4.text = "Player 4 Ready";
				}

				//txt.text = "Starting without everyone ready... ";
				Invoke("startCountDown", 0.1f);
			}


        }
    }
        

    public void startCountDown()
    {
        InvokeRepeating("countingDown", 0.1f, 1f);
    }

    public void countingDown()
    {
        
        txt.text = endTxt + countdown; 

        countdown--;

        
        if (countdown == 0)
        {
            Application.LoadLevel(2);
        }
    }
}
