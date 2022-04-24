using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using MagicOwl;
using UnityEngine;

public class UnlockPanel : CommonUIBehaviour
{
    public Transform Cars;

    private List<ShopCar> ShopCars
    {
        get
        {
            if (_shopCars.IsNullOrEmpty())
                _shopCars = Cars.GetComponentsInChildren<ShopCar>(true).ToList();
            return _shopCars;
        }
    }
    private List<ShopCar> _shopCars;
    
    private CarType _carToUnlock;

    private void Start()
    {
        _carToUnlock = UnlockManager.Instance.GetUnlockCar();
        var car = ShopCars.Find(c => c.CarType == _carToUnlock);
        car.SetActive(true);
    }

    public void ClaimTheCar()
    {
        AdManager.Instance.OnRewardAdComplete = UnlockCar;
        AdManager.Instance.ShowRewardAd();
    }

    private void UnlockCar()
    {
        UnlockManager.Instance.UnlockCar(_carToUnlock);
        UnlockManager.Instance.SelectCar(_carToUnlock);
        InputManager.Instace.NextLevel();
    }
}