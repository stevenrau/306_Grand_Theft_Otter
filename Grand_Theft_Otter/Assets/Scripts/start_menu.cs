/* 
followed the tutorial http://xenosmashgames.com/creating-start-menu-unity-5/ as a start on making the main menu

*/
using UnityEngine;
using UnityEngine.UI; // we need this namespace in order to access UI elements within our script
using System.Collections;


public class start_menu : MonoBehaviour
{

    /********************************************************************
    * gamechoices screen:
    *********************************************************************/

    public Button playerReg2;
    public Button playerReg4;  

    public Button playerTech2;
    public Button playerTech4;

    /********************************************************************
    * startscreen:
    *********************************************************************/
    //public Canvas quitMenu; // no longer popping up
    public Button startText;
    public Button exitText;

    void Start()
    {
        /********************************************************************
        * gamechoices screen:
        *********************************************************************/
        playerReg2 = playerReg2.GetComponent<Button>();
        playerReg4 = playerReg4.GetComponent<Button>();

        playerTech2 = playerTech2.GetComponent<Button>();
        playerTech4 = playerTech4.GetComponent<Button>();

        /********************************************************************
        * startscreen:
        *********************************************************************/
        //quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        //quitMenu.enabled = false;

    }

    /********************************************************************
    * Setting up game play according to user choices; sending to Game
    *********************************************************************/

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

    /********************************************************************
    * Taking user to next screen 
    *********************************************************************/

    //With use of XBOX controller instead of mouse, the next two functions that pop up the canvas
    //for a quit menu check are complicated because the event system no longer works
    //public void ExitPress() //this function will be used on our Exit button
    //{
    //    quitMenu.enabled = true; //enable the Quit menu when we click the Exit button
    //    startText.enabled = false; //then disable the Play and Exit buttons so they cannot be clicked
    //    exitText.enabled = false;

    //}

    //public void NoPress() //this function will be used for our "NO" button in our Quit Menu
    //{
    //    quitMenu.enabled = false; //we'll disable the quit menu, meaning it won't be visible anymore
    //    startText.enabled = true; //enable the Play and Exit buttons again so they can be clicked
    //    exitText.enabled = true;

    //}

    public void StartGameChoices() //this function will be used on our "Play" button click
    {
        Application.LoadLevel(1); //"1" is set in build settings

    }

    public void StartInstructions() //this function used on "Instructions" button click
    {
        Application.LoadLevel(2); // "2" is set in build settings
    }

    public void ExitGame() //This function will be used on our "Yes" button in our Quit menu
    {
        Application.Quit(); //this will quit our game. Note this will only work after building the game

    }

}