using System;
using UnityEngine;


public class RadioChannel : MonoBehaviour
{
    public PlanetSoundContainer[] PlanetSounds;
    

    public void UpdateSoundVolume(int soundIndex, float volume)
    {
        var planetSound = PlanetSounds[soundIndex];
        var audioSource = PlanetSounds[soundIndex].audioSource;
        
        float midpoint = (planetSound.planetSound.low + planetSound.planetSound.high) / 2;
        
        float normalizedVolume = Mathf.Abs(volume - midpoint) / (midpoint - planetSound.planetSound.low);
        audioSource.volume = 1 - normalizedVolume; 
    }
}
