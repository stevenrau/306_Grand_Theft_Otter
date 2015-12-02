using UnityEngine;
using System.Collections;

public class score_keeper : MonoBehaviour {


	private  int leftScore = 0;
	private  int rightScore = 0;
	
	private int maxScore = 5;
	
	static GameObject damRampLeft;
	static GameObject damRampRight;

	sound_player soundPlayer;
	public AudioClip scoreSound1;
	public AudioClip scoreSound2;

	
	
	void Start()
	{
		damRampLeft = GameObject.Find ("Dam_Ramp_Left");
		damRampRight = GameObject.Find ("Dam_Ramp_Right");

		soundPlayer = GameObject.Find ("Sound_Player(Clone)").GetComponent<sound_player>();
	}

	//increase score and build the bridge further with each point scored
	public void IncrementLeftScore()
	{
		if (Random.value > 0.5) {
			soundPlayer.PlayClip(scoreSound1, 1.0f);
			
		}
		else 			
			soundPlayer.PlayClip(scoreSound2, 1.0f);
		if (leftScore < maxScore) {
			leftScore++;
            //enable the next section of the bridge
            damRampLeft.transform.GetChild (leftScore - 1).gameObject.SetActive (true);
		}

        //if (leftScore == maxScore)
        //{
        //    Application.LoadLevel(3);
        //}
		//print ("Left score: " + leftScore);
	}
	
	public void IncrementRightScore()
	{

		if (Random.value > 0.5) 
				soundPlayer.PlayClip(scoreSound1, 1.0f);
				
		else 				
				soundPlayer.PlayClip(scoreSound2, 1.0f);

		if (rightScore < maxScore) {
			rightScore++;
			//enable the next section of the bridge
			damRampRight.transform.GetChild (rightScore - 1).gameObject.SetActive (true);
		}

        //if (rightScore == maxScore)
        //{
        //    Application.LoadLevel(4);
        //}
        //print ("Right score: " + rightScore);
    }

	public void ResetScores(){
		leftScore = 0;
		rightScore = 0;
	}

    public int getLeftScore()
    {
        return leftScore;
    }

    public int getRightScore()
    {
        return rightScore;
    }

    public int getMaxScore()
    {
        return maxScore;
    }
}
