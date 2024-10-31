using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://pastebin.com/V0j6exYJ
// i'll be adding my own comments/thoughts here as i try to
// understand a random, unknown pastebin piece of code
public class Oscilloscope : MonoBehaviour
{
    [Range(0, 2000)]
    public double frequency = 400.0f;
 
    private double increment;
    private double phase;
    private readonly double sampleRate = 48000.0f;
 
    [Range(0,32)]
    public float gain = 0.5f;
 
    [Range(0, 32)]
    public int c = 10;
 
    [Range(-32, 32)]
    // what does this do?
    public int ind = 1;
 
    [Range(-256, 256)]
    public float z = 5f;
 
    [Range(0, 2)]
    public float a = 0.75f;
 
    [Range(-0, 4)]
    public float b = 7f;
 
    [Range(0, 10)]
    public byte choice = 0;
 
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    [Range(512, 16384)] 
    public int lengthOfLineRenderer;

    private void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.02f;
        lineRenderer.positionCount = lengthOfLineRenderer;
        lineRenderer.useWorldSpace = false;
 
        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;
    }

    private void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        // an audioclip can be attached here,
        // but *none* of the sliders affect the visual output
        AudioSource source = gameObject.GetComponent<AudioSource>();
        // why can't this be changed?
        float[] samples = new float[16384];
        source.GetOutputData(samples, 0);
        
        for (int i = 0; i < lengthOfLineRenderer; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(i * 0.5f, samples[i], 0.0f));
        }
    }
    
    private void OnAudioFilterRead(float[] data, int channels)
    {
 
        increment = frequency * 2.0f * Mathf.PI / sampleRate;
        for (int i = 0; i < data.Length - 1; i += channels)
        {

            phase += increment;
 
            // converts a stereo audio sample to mono for the one-channel scope
            if (channels == 2)
            {
                data[i + 1] = data[i];
            }
        }
    }
}
