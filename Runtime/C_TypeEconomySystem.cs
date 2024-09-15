using System;
using System.Collections.Generic;
using System.Linq;
using Com.A9.Singleton;
using UnityEditor;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;

namespace Com.A9.C_TypeEconomy
{
    public class C_TypeEconomySystem : Singleton<C_TypeEconomySystem>, IStoreListener
    {
        public IStoreController m_StoreController; // The Unity Purchasing system.
        List<IC_TypeItem> c_TypeItems = new List<IC_TypeItem>();

        void Start()
        {
            c_TypeItems = GetComponentsInChildren<IC_TypeItem>().ToList();
            InitializePurchasing();
        }

        public string GetRegion()
        {
            if (c_TypeItems.Count == 0 || c_TypeItems == null)
            {
                return "NA";
            }
            var product = C_TypeEconomySystem.instance.m_StoreController.products.WithID(c_TypeItems[0].GetID());
            if (product == null)
            {
                Debug.Log("Product in Region is null");
                return "NA";
            }
            return product.metadata.isoCurrencyCode;
        }

        void InitializePurchasing()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            c_TypeItems.ForEach(item =>
            {
                builder.AddProduct(item.GetID(), item.PurchaseType());
            });

            UnityPurchasing.Initialize(this, builder);
        }

        public void BuyProduct(string pruductid)
        {
            m_StoreController.InitiatePurchase(m_StoreController.products.WithID(pruductid));
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("In-App Purchasing successfully initialized");
            m_StoreController = controller;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            OnInitializeFailed(error, null);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";

            if (message != null)
            {
                errorMessage += $" More details: {message}";
            }

            Debug.Log(errorMessage);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            //Retrieve the purchased product
            var product = args.purchasedProduct;

            //Add the purchased product to the players inventory
            //付款成功，通知服务器发货
            //此处需要自行添加逻辑，通知自己的服务器发货，我这边就省略了。
            /*
             ***
             ***
             ***
             */
            c_TypeItems.Find(item => item.GetID() == product.definition.id).OnPurchaseSuccess();
            Debug.Log($"Purchase Complete - Product: {product.definition.id}");

            //We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            //付款失败
            Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            //付款失败
            Debug.Log($"Purchase failed - Product: '{product.definition.id}'," +
                $" Purchase failure reason: {failureDescription.reason}," +
                $" Purchase failure details: {failureDescription.message}");
        }

    }
}
