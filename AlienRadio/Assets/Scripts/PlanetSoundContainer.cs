using System;
using UnityEngine;


public class PlanetSoundContainer : MonoBehaviour
{
    public PlanetSound planetSound;
    
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); 
    }
}
