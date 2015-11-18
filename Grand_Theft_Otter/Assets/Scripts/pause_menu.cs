using UnityEngine;
using System.Collections;

public class pause_menu : MonoBehaviour {

    public GameObject PauseUI;

    private bool paused = false;

    //get_input pauseInputScript;

    void Start()
    {
        //pauseInputScript = GetComponent<get_input>(); how to access the script from a different gameObject?
        PauseUI.SetActive(false);
    }

    void Update()
    {
        //if (pauseInputScript.GetStartButton())
        if(Input.GetButton("Pause"))
        {
            paused = !paused;
        }

        if (paused)
        {
            PauseUI.SetActive(true);
            //stop time
            Time.timeScale = 0;
        }

        if (!paused)
        {
            PauseUI.SetActive(false);
            //set time back to normal
            Time.timeScale = 1;
        }
    }
}
