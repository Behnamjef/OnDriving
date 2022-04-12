using System;
using System.Collections.Generic;
using MagicOwl;
using UnityEngine;

namespace DefaultNamespace
{
    public class UnlockPanel : CommonUIBehaviour
    {
        [SerializeField] private List<ShopCar> ShopCars;

        private void Start()
        {
            var carType = UnlockManager.Instance.GetUnlockCarOnThisLevel();
            var car = ShopCars.Find(c => c.CarType == carType);
            car.SetActive(true);
        }
    }
}