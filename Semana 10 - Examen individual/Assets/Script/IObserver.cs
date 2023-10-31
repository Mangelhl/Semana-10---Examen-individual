using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IObserver 
{
    public event Action<int> UpdateProgress;
    public event Action<int> UpdateQuantity;
    public event Action<int> UpdateMoney;

    public void NotifyProgress(int progress)
    {
        UpdateProgress?.Invoke(progress);
    }

    public void NotifyQuantity(int quantity)
    {
        UpdateQuantity?.Invoke(quantity);
    }

    public void NotifyMoney(int money)
    {
        UpdateMoney?.Invoke(money);
    }
}
