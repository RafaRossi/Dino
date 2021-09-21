using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Manager<AudioManager>
{
    [SerializeField] private AudioSource fxPlayer = null;
    [SerializeField] private AudioSource musicPlayer = null;

    protected override void Initialize()
    {
        base.Initialize();

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void PlayFX(Sound sound)
    {
        if (sound != null)
        {
            PlaySound(fxPlayer, sound);
        }
    }

    public void PlayMusic(Sound sound)
    {
        if (sound != null)
        {
            PlaySound(musicPlayer, sound);
        }
    }

    private void PlaySound(AudioSource source, Sound sound)
    {
        source.PlayOneShot(sound.clips[Random.Range(0, sound.clips.Count)]);
        source.volume = sound.volume;
    }
}
