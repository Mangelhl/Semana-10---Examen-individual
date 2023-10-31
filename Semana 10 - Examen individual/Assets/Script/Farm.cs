using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm 
{
    private static Farm instance;
    public static Farm Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Farm();
            }
            return instance;
        }
    }

    private int numberOfCorrals = 1;

    public int NumberOfCorrals
    {
        get { return numberOfCorrals; }
    }

    public void ExpandCorral()
    {
        numberOfCorrals++;
    }
}
