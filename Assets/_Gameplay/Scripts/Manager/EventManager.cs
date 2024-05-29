using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public event Action OnBuyPant;
    public event Action OnBuyHat;
    public event Action OnBuyKhien;
    public event Action OnBuySetSkin;

    public void BuyPant()
    {
        OnBuyPant?.Invoke();
    }

    public void BuyHat()
    {
        OnBuyHat?.Invoke();
    }

    public void BuyKhien()
    {
        OnBuyKhien?.Invoke();
    }

    public void BuySetSkin()
    {
        OnBuySetSkin?.Invoke();
    }
}

