using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Sound File", menuName = "Sound File")]
public class Sound : ScriptableObject
{
    public List<AudioClip> clips = new List<AudioClip>();
    [Range(0, 1)] public float volume;
}
