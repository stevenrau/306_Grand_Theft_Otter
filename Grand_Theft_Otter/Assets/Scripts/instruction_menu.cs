using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class instruction_menu : MonoBehaviour {

    public Button nextText;
    public Button menuText;

    //counter for rolling through the instruction scenes, start at Objectives

    // Use this for initialization
    void Start () {

        

        nextText = nextText.GetComponent<Button>();
        menuText = menuText.GetComponent<Button>();

    }

    public void clickedNext() 
    {
        constants.sceneCounter = constants.sceneCounter + 1;
        //Debug.Log(constants.sceneCounter);
        Application.LoadLevel(constants.sceneCounter); //set in build settings

    }

    public void clickedMain() 
    {
        
        Application.LoadLevel(0); //set in build settings

    }

    public void clickedPlay()
    {
        
        Application.LoadLevel(1);
    }
}
