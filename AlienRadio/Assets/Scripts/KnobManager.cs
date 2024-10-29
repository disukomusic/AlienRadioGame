using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class KnobManager : MonoBehaviour
{
    private SerialPort serialPort;
    public string portName = "COM3";  // Set this to the port your Arduino is connected to
    public int baudRate = 9600;

    public RadioManager radioManager;
    private const int MaxAnalogValue = 1023;

    private Thread serialThread;
    private bool isRunning = false;
    private string lastSerialInput;

    private void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        serialPort.Open();
        
        // Start the serial reading thread
        isRunning = true;
        serialThread = new Thread(ReadSerialData);
        serialThread.Start();
    }

    private void ReadSerialData()
    {
        while (isRunning && serialPort.IsOpen)
        {
            try
            {
                lastSerialInput = serialPort.ReadLine();
                ProcessSerialInput(lastSerialInput);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error reading serial data: " + e.Message);
            }
        }
    }

    private void ProcessSerialInput(string serialInput)
    {
        string[] values = serialInput.Split(' ');

        if (values.Length >= 3)
        {
            // Parse and normalize each potentiometer value
            float pot1 = Mathf.Clamp(float.Parse(values[1]) / MaxAnalogValue, 0, 1);
            float pot2 = Mathf.Clamp(float.Parse(values[3]) / MaxAnalogValue, 0, 1);
            float pot3 = Mathf.Clamp(float.Parse(values[5]) / MaxAnalogValue, 0, 1);

            // Update pots array in RadioManager only if values have changed
            if (radioManager.pots[0] != pot1 || radioManager.pots[1] != pot2 || radioManager.pots[2] != pot3)
            {
                radioManager.pots[0] = pot1;
                radioManager.pots[1] = pot2;
                radioManager.pots[2] = pot3;

                // Trigger the update in RadioManager
                radioManager.UpdatePlanets();
            }
        }
    }

    private void OnApplicationQuit()
    {
        isRunning = false;
        
        if (serialThread != null && serialThread.IsAlive)
        {
            serialThread.Join();
        }

        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
