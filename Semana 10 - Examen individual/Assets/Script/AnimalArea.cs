using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalArea : MonoBehaviour
{
    private List<Animal> animalsInArea = new List<Animal>(); 
    public bool IsFull()
    {
        return animalsInArea.Count >= maxAnimalCapacity;
    }

    public void AddAnimal(Animal animal)
    {
        if (!IsFull())
        {
            animalsInArea.Add(animal);
            
            animal.transform.position = transform.position;
        }
        else
        {
            Debug.Log("El área de animales está llena.");
        }
    }

    public void RemoveAnimal(Animal animal)
    {
        animalsInArea.Remove(animal);
    }
}