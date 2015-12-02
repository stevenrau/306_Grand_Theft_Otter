using UnityEngine;
using System.Collections;

public class boat : MonoBehaviour {

    SpriteRenderer boatSprite;
    Animator boatAnim;


    score_keeper scoreScript;

    Collider2D[] boatColliders;

	// Use this for initialization
	void Start () {

        boatSprite = GetComponent<SpriteRenderer>();
        boatSprite.sortingOrder = -1;
        boatAnim = GetComponent<Animator>();

        scoreScript = GameObject.FindGameObjectWithTag("Score_Keeper").GetComponent<score_keeper>();

        boatColliders = GetComponents<Collider2D>();
        boatColliders[0].enabled = false;
        boatColliders[1].enabled = false;

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
            boatSprite.sortingOrder = 1;
            boatColliders[0].enabled = true;
            boatColliders[1].enabled = true;
        }

    }
}
