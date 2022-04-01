//This script allows you to toggle music to play and stop.
//Assign an AudioSource to a GameObject and attach an Audio Clip in the Audio Source. Attach this script to the GameObject.

using UnityEngine;

public class Audio : MonoBehaviour
{
    AudioSource[] m_MyAudioSource;


    void Start()
    {
        //Fetch the AudioSource from the GameObject
        m_MyAudioSource = gameObject.GetComponents<AudioSource>();
    }

    void Update()
    {
      
    }

    public void loosePointSound()
    {
        m_MyAudioSource[0].Play();
    }

    public void wolfSound()
    {
        m_MyAudioSource[1].Play();
    }
}