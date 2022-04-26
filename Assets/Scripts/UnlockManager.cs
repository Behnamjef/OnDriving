using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using MagicOwl;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnlockManager : SingletonBehaviour<UnlockManager>
{
    public CarType FirstCarToBeUnlock;
    public List<UnlockableCarInfo> PurchasableCarInfos;
    public List<CarType> UnlockCarOrder;

    public override void Init()
    {
        base.Init();
        UnlockCar(FirstCarToBeUnlock);
        if (!PlayerPrefs.HasKey("SelectedCar"))
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

    public CarType GetUnlockCar()
    {
        if (!IsCarUnlock(CarType.Excavator1))
            return CarType.Excavator1;
        if (!IsCarUnlock(CarType.Excavator2))
            return CarType.Excavator2;
        if (!IsCarUnlock(CarType.Excavator3))
            return CarType.Excavator3;
        var lockedCars = UnlockCarOrder.Where(c => !IsCarUnlock(c)).ToArray();
        return lockedCars[Random.Range(0,lockedCars.Length)];
    }
    
    private bool IsThisLastLevel()
    {
        int Level = PlayerPrefs.GetInt("Level", 1);
        var btnLevel = PlayerPrefs.GetInt("BtnLevel", 0);
        return Level-1 == btnLevel;
    }

    public bool CanUnlockCar()
    {
        return IsThisLastLevel() && UnlockCarOrder.Exists(c => !IsCarUnlock(c));
    }
}

[Serializable]
public class UnlockableCarInfo
{
    public CarType CarType;
    public int UnlockPrice;
}