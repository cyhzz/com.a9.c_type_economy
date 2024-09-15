using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Com.A9.C_TypeEconomy;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class C_TypeItemButtonType_0 : MonoBehaviour
{
    [SerializeField]
    Button button;
    [SerializeField]
    string id;
    [SerializeField]
    UnityEvent OnCanPurchased;

    void Start()
    {
        var product = C_TypeEconomySystem.instance.m_StoreController.products.WithID(id);
        if (product == null)
        {
            Debug.Log("Product is null and disable button");
            button.interactable = false;
        }

        var local = C_TypeEconomySystem.instance.GetLocalItemWithID(id);

        if (local == null)
        {
            Debug.Log("Local is null and disable button");
            button.interactable = false;
        }

        if (product.hasReceipt && local.CanPurchase() == false)
        {
            Debug.Log("Product has receipt and disable button");
            return;
        }

        OnCanPurchased?.Invoke();
        button.onClick.AddListener(() =>
        {
            local.TryPurshase();
        });
    }
}
