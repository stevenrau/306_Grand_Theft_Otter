using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pause_menu : MonoBehaviour {

    //private int numPlayers;
    private GameObject theGame;

    private GameObject PauseUI;

    private bool paused = false;

    //public Button resume;
    private Button restart;
    private Button mainMenu;
    private Button quit;

    //GameObject[] beaverPlayer;

    //// the scripts that will be disabled when paused
    //moving[] movingScript;
    //dash[] dashScript;
    //throwing[] throwingScript;

    void Start()
    {

        PauseUI = GameObject.Find("pause_menu").transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        PauseUI.SetActive(false);

        //resume = resume.GetComponent<Button>();
        restart = PauseUI.transform.GetChild(1).gameObject.GetComponent<Button>();
        mainMenu = PauseUI.transform.GetChild(2).gameObject.GetComponent<Button>();
        quit = PauseUI.transform.GetChild(3).gameObject.GetComponent<Button>();

        //theGame = GameObject.Find("game_setup");

        //if(constants.fourPlayers)
        //{
        //    numPlayers = 4;
        //}

        //beaverPlayer = new GameObject[numPlayers];
        //movingScript = new moving[numPlayers];
        //dashScript = new dash[numPlayers];
        //throwingScript = new throwing[numPlayers];

        //beaverPlayer = GameObject.FindGameObjectsWithTag("Player");

        ////scripts that will be disabled
        //for (int i = 0; i < numPlayers; i++)
        //{
        //    movingScript[i] = beaverPlayer[i].GetComponent<moving>();
        //    dashScript[i] = beaverPlayer[i].GetComponent<dash>();
        //    throwingScript[i] = beaverPlayer[i].GetComponent<throwing>();
        //}




    }

    void Update()
    {
        //if (pauseInputScript.GetStartButton())
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;

            if (paused) //paused is on
            {
                PauseUI.SetActive(true);

                //stop time
                Time.timeScale = 0;
            }
            else if (!paused) //game resumes
            {
                PauseUI.SetActive(false);

                //set time back to normal
                Time.timeScale = 1;
            }

            //PlayerFreeze(paused);
        }
        
    }

    /********************************************************************
    * Sprites need to freeze and not be controlled by button presses
    *********************************************************************/

    //public void PlayerFreeze(bool isPaused)
    //{
    //    //disable player controls 
    //    for(int i = 0; i < numPlayers; i++)
    //    {
    //        movingScript[i].enabled = isPaused;
    //        dashScript[i].enabled = isPaused;
    //        throwingScript[i].enabled = isPaused;
    //    }
        
    //}


    /********************************************************************
    * Setting up pause button functionality
    *********************************************************************/

    public void PressedResume() 
    {

        paused = !paused;
        //PlayerFreeze(paused);

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

    public void PressedRestart() 
    {
        //Destroy(theGame);
        //theGame = Instantiate (Resources.Load ("game_setup")) as GameObject;
        Time.timeScale = 1;
        Application.LoadLevel(1);

    }

    public void PressedMainMenu() 
    {
        //Destroy(theGame);
        Time.timeScale = 1;
        Application.LoadLevel(0);

    }

    public void PressedQuit()
    {

        Application.Quit();

    }

}
