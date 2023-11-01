using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeed : MonoBehaviour
{
    public GameObject seedPrefab;

    public GameObject PlantingArea;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Player.Instance != null)
            {
                GameObject newSeedObject = Instantiate(seedPrefab); // Instanciar como GameObject
                Seed newSeed = newSeedObject.GetComponent<Seed>(); // Obtener el componente Seed

                if (newSeed != null)
                {
                    Player.Instance.PlantSeed(newSeed);
                    newSeedObject.transform.position = PlantingArea.transform.position + Vector3.up;
                }
            }
        }
    }
    
}
