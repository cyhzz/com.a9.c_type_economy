using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Com.A9.C_TypeEconomy
{
    public class C_TypeItem_IOS : MonoBehaviour, IC_TypeItem
    {
        public string id;
        public ProductType type;
        public event Action OnPurchaseSucceed;

        public string GetID()
        {
            return id;
        }

        public ProductType PurchaseType()
        {
            return type;
        }

        public bool CanPurchase()
        {
            return !Purchased();
        }

        public float GetPrice()
        {
            return (float)C_TypeEconomySystem.instance.m_StoreController.products.WithID(id).metadata.localizedPrice;
        }

        public bool Purchased()
        {
            if (type == ProductType.NonConsumable)
                return C_TypeEconomySystem.instance.m_StoreController.products.WithID(id).hasReceipt;
            return false;
        }

        public void TryPurshase()
        {
            C_TypeEconomySystem.instance.BuyProduct(id);
        }

        public void OnPurchaseSuccess()
        {
            OnPurchaseSucceed?.Invoke();
        }
    }
}