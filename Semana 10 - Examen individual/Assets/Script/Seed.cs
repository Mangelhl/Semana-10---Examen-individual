using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Plant
{
    public int Count { get; private set; }

    public Seed(int count)
    {
        Count = count;
    }

    public void Plant()
    {
        Count--;
    }
}
