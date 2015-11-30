using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class result_menu : MonoBehaviour {

    public Button replay;
    public Button mainMenu;

    // Use this for initialization
    void Start () {

        replay = replay.GetComponent<Button>();
        replay = replay.GetComponent<Button>();

    }

    public void ReplayClicked() //no tech 2 player
    {
        Application.LoadLevel(1); //"2" is set in build settings to game

    }

    public void MainMenuClicked() //no tech 4 player
    {
        Application.LoadLevel(0); //"0" is set in build settings to main menu

    }
}
