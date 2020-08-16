using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip playButton;
    public AudioClip exitButton;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //This is for whenever the player presses F it will sound the sound.
        if (Input.GetKeyDown(KeyCode.F))
        {
            audioSource.clip = playButton;
            audioSource.Play();
        }
    }
    //This is for the button for when the player clicks the button that it plays the sound.
    public void PlayButtonSelect()
    {
        audioSource.clip = playButton;
        audioSource.Play();
    }
}
