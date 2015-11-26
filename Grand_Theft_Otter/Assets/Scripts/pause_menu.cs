using UnityEngine;
using System.Collections;

public class pause_menu : MonoBehaviour {

    private GameObject PauseUI;

    private bool paused = false;

    //get_input pauseInputScript;

    void Start()
    {
        PauseUI = GameObject.Find("pause_menu");

        //pauseInputScript = GetComponent<get_input>(); 
        PauseUI.SetActive(false);

    }

    void Update()
    {
        //if (pauseInputScript.GetStartButton())
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
            if (paused)
            {
                PauseUI.SetActive(true);
                //stop time
                Time.timeScale = 0;
            }
            else if (!paused)
            {
                PauseUI.SetActive(false);
                //set time back to normal
                Time.timeScale = 1;
            }
        }

        

        ////team one wins
        //else if (leftScore == maxScore)
        //{
        //    Instantiate(Resources.Load("results_menu_1"));
        //}

        ////team two wins
        //else if (rightScore == maxScore)
        //{
        //    Instantiate(Resources.Load("results_menu_2"));
        //}
    }
}
