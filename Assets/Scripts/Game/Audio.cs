//This script allows you to toggle music to play and stop.
//Assign an AudioSource to a GameObject and attach an Audio Clip in the Audio Source. Attach this script to the GameObject.

using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    private AudioSource[] m_MyAudioSource;
    public Slider slider;

    void Start()
    {
        //Fetch the AudioSource from the GameObject
        gameObject.tag = GameManager.AUDIO_TAG;
        m_MyAudioSource = gameObject.GetComponents<AudioSource>();
        // To play the background music
        PlayMusic();
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(transform.gameObject);

    }

    // To play the background music
    public void PlayMusic()
    {
        if (m_MyAudioSource[0].isPlaying) return;
        m_MyAudioSource[0].Play();
    }

    // To stop the background music
    public void StopMusic()
    {
        m_MyAudioSource[0].Stop();
    }

    // When going back to the main menu, to reset the audio
    public void KillMusic()
    {
        StopMusic();
        Destroy(transform.gameObject);
    }

    // To set the volume from the main menu slider
    public void SetVolume(float volume)
    {
        foreach (AudioSource aud in m_MyAudioSource)
        {
            aud.volume = volume;
        }
    }

    // Functions to play different sounds during the game

    public void loosePointSound()
    {
        m_MyAudioSource[1].Play();
    }

    public void wolfSound()
    {
        m_MyAudioSource[2].Play();
    }

    public void sheepSound()
    {
        m_MyAudioSource[3].Play();
    }

    public void winPointSound()
    {
        m_MyAudioSource[4].Play();
    }
}