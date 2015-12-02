using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class tutorial_countdown : MonoBehaviour {

    Text txt;
    string resetTxt;
    public static int countdown;

    // Use this for initialization
    void Start()
    {
        txt = gameObject.GetComponent<Text>();
        resetTxt = txt.text;

        countdown = 30;
        InvokeRepeating("countingDown", 1f, 1f);
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    txt.text = txt.text + countdown;
    //}

    public void countingDown()
    {
        txt.text = resetTxt;
        txt.text = txt.text + countdown;
        countdown--;
        if (countdown == 0)
        {
            Application.LoadLevel(2);
        }
    }
}
