using System;
using UnityEngine;


public class RadioChannel : MonoBehaviour
{
    public PlanetSoundContainer[] PlanetSounds;
    

    public void UpdateSoundVolume(int soundIndex, float volume)
    {
        var planetSound = PlanetSounds[soundIndex];
        var audioSource = planetSound.audioSource;

        // Calculate low, high, and midpoint values
        float low = planetSound.planetSound.low;
        float high = planetSound.planetSound.high;
        float midpoint = (low + high) / 2;

        // Normalize the volume based on the distance from the midpoint
        if (volume <= low)
        {
            audioSource.volume = 0; // Volume is 0 if the potentiometer is at the low point
        }
        else if (volume >= high)
        {
            audioSource.volume = 0; // Volume is also 0 if the potentiometer is at the high point
        }
        else
        {
            // Calculate the distance from the midpoint
            float distanceFromMidpoint = Mathf.Abs(volume - midpoint);
            // The max distance from midpoint to either low or high
            float maxDistance = Mathf.Abs(midpoint - low); // or (high - midpoint)

            // Normalize the volume: 1 - (distanceFromMidpoint / maxDistance)
            audioSource.volume = 1 - (distanceFromMidpoint / maxDistance);
        }

        // Debug log to verify volume levels
        Debug.Log($"Sound Index: {soundIndex}, Pot Value: {volume}, Volume: {audioSource.volume}");
    }

}
