using System;
using UnityEngine;
using System.IO.Ports;
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
        try
        {
            foreach (AudioSource audioSource in Earthsources)
            {
                PlanetSoundContainer container = audioSource.GetComponent<PlanetSoundContainer>();
                if (container != null)
                {
                    int potIndex = container.potIndex;
                    int planetIndex = 0; // Hardcoded
                    float earthVolume = audioSource.volume;

                    if (KnobManager.Instance.serialPort.IsOpen)
                    {
                        KnobManager.Instance.serialPort.WriteLine($"{potIndex}, {planetIndex}, {earthVolume}");
                        Debug.Log($"{potIndex}, {planetIndex}, {earthVolume}");
                    }
                    else
                    {
                        Debug.LogWarning("Serial port is not open!");
                    }
                }
                else
                {
                    Debug.LogWarning("PlanetSoundContainer not found on AudioSource.");
                }
            }

            foreach (AudioSource audioSource in Hotsources)
            {
                PlanetSoundContainer container = audioSource.GetComponent<PlanetSoundContainer>();
                if (container != null)
                {
                    int potIndex = container.potIndex;
                    int planetIndex = 1; // Hardcoded
                    float hotVolume = audioSource.volume;

                    if (KnobManager.Instance.serialPort.IsOpen)
                    {
                        KnobManager.Instance.serialPort.WriteLine($"{potIndex}, {planetIndex}, {hotVolume}");
                        Debug.Log($"{potIndex}, {planetIndex}, {hotVolume}");
                    }
                    else
                    {
                        Debug.LogWarning("Serial port is not open!");
                    }
                }
            }

            foreach (AudioSource audioSource in Coldsources)
            {
                PlanetSoundContainer container = audioSource.GetComponent<PlanetSoundContainer>();
                if (container != null)
                {
                    int potIndex = container.potIndex;
                    int planetIndex = 2; // Hardcoded
                    float coldVolume = audioSource.volume;

                    if (KnobManager.Instance.serialPort.IsOpen)
                    {
                        KnobManager.Instance.serialPort.WriteLine($"{potIndex}, {planetIndex}, {coldVolume}");
                        Debug.Log($"{potIndex}, {planetIndex}, {coldVolume}");
                    }
                    else
                    {
                        Debug.LogWarning("Serial port is not open!");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in SendData: " + ex.Message);
        }
    }
}
