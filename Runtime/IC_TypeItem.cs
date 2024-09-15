using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Com.A9.C_TypeEconomy
{
    public interface IC_TypeItem
    {
        public string GetID();
        public ProductType PurchaseType();
        public float GetPrice();

        public bool CanPurchase();
        public void TryPurshase();
        public bool Purchased();
        public event Action OnPurchaseSucceed;
        public void OnPurchaseSuccess();
    }
}