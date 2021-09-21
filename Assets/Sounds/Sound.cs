using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Sound File", menuName = "Sound File")]
public class Sound : ScriptableObject
{
    public List<AudioClip> clips = new List<AudioClip>();
    public bool loop = false;
}
