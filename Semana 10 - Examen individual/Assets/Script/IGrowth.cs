using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrowth
{
    void Growth();
}

public abstract class Sale
{
    public int Price { get; set; }
    public abstract void Sell();
}