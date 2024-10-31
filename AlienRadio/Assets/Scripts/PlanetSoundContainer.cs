using System;
using UnityEngine;


public class PlanetSoundContainer : MonoBehaviour
{
    public int potIndex;
    public PlanetSound planetSound;
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    private void Update()
    {
        float low = planetSound.low;
        float high = planetSound.high;
    
        float midpoint = (low + high) / 2;

        float currentValue = KnobManager.Instance.GetPotValue(potIndex); 

        // Calculate the normalized value between 0 and 1
        float normalizedValue = (currentValue - low) / (high - low);

        // Use a quadratic function to set the volume (parabola opening down)
        // This makes the volume loudest at the midpoint
        audioSource.volume = 1 - Mathf.Pow(normalizedValue - 0.5f, 2) * 4; // Adjust '4' to change the steepness

        // Clamp the volume between 0 and 1
        audioSource.volume = Mathf.Clamp(audioSource.volume, 0, 1);
    }
}
