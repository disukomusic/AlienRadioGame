using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioManager : MonoBehaviour
{
    public RadioChannel[] channels;
    
    public float[] pots;
    private void Update()
    {
        for (var _index = 0; _index < channels.Length; _index++)
        {
            var _channel = channels[_index];
            _channel.UpdateSoundVolume(_index, pots[_index]);
        }
    }
}
