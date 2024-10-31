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

        // Update each channel's sound volume
        for (var _index = 0; _index < channels.Length; _index++)
        {
            var _channel = channels[_index];
            _channel.UpdateSoundVolume(_index, pots[_index]);
        }
    }
}