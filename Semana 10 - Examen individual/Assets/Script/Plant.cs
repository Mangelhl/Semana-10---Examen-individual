using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Plant : IGrowth
{

    public event Action<int> ProgressGrowthComplete;
    public int Price { get; set; }

    private int progress = 0;          

    public void Growth()
    {
        if (progress == 100)
        {
            ProgressGrowthComplete?.Invoke(Price);
        }
    }
    
    public void SimulateGrowth()
    {        
        progress += 10; 
        Growth();
    }
}
