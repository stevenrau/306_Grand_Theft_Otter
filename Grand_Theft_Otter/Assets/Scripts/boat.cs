using UnityEngine;
using System.Collections;

public class boat : MonoBehaviour {

    GameObject boatSprite;
    Animator boatAnim;

    score_keeper scoreScript;

	// Use this for initialization
	void Start () {

        boatSprite = transform.gameObject;
        boatAnim = boatSprite.GetComponent<Animator>();

        scoreScript = GameObject.FindGameObjectWithTag("Score_Keeper").GetComponent<score_keeper>();

    }
	
	// Update is called once per frame
	void Update () {

        if(scoreScript.getLeftScore() == 1 || scoreScript.getRightScore() == 1)
        {
            boatAnim.SetInteger("maxPoints", 1);
        }

        if (scoreScript.getLeftScore() == 3 || scoreScript.getRightScore() == 3)
        {
            boatAnim.SetInteger("maxPoints", 3);
        }

        if (scoreScript.getLeftScore() == 5 || scoreScript.getRightScore() == 5)
        {
            boatAnim.SetInteger("maxPoints", 5);
        }

    }
}
