﻿using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = .25f;

    [Range(0.1f, 3f)]
    public float pitch = 1f;

    public bool loop;

    public AudioSource source;
}