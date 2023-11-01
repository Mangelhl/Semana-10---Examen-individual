using UnityEngine;
using System;
using System.Collections.Generic;

public class Market : MonoBehaviour
{
    public static Market Instance { get; private set; }

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

    public void SellPlant(Plant plant)
    {
        int salePrice = plant.Price;
        Player.Instance.Money += salePrice;

        Player.Instance.NotifyMoney(Player.Instance.Money);
        Player.Instance.NotifyQuantity(-1);

        Player.Instance.RemovePlant(plant);
    }

    public void SellAnimal(Animal animal)
    {
        int salePrice = animal.Price;
        Player.Instance.Money += salePrice;

        Player.Instance.NotifyMoney(Player.Instance.Money);
        Player.Instance.NotifyQuantity(-1);

        Player.Instance.RemoveAnimal(animal);
    }
}
