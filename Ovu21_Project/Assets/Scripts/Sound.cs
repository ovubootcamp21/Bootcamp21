using UnityEngine;

[System.Serializable]
public class Sound
{
    [HideInInspector]
    public AudioSource audioSource;

    public AudioClip audioClip;

    public string audioName;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    public bool playOnAwake;
}