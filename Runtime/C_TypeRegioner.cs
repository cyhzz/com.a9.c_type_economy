using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Com.A9.C_TypeEconomy
{
    public class C_TypeRegioner : MonoBehaviour
    {
        [SerializeField]
        UnityEvent OnCHS;
        [SerializeField]
        UnityEvent OnNotCHS;

        void Start()
        {
            Init();
            C_TypeEconomySystem.instance.OnInitializedSucc += Init;
        }

        void Init()
        {
            if (C_TypeEconomySystem.instance.m_StoreController == null)
            {
                return;
            }
            var rg = C_TypeEconomySystem.instance.GetRegion();

            if (rg == "CNY")
            {
                OnCHS?.Invoke();
            }
            else
            {
                OnNotCHS?.Invoke();
            }
        }
    }
}