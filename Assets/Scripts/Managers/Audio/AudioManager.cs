using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioClip[] backgroundMusicClips;

    public void Start()
    {
        ChangeBackgroundMusic(0);
    }

    public void ChangeBackgroundMusic(int clip)
    {
        backgroundMusic.Stop();
        switch(clip)
        {
            case 0:
                backgroundMusic.clip = backgroundMusicClips[0];
                break;
            case 1:
                backgroundMusic.clip = backgroundMusicClips[1];
                break;
            case 2:
                backgroundMusic.clip = backgroundMusicClips[2];
                break;
            case 3:
                backgroundMusic.clip = backgroundMusicClips[3];
                break;
        }
        backgroundMusic.Play();
    }

}
