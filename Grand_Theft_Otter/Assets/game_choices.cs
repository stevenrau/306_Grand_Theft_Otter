using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class game_choices : MonoBehaviour {

    public Button playerReg2;
    public Button playerReg4;

    public Button playerTech2;

    public Button playerTech4;
    // Use this for initialization
    void Start () {

        playerReg2 = playerReg2.GetComponent<Button>();
        playerReg4 = playerReg4.GetComponent<Button>();

        playerTech2 = playerTech2.GetComponent<Button>();
        playerTech4 = playerTech4.GetComponent<Button>();

    }

    public void ChoicePlayerReg2() //no tech 2 player
    {
        constants.fourPlayers = false;
        constants.hasTech = false;
        Application.LoadLevel(2); //"2" is set in build settings

    }

    public void ChoicePlayerReg4() //no tech 4 player
    {
        constants.fourPlayers = true;
        constants.hasTech = false;
        Application.LoadLevel(2); //"2" is set in build settings

    }

    public void ChoicePlayerTech2() //tech 2 player
    {
        constants.fourPlayers = false;
        constants.hasTech = true;
        Application.LoadLevel(2); //"2" is set in build settings

    }

    public void ChoicePlayerTech4() //tech 4 player
    {
        constants.fourPlayers = true;
        constants.hasTech = true;
        Application.LoadLevel(2); //"2" is set in build settings

    }
}
