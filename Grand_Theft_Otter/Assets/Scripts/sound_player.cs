using UnityEngine;
using System.Collections;

// This script is responsible for playing the sounds associated with the whole game
// This script has access to an audio source that is never destroyed, so any script 
// that may want to play a sound should use the public method included in this script.

// The script that wants to play the sound should have a field that is an audio clip that they want to play

public class sound_player : MonoBehaviour {

    
    AudioSource source; //the persistant audio source component

	// Use this for initialization
	void Start ()
    {
        source = GetComponent<AudioSource>(); //get the reference to the audio source
	}
	
    //simply play a one shot clip at a given volume
    public void PlayClip(AudioClip clip, float vol)
    {
        source.PlayOneShot(clip, vol);
    }
}
