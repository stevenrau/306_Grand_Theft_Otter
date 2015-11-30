
using UnityEngine;
using System.Collections;

public class get_input : MonoBehaviour
{

    // to determine which platform compatability
    private bool isPC = true;

	//the script that will store team number
	private player_state playerStateScript;

    // to determine which controller is mapped
    public string playerID;


    // controller names in Prject Settings -> Input Manager
    private string movHorz;    // movement axis
    private string movVert;

    private string aimHorz;    // aiming axis
    private string aimVert;

    private string throwBumper; // passing button 
    private string dashButton; // dash button
    private string startButton; // start or pause button 

    // Use this for initialization
    void Start()
    {
		//grab reference to state script
		playerStateScript = GetComponent<player_state> ();

		// check the os to ensure that the proper control scheme is used
        RuntimePlatform os = Application.platform;
        print(os.ToString());
        if (os == RuntimePlatform.WindowsEditor || os == RuntimePlatform.WindowsPlayer)
        {
            isPC = true;
            print("it's windows!!");
        }
        else if (os == RuntimePlatform.OSXEditor || os == RuntimePlatform.OSXPlayer)
        {
            isPC = false;
            print("it's Mac!!");
        }
        else
        {
            isPC = true;
            print("it's Other!!");
        }

		//SetPlayerID (playerID);
		SetPlayerID (playerStateScript.GetPlayerID ());
    }


    /***************************************************************************************
	* Movement Input
	* ************************************************************************************/

    // Horizontal Axis of Direction
    public float GetMoveHorizontalAxis()
    {

        return Input.GetAxis(movHorz);

    }

    // Vertical Axis of Direction
    public float GetMoveVerticalAxis()
    {

        return Input.GetAxis(movVert);

    }


    /***************************************************************************************
	* Aiming Input
	* ************************************************************************************/

    // Horizontal Axis of Direction
    public float GetAimHorizontalAxis()
    {

        return Input.GetAxis(aimHorz);

    }

    // Vertical Axis of Direction
    public float GetAimVerticalAxis()
    {

        return Input.GetAxis(aimVert);

    }


    /***************************************************************************************
	* Throwing Input
	* ************************************************************************************/
    public bool GetThrowingButton()
    {
        return Input.GetButton(throwBumper);

    }

    /***************************************************************************************
    * Dash Input
    * ************************************************************************************/
    public bool GetDashButton()
    {
        return Input.GetButton(dashButton);
    }

    /***************************************************************************************
    * Pause Input
    * ************************************************************************************/
    public bool GetStartButton()
    {
        return Input.GetButton(startButton);
    }

    // Setting player ID to distingish between players and platforms
    public void SetPlayerID(string id)
    {
        playerID = id;

		//set team number now for easy access later
		if(id == "1" || id == "3")
		{
			playerStateScript.SetTeamNumber("1");  // team 1 is controllers 1 and 2

		}
		else
		{
			playerStateScript.SetTeamNumber("2"); // team 2 is controllers 3 and 4
		}

        movHorz = "left_analog_horizontal_" + playerID;
        movVert = "left_analog_vertical_" + playerID;

        if (isPC)
        {

            aimHorz = "right_analog_horizontal_" + playerID;
            aimVert = "right_analog_vertical_" + playerID;

            throwBumper = "r_bumper_" + playerID;
            dashButton = "dash_" + playerID;
            startButton = "Pause";
        }
        else
        {
            aimHorz = "right_analog_horizontal_Mac_" + playerID;
            aimVert = "right_analog_vertical_Mac_" + playerID;

            throwBumper = "r_bumper_Mac_" + playerID;
            dashButton = "dash_Mac_" + playerID;
            startButton = "Pause_Mac";
        }
    }

    // returns the player's id (controller) : 1, 2, 3, or 4
    public string GetPlayerID()
    {
        return playerID;
    }

   
    // returns the platform type
    public bool PlatformIsPC()
    {
        return isPC;
    }
}
