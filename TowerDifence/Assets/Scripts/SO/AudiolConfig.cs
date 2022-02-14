using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudiolConfig : ScriptableObject
{
    private float _soundPover;
    private float _musicPover;

    public float Music => _musicPover;
    public float Sound => _soundPover;

    public void InitMusic(float music)
    {
        _musicPover = music;
    }

    public void InitSound(float sound)
    {
        _soundPover = sound;
    }
}
