using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicNormal;
    public AudioSource musicNoTime;

    private void Start()
    {
        AudioListener.pause = false;
        Time.timeScale = 1;

        musicNoTime.volume = 0f;
        musicNormal.volume = 0.2f;

        musicNormal.Play();
        musicNoTime.Stop();
    }

    public void StopMusicNormal()
    {
        musicNormal.Stop();
    }

    public void StopMusicNoTime()
    {
        musicNoTime.Stop();
    }

    public void PlayMusicNormal()
    {
        musicNormal.Play();
    }

    public void PlayMusicNoTime()
    {
        musicNoTime.Play();
    }

    public void MusicNormalVolume()
    {
        musicNoTime.volume = 0f;
        musicNormal.volume = Mathf.Lerp(musicNormal.volume, 0.3f, Time.deltaTime * 0.5f);
    }

    public void MusicNoTimeVolume()
    {
        musicNormal.volume = 0f;
        musicNoTime.volume = Mathf.Lerp(musicNoTime.volume, 0.5f, Time.deltaTime * 0.5f);
    }
}

