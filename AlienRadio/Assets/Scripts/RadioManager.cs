using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RadioManager : MonoBehaviour
{
    public static RadioManager Instance;
    public RadioChannel[] channels;

    // Array to hold potentiometer values
    public float[] pots;

    private void Start()
    {
        Instance = this;
        pots = new float[channels.Length]; // Initialize the pots array
    }

    // UpdatePlanets now takes an array of pot values
    public void UpdatePlanets(float[] potValues)
    {
        // Update pots with the incoming values
        pots = potValues;

        // Loop through each channel
        for (var channelIndex = 0; channelIndex < channels.Length; channelIndex++)
        {
            var channel = channels[channelIndex];

            // Loop through each sound in the channel
            for (var soundIndex = 0; soundIndex < channel.PlanetSounds.Length; soundIndex++)
            {
                // Check if the corresponding potentiometer exists
                if (channelIndex < pots.Length)
                {
                    // Update each sound's volume based on the corresponding potentiometer value
                    channel.UpdateSoundVolume(soundIndex, pots[channelIndex]);
                }
                else
                {
                    Debug.LogWarning($"No potentiometer value found for channel index {channelIndex}");
                }
            }

            Debug.Log($"Now updating Channel Index {channelIndex}");
        }
    }

}