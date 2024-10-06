using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text displayedCurrency;
    [SerializeField] private int currentCurrency;
    [SerializeField] private int antCurrency;
    [SerializeField] private int beetleCurrency;
    [SerializeField] private int ladybugCurrency;
    [SerializeField] private int termiteCurrency;


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
            currentCurrency += antCurrency;
            SetCurrency(currentCurrency);
        } else if (type.ToLower().Substring(0, 11) == "enemybeetle")
        {
            currentCurrency += beetleCurrency;
            SetCurrency(currentCurrency);
        }
        else if (type.ToLower().Substring(0, 12) == "enemyladybug")
        {
            currentCurrency += ladybugCurrency;
            SetCurrency(currentCurrency);
        }
        else if (type.ToLower().Substring(0, 12) == "enemytermite")
        {
            currentCurrency += termiteCurrency;
            SetCurrency(currentCurrency);
        }
    }
}
