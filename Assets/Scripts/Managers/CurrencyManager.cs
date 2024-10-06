using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text displayedCurrency;
    [SerializeField] private int currentCurrency;

   
    void Start()
    {
        SetCurrency(currentCurrency);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrency(int newCurrency) {
        displayedCurrency.text = string.Format("{0}", newCurrency);
    }

    public void AddCurrencyOnDeath(string type)
    {
        if (type.ToLower().Substring(0, 8) == "enemyant")
        {
            currentCurrency += 20;
            SetCurrency(currentCurrency);
        }
    }
}
