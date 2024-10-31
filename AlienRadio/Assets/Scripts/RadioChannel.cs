using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class RadioChannel : MonoBehaviour
{
    public PlanetSoundContainer[] PlanetSounds;

    public void UpdateSoundVolume(int soundIndex, float volume)
    {
        var planetSound = PlanetSounds[soundIndex];
        var audioSource = planetSound.audioSource;

        // Get low and high values from the planet sound
        float low = planetSound.planetSound.low;
        float high = planetSound.planetSound.high;
        
        // Calculate the sweet spot as the midpoint
        float sweetSpot = (low + high) / 2;

        // Adjust volume based on the potentiometer value
        if (volume <= low)
        {
            audioSource.volume = 0; // Volume is 0 if the potentiometer is at or below the low point
        }
        else if (volume >= high)
        {
            audioSource.volume = 0; // Volume is also 0 if the potentiometer is at or above the high point
        }
        else
        {
            // Calculate distance from the sweet spot
            float distanceFromSweetSpot = Mathf.Abs(volume - sweetSpot);
            // Calculate the maximum distance to normalize
            float maxDistance = Mathf.Abs(sweetSpot - low); // or (high - sweetSpot)

            // Normalize the volume: 1 - (distanceFromSweetSpot / maxDistance)
            audioSource.volume = 1 - (distanceFromSweetSpot / maxDistance);
        }

        // Debug log to verify volume levels
        Debug.Log($"Sound Index: {soundIndex}, Pot Value: {volume}, Volume: {audioSource.volume}");
    }
}