using System;
using System.IO.Ports;
using UnityEngine;

public class KnobManager : MonoBehaviour
{
    public static KnobManager Instance;
    public static string portName = "COM3";
    // Public variables to hold the normalized values of the potentiometers
    public float pot1Value;
    public float pot2Value;
    public float pot3Value;

    // Create a SerialPort object for communication
    SerialPort serialPort = new SerialPort(portName, 9600); // Adjust COM port as necessary

    // Variables to store previous values for comparison
    private float previousPot1Value;
    private float previousPot2Value;
    private float previousPot3Value;

    private void Awake()
    {
        Instance = this;

    }

    void Start()
    {
        // Open the serial port
        serialPort.Open();
        serialPort.ReadTimeout = 100; // Set a read timeout for stability
    }

    void Update()
    {
        // Check if there's data available from Arduino
        if (serialPort.IsOpen && serialPort.BytesToRead > 0)
        {
            try
            {
                // Read a line from the serial port
                string data = serialPort.ReadLine();

                // Split the data by comma
                string[] values = data.Split(',');

                // Ensure we have 3 values
                if (values.Length == 3)
                {
                    // Parse values to floats, scale to 0-1, and map to public variables
                    float newPot1Value = float.Parse(values[0]) / 1023f;
                    float newPot2Value = float.Parse(values[1]) / 1023f;
                    float newPot3Value = float.Parse(values[2]) / 1023f;

                    // Check if any of the potentiometer values have changed
                    if (newPot1Value != previousPot1Value || newPot2Value != previousPot2Value || newPot3Value != previousPot3Value)
                    {
                        // Update the public values
                        pot1Value = newPot1Value;
                        pot2Value = newPot2Value;
                        pot3Value = newPot3Value;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Error reading serial data: " + ex.Message);
            }
        }
    }

    public float GetPotValue(int potIndex)
    {
        //imn so sorry i wanted so badly for this to be modular
        if (potIndex == 0)
        {
            return pot1Value;
        } else if (potIndex == 1)
        {
            return pot2Value;
        }
        else if (potIndex == 2)
        {
            return pot3Value;
        }
        else
        {
            return 0;
        }
    }

    void OnApplicationQuit()
    {
        // Close the serial port when the application quits
        if (serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
