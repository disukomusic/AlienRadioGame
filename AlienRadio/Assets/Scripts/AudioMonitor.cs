using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMonitor : MonoBehaviour
{
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    void Update()
    {
        transform.localScale = new Vector3(audioSource.volume, audioSource.volume, audioSource.volume);
    }
}
