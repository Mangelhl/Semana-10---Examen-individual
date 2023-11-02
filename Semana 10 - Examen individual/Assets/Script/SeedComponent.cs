using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedComponent : MonoBehaviour, ISeed
{
    private Seed seed;

    public SeedComponent(Seed seed)
    {
        this.seed = seed;
    }

    public void Plant()
    {
        seed.Plant();
    }
}
