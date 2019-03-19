﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ambientSource : MonoBehaviour
{
    public float maxRange = 5.0f;
    public AudioClip[] soundClips = new AudioClip[0];

    AudioSource audioS;

    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        audioS.clip = soundClips[Random.Range(0, soundClips.Length - 1)];
        audioS.Play();
    }

    private void Update()
    {
        float f = Vector2.Distance(transform.position, PlayerTracker.Instance.transform.position);
        audioS.volume = Mathf.Lerp(0.0f, maxRange, f) * AudioManager.instance.sfxVolume;
    }
}
