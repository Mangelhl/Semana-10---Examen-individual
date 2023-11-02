using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeed : MonoBehaviour
{
    public static PlantSeed Instance { get; private set; }
    private bool planted = false;

    public GameObject seedPrefab;
    
    float yOffset = 0.1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!planted && Input.GetKeyDown(KeyCode.Alpha1) && Player.Instance.HasSeed)
        {
            GameObject newSeedObject = Instantiate(seedPrefab);
            Seed newSeed = newSeedObject.GetComponent<Seed>();

            if (newSeed != null)
            {
                newSeedObject.transform.position = transform.position + transform.up * yOffset;
                Player.Instance.PlantSeed(newSeed);
                planted = true;
                Player.Instance.HasSeed = false;
            }
        }
    }

    public void ReceiveSeed(Seed seed)
    {
        if (!IsOccupied())
        {
            // Realizar la plantación de la semilla aquí
        }
        else
        {
            Debug.Log("El área de plantación está ocupada.");
        }
    }

    private bool IsOccupied()
    {
        return planted;
    }
}
