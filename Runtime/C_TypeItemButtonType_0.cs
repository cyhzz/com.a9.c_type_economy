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
        Init();
        C_TypeEconomySystem.instance.OnInitializedSucc += Init;
    }

    void OnDestroy()
    {
        if (C_TypeEconomySystem.instance == null)
        {
            return;
        }
        C_TypeEconomySystem.instance.OnInitializedSucc -= Init;
    }

    void Init()
    {
        if (C_TypeEconomySystem.instance.m_StoreController == null)
        {
            if (C_TypeEconomySystem.instance.error_log)
            {
                Debug.LogError("c_TypeEconomySystem.m_StoreController is null");
            }
            else
            {
                Debug.Log("c_TypeEconomySystem.m_StoreController is null");
            }
            return;
        }

        var product = C_TypeEconomySystem.instance.m_StoreController.products.WithID(id);
        if (product == null)
        {
            if (C_TypeEconomySystem.instance.error_log)
            {
                Debug.LogError("Product is null and disable button");
            }
            else
            {
                Debug.Log("Product is null and disable button");
            }
            button.interactable = false;
            return;
        }

        var local = C_TypeEconomySystem.instance.GetLocalItemWithID(id);

        if (local == null)
        {
            if (C_TypeEconomySystem.instance.error_log)
            {
                Debug.LogError("Local is null and disable button");
            }
            else
            {
                Debug.Log("Local is null and disable button");
            }
            button.interactable = false;
            return;
        }

        if (product.hasReceipt && local.CanPurchase() == false)
        {
            if (C_TypeEconomySystem.instance.error_log)
            {
                Debug.LogError("Product has receipt and disable button");
            }
            else
            {
                Debug.Log("Product has receipt and disable button");
            }
            return;
        }
        if (C_TypeEconomySystem.instance.error_log)
        {
            Debug.LogError("Product no receipt and enable button");
        }
        else
        {
            Debug.Log("Product no receipt and enable button");
        }

        OnCanPurchased?.Invoke();
        button.onClick.AddListener(() =>
        {
            local.TryPurshase();
        });
    }
}
