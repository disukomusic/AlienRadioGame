using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioStatic : MonoBehaviour
{
    public List<AudioSource> audioSources;
    public AudioSource staticSource;

    private float averageAudioVolume;

    private void Update()
    {
        staticSource.volume = 1 - CalculateAverageVolume(audioSources);
    }
    
    
    float CalculateAverageVolume(List<AudioSource> sources)
    {
        float totalVolume = 0f;

        foreach (AudioSource source in sources)
        {
            totalVolume += source.volume;
        }

        return sources.Count > 0 ? totalVolume / sources.Count : 0f;
    }
}
