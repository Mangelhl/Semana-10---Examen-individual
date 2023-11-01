using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCVendor : MonoBehaviour
{
    public event Action OnInteract;
    public int seedPrice = 0;
    public int animalPrice = 0;

    private bool shopShown = false;


    public void SellSeeds()
    {
        Player.Instance.BuySeeds(1);
        HideShop();
    }

    public void SellAnimals()
    {
        Player.Instance.BuyAnimals(1);
        HideShop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !shopShown)
        {
            OnInteract?.Invoke();
            VendorShop.Instance.ShowVendorUI(this);
            shopShown = true;
        }
    }

    public void HideShop()
    {
        shopShown = false;
        VendorShop.Instance.HideVendorUI();
    }
}
