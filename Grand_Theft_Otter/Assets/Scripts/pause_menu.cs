using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pause_menu : MonoBehaviour {

    private GameObject PauseUI;

    private bool paused = false;

    public Button resume;
    public Button restart;
    public Button mainMenu;
    public Button quit;

    //get_input pauseInputScript;

    void Start()
    {
        resume = resume.GetComponent<Button>();
        restart = restart.GetComponent<Button>();
        mainMenu = mainMenu.GetComponent<Button>();
        quit = quit.GetComponent<Button>();

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
        
    }

    /********************************************************************
    * Setting up pause button functionality
    *********************************************************************/

    public void PressedResume() 
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

    public void PressedRestart() 
    {

        Application.LoadLevel(2);

    }

    public void PressedMainMenu() 
    {

        Application.LoadLevel(0);

    }

    public void PressedQuit()
    {

        Application.Quit();

    }

}
