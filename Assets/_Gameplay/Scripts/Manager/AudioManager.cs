using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    [SerializeField] private List<AudioClip> audioClipList;
    [SerializeField] private AudioSource audioSource;
    void Start()
    {
        LoadAudioClip();
    }

    private void LoadAudioClip()
    {
        foreach (AudioClip audioClip in audioClipList)
        {
            audioClips.Add(audioClip.name, audioClip);
        }
    }

    public void PlaySound(string soundName)
    {
        if (audioClips.ContainsKey(soundName))
        {
            audioSource.PlayOneShot(audioClips[soundName]);
            Debug.Log(audioClips[soundName]);
        }
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
