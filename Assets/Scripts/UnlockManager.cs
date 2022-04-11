using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using MagicOwl;
using UnityEngine;

public class UnlockManager : SingletonBehaviour<UnlockManager>
{
    public CarType FirstCarToBeUnlock;
    public List<UnlockableCarInfo> PurchasableCarInfos;

    private void Awake()
    {
        UnlockCar(FirstCarToBeUnlock);
        if(!PlayerPrefs.HasKey("SelectedCar"))
            SelectCar(FirstCarToBeUnlock);
    }

    public bool IsCarUnlock(CarType carType)
    {
        return PlayerPrefs.GetInt($"{carType}_Unlocked", 0) == 1;
    }
    
    public UnlockableCarInfo GetCar(CarType carType)
    {
        return PurchasableCarInfos.FirstOrDefault(c => c.CarType == carType);
    }

    public void UnlockCar(CarType carType)
    {
        PlayerPrefs.SetInt($"{carType}_Unlocked", 1);
    }
    
    public CarType GetSelectedCarType()
    {
        var selectedCarName =
            PlayerPrefs.GetString("SelectedCar", UnlockManager.Instance.FirstCarToBeUnlock.ToString());
        return (CarType)Enum.Parse(typeof(CarType), selectedCarName);
    }
    
    public void SelectCar(CarType carType)
    {
        PlayerPrefs.SetString($"SelectedCar", carType.ToString());
    }
}

[Serializable]
public class UnlockableCarInfo
{
    public CarType CarType;
    public int UnlockPrice;
    public int UnlockLevel;
}