using UnityEngine;
using System.Collections;

public class get_input : MonoBehaviour
{

    // to determine which platform compatability
    private bool isPC = true;

    // to determine which controller is mapped
    string playerID;


    // controller names in Prject Settings -> Input Manager
    private string mov_horz;    // movement axis
    private string mov_vert;

    private string aim_horz;    // aiming axis
    private string aim_vert;

    private string throw_bumper; // passing button 

    // Use this for initialization
    void Start()
    {
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
    }


    /***************************************************************************************
	* Movement Input
	* ************************************************************************************/

    // Horizontal Axis of Direction
    public float GetMoveHorizontalAxis()
    {

        return Input.GetAxis(mov_horz);

    }

    // Vertical Axis of Direction
    public float GetMoveVerticalAxis()
    {

        return Input.GetAxis(mov_vert);

    }


    /***************************************************************************************
	* Aiming Input
	* ************************************************************************************/

    // Horizontal Axis of Direction
    public float GetAimHorizontalAxis()
    {

        return Input.GetAxis(aim_horz);

    }

    // Vertical Axis of Direction
    public float GetAimVerticalAxis()
    {

        return Input.GetAxis(aim_vert);

    }


    /***************************************************************************************
	* Throwing Input
	* ************************************************************************************/
    public bool GetThrowingButton()
    {

        return Input.GetButton(throw_bumper);

    }



    // Setting player ID to distingish between players and platforms
    public void SetPlayerID(string id)
    {
        playerID = id;

        mov_horz = "left_analog_horizontal_" + playerID;
        mov_vert = "left_analog_vertical_" + playerID;

        if (isPC)
        {

            aim_horz = "right_analog_horizontal_" + playerID;
            aim_vert = "right_analog_vertical_" + playerID;

            throw_bumper = "r_bumper_" + playerID;
        }
        else
        {
            aim_horz = "right_analog_horizontal_Mac_" + playerID;
            aim_vert = "right_analog_vertical_Mac_" + playerID;

            throw_bumper = "r_bumper_Mac_" + playerID;
        }
    }

    // returns the platform type
    public bool PlatformIsPC()
    {
        return isPC;
    }
}
