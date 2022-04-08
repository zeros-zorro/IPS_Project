//This script allows you to toggle music to play and stop.
//Assign an AudioSource to a GameObject and attach an Audio Clip in the Audio Source. Attach this script to the GameObject.

using UnityEngine;

public class AudioRing : MonoBehaviour
{
    AudioSource m_MyAudioSource;
    //Play the music
    bool m_Play;

    void Start()
    {
        //Fetch the AudioSource from the GameObject
        m_MyAudioSource = gameObject.GetComponent<AudioSource>();
        m_Play = true;
        
    }

    // To set the volume from the main menu slider
    public void SetVolume(float volume)
    {
        m_MyAudioSource.volume = volume;
    }

    void Update()
    {

    }

    public void winPointSound()
    {
        if (m_Play)
        {
            m_MyAudioSource.Play();
        }
    }
}