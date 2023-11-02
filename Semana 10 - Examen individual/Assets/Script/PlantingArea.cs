using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantingArea : MonoBehaviour
{
    public GameObject plantPrefab;  
    private GameObject plant;
    private int growthProgress = 0;
    public int maxGrowthProgress = 5;
    public Image progressBar;

    public bool IsOccupied()
    {
        return plant != null;
    }
   
    public void IncrementGrowthProgress()
    {
        if (growthProgress < maxGrowthProgress)
        {
            growthProgress++;

            float fillAmount = (float)growthProgress / maxGrowthProgress;
            progressBar.fillAmount = 0.5f;
        }
        else
        {            
            Player.Instance.CultivatePlant(plant.GetComponent<Plant>());
            Destroy(plant); 
            plant = null;
            growthProgress = 0; 
        }
    }
}
