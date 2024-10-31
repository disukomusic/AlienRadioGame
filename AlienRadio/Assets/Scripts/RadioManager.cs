using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RadioManager : MonoBehaviour
{
    public static RadioManager Instance;
    public RadioChannel[] channels;
    
    private void Start()
    {
        Instance = this;
    }
}