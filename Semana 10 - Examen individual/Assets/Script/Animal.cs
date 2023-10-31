using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Animal : IGrowth
{
    public event Action<int> ProgressReproductionComplete;

    private int progress = 0;
    private int id;

    public Animal(int id, int price)
    {
        this.id = id;
        Price = price;
    }

    public int Price { get; set; }

    public void Growth()
    {
        if (progress == 100)
        {
            ProgressReproductionComplete?.Invoke(id);
        }
    }

    public void SimulateGrowth()
    {
        progress += 10;
        Growth();
    }
}
