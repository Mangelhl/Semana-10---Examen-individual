using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public PlantSeed plantSeed;
    private static Player instance;
    public static Player Instance => instance;

    private List<Animal> corralAnimals = new List<Animal>();
    private List<Plant> plants = new List<Plant>();

    private Transform playerTransform;
    private IObserver observer;

    public event Action<int> ProgressGrowthComplete;
    public event Action<int> ProgressReproductionComplete;

    public int Money { get; set; }

    public float moveSpeed = 5.0f;

    public int maxPlantCount = 10; 
    public int maxCorralAnimalCount = 10;

    public GameObject seedPrefab;

    private NPCVendor currentVendor;

    private bool hasSeed = false;
    private bool hasPurchasedSeeds = false;

    public bool HasSeed
    {
        get { return hasSeed; }
        set
        {            
            if (hasPurchasedSeeds)
            {
                hasSeed = value;
            }
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            observer = new IObserver();
        }        
    }

    public int CalculatePricePurchase(int unitPrice, int amount)
    {
        return unitPrice * amount;
    }

    private void Start()
    {
        playerTransform = transform;
    }

    private void Update()
    {        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);

        playerTransform.Translate(movement * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E) && currentVendor != null)
        {
            OpenVendorShop();
        }

       if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (hasPurchasedSeeds && !hasSeed && plants.Count < maxPlantCount)
            {
                Seed newSeed = Instantiate(seedPrefab).GetComponent<Seed>();
                PlantSeed(newSeed);
            }
            else
            {
                Debug.Log("No puedes plantar en este momento.");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {            
            if (plants.Count > 0)
            {
                CultivatePlant(plants[0]);
            }
            else
            {
                Debug.Log("No hay plantas para cultivar.");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {            
            if (corralAnimals.Count < maxCorralAnimalCount)
            {
                Animal newAnimal = new Animal(1, 50);
                PlaceAnimal(newAnimal);
            }
            else
            {
                Debug.Log("El corral de animales está lleno.");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GoToMarket();
        }
    }

    private void OpenVendorShop()
    {

        VendorShop.Instance.SetCurrentVendor(currentVendor);
    }

    public void SetCurrentVendor(NPCVendor vendor)
    {
        currentVendor = vendor;
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
            hasPurchasedSeeds = true;
            hasSeed = true;            
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
        if(plants.Count < maxPlantCount)
        {
            plants.Add(seed);
            seed.Plant();
            observer.NotifyQuantity(seed.Count);
            plantSeed.ReceiveSeed(seed);
        }
        else
        {
            Debug.Log("No hay espacio para más plantas.");
        }
    }

    public void PlaceAnimal(Animal animal)
    {
        bool isCorralSpecial = true;

        if (isCorralSpecial)
        {
            if (corralAnimals.Count < maxCorralAnimalCount)
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
                Debug.Log("El corral de animales está lleno.");
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

    private void GoToMarket()
    {
        float distanceToMarket = Vector3.Distance(playerTransform.position, Market.Instance.transform.position);
        if (distanceToMarket < 2.0f)
        {
            if (plants.Count > 0)
            {
                SellPlant(plants[0]);
            }

            if (corralAnimals.Count > 0)
            {
                SellAnimal(corralAnimals[0]);
            }

            int newQuantity = 1; 
            int newAmountOfMoney = 10;
            int newProgress = 5;
            

            Sell(newAmountOfMoney, newQuantity, newProgress);
        }
        else
        {
            Debug.Log("Estás demasiado lejos del mercado para vender.");
        }
    }
    private void Sell(int saleprice, int quantitySold, int newProgress)
    {
        Money += saleprice * quantitySold;
        observer.NotifyMoney(Money);
        observer.NotifyQuantity(-quantitySold);
    }  

    public void SellPlant(Plant plant)
    {
        int salePrice = plant.Price;
        Money += salePrice;

        observer.NotifyMoney(Money);
        observer.NotifyQuantity(-1);

        plants.Remove(plant);
    }

    public void SellAnimal(Animal animal)
    {
        int salePrice = animal.Price;
        Money += salePrice;

        observer.NotifyMoney(Money);
        observer.NotifyQuantity(-1);

        corralAnimals.Remove(animal);
    }
    public void NotifyMoney(int money)
    {
        observer.NotifyMoney(money);
    }

    public void NotifyQuantity(int quantity)
    {
        observer.NotifyQuantity(quantity);
    }

    public void RemovePlant(Plant plant)
    {
        plants.Remove(plant);
    }

    public void RemoveAnimal(Animal animal)
    {
        corralAnimals.Remove(animal);
    }
}