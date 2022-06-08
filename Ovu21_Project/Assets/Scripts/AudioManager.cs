using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;

            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.loop;
            s.audioSource.playOnAwake = s.playOnAwake;
        }
    }

    private void Start()
    {
        PlaySound("Theme");
    }

    public void PlaySound(string audioName)
    {
        Sound s = Array.Find(sounds, sound => sound.audioName == audioName);
        if (s == null)
        {
            Debug.LogWarning($"Sound {audioName} not found!");
            return;
        }
        s.audioSource.Play();
    }

    public void StopSound(string audioName)
    {
        Sound sound = Array.Find(sounds, sound => sound.audioName == audioName);
        if (sound == null)
        {
            Debug.LogWarning($"Sound {audioName} not found!");
            return;
        }
        sound.audioSource.Stop();
    }
}