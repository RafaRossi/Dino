using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Manager<AudioManager>
{
    [Header("FX Options")]
    [SerializeField] private AudioSource fxPlayer = null;

    [Header("Music Options")]
    [SerializeField] private AudioSource musicPlayer = null;
    [SerializeField] private Sound musicToPlay = null;

    private void Start()
    {
        PlayMusic(musicToPlay);
    }

    public void StopMusic()
    {
        musicPlayer.Stop();
    }

    public void StopFX()
    {
        fxPlayer.Stop();
    }

    public void PlayFX(Sound sound)
    {
        if (sound != null)
        {
            fxPlayer.loop = sound.loop;
            fxPlayer.PlayOneShot(sound.clips[Random.Range(0, sound.clips.Count)]);
        }
    }

    public void PlayMusic(Sound sound)
    {
        if (sound != null)
        {
            musicToPlay.loop = sound.loop;
            musicPlayer.clip = sound.clips[Random.Range(0, sound.clips.Count)];

            musicPlayer.Play();
        }
    }
}
