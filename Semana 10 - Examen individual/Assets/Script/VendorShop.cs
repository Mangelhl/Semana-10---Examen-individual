using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendorShop : MonoBehaviour
{
    public static VendorShop Instance { get; private set; }

    public Button buySeedsButton;
    public Button buyAnimalsButton;

    public GameObject darkPanel;

    private NPCVendor currentVendor;

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

    private void Start()
    {
        buySeedsButton.onClick.AddListener(BuySeeds);
        buyAnimalsButton.onClick.AddListener(BuyAnimals);

        buySeedsButton.gameObject.SetActive(false);
        buyAnimalsButton.gameObject.SetActive(false);
        darkPanel.SetActive(false);
    }

    public void SetCurrentVendor(NPCVendor vendor)
    {
        currentVendor = vendor;
    }

    public void ShowVendorUI(NPCVendor vendor)
    {
        currentVendor = vendor;
        buySeedsButton.gameObject.SetActive(true);
        buyAnimalsButton.gameObject.SetActive(true);
        darkPanel.SetActive(true);
    }

    public void HideVendorUI()
    {
        buySeedsButton.gameObject.SetActive(false);
        buyAnimalsButton.gameObject.SetActive(false);
        darkPanel.SetActive(false);
    }

    private void BuySeeds()
    {        
        if (currentVendor != null)
        {
            currentVendor.SellSeeds();
        }
    }

    private void BuyAnimals()
    {       
        if (currentVendor != null)
        {
            currentVendor.SellAnimals();
        }
    }
}
