using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance => instance ?? (instance = new Player());

    private List<Animal> corralAnimals = new List<Animal>();
    private List<Plant> plants = new List<Plant>();

    private Transform playerTransform;
    private IObserver observer;

    public event Action<int> ProgressGrowthComplete;
    public event Action<int> ProgressReproductionComplete;

    public int Money { get; set; }

    public Player()
    {
        observer = new IObserver();
    }

    public int CalculatePricePurchase(int unitPrice, int amount)
    {
        return unitPrice * amount;
    }

    void Start()
    {
        playerTransform = GetComponent<Transform>();
    }

    public void Move(float x, float y)
    {
        Vector3 currentPosition = playerTransform.position;

        float newX = currentPosition.x + x;
        float newY = currentPosition.y + y;

        playerTransform.position = new Vector3(newX, newY, currentPosition.z);
    }
   

    public void BuySeeds(int amount)
    {
        int totalPrice = CalculatePricePurchase(10, amount);
        if (Money >= totalPrice)
        {
            Money -= totalPrice;
            Seed newSeed = new Seed(amount);
            
            observer.NotifyMoney(Money);
            observer.NotifyQuantity(amount);
        }
    }

    public void BuyAnimals(int amount)
    {
        int totalPrice = CalculatePricePurchase(50, amount);
        if (Money >= totalPrice)
        {
            Money -= totalPrice;
            Animal newAnimal = new Animal(1, 50);
           
            observer.NotifyMoney(Money);
            observer.NotifyQuantity(amount);
        }
    }

    public void PlantSeed(Seed seed)
    {
        seed.Plant();
        observer.NotifyQuantity(seed.Count); 
    }

    public void PlaceAnimal(Animal animal)
    {
        bool isCorralSpecial = true;

        if (isCorralSpecial)
        {
            corralAnimals.Add(animal);

            animal.ProgressReproductionComplete += ProgressReproductionComplete;

            if (corralAnimals.Count >= 2)
            {
                StartPlayback();
            }
        }
        else
        {
            Debug.Log("No hay corral especial para animales.");
        }               
    }

    private void StartPlayback()
    {        
        Animal father = corralAnimals[0];
        Animal mother = corralAnimals[1];
                
        Animal newAnimal = new Animal(2, 50);
        corralAnimals.Add(newAnimal);

        observer.NotifyQuantity(1);
    }

    public void CultivatePlant(Plant plant)
    {
        plant.SimulateGrowth();
        plant.ProgressGrowthComplete += ProgressGrowthComplete;

        observer.NotifyProgress(plant.Price);
    }

    public void SellPlant(Plant plant)
    {
        int salePrice = plant.Price;
        Money += salePrice;
        
        observer.NotifyMoney(Money);
        observer.NotifyQuantity(-1); 
    }

    public void SellAnimal(Animal animal)
    {        
        int salePrice = animal.Price;
        Money += salePrice;
       
        observer.NotifyMoney(Money);
        observer.NotifyQuantity(-1); 
    }

    
}
