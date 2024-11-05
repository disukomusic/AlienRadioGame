using System;
using UnityEngine;

public class LedManager : MonoBehaviour
{
    public static LedManager Instance;
    public AudioSource[] Earthsources;
    public AudioSource[] Hotsources;
    public AudioSource[] Coldsources;

    private void Start()
    {
        Instance = this;
    }

    public void SendData()
    {
        // Calculate and send data if average volume > 0.8 for Earth sources
        CalculateAndSendAverage(Earthsources, 0);

        
        // Calculate and send data if average volume > 0.8 for Hot sources
        CalculateAndSendAverage(Hotsources, 1);

        // Calculate and send data if average volume > 0.8 for Cold sources
        CalculateAndSendAverage(Coldsources, 2);

    }

    private bool CalculateAndSendAverage(AudioSource[] sources, int planetIndex)
    {
        if (sources.Length == 0) return false;

        float totalVolume = 0f;

        foreach (AudioSource source in sources)
        {
            totalVolume += source.volume;
        }

        float averageVolume = totalVolume / sources.Length;
        //Debug.Log($"Average volume for planet {planetIndex}: {averageVolume}");

        if (averageVolume > 0.5f)
        {
            if (KnobManager.Instance.serialPort.IsOpen)
            {
                KnobManager.Instance.serialPort.WriteLine($"{planetIndex}, {averageVolume}");
                Debug.Log($"{planetIndex}, {averageVolume}");
                return true;
            }
            else
            {
                Debug.LogWarning("Serial port is not open!");
                
            }
        }
        else if (averageVolume < 0.5f)
        {
            KnobManager.Instance.serialPort.WriteLine($"off");
            Debug.Log($"off");
        }

        return false;
    }
}