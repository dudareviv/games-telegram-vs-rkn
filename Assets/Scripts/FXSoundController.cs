using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FXSoundController : MonoBehaviour
{
    public List<AudioClip> AudioClips;

    public bool PlayOnStart;

    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (!PlayOnStart)
            return;

        PlayRandom();
    }

    public void PlayRandom()
    {
        _audio.Stop();

        _audio.clip = AudioClips[Random.Range(0, AudioClips.Count)];
        _audio.Play();
    }
}