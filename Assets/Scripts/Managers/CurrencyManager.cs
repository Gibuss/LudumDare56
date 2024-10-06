using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CurrencyManager : MonoBehaviour
{
    [Header("Initialization")]
    [SerializeField] private TMP_Text displayedCurrency;
    [SerializeField] private int currentCurrency;

    [Header("Ennemies gains")]
    [SerializeField] private int antGain;
    [SerializeField] private int beetleGain;
    [SerializeField] private int ladybugGain;
    [SerializeField] private int termiteGain;

    [Header("Turrets Costs")]
    [SerializeField] private int beeCost;
    [SerializeField] private int bumbeeCost;
    [SerializeField] private int hornetCost;
    [SerializeField] private int waspCost;


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

    public void AddCurrencyOnMobDeath(string type)
    {
        if (type.ToLower().Substring(0, 8) == "enemyant")
        {
            currentCurrency += antGain;
            SetCurrency(currentCurrency);
        } else if (type.ToLower().Substring(0, 11) == "enemybeetle")
        {
            currentCurrency += beetleGain;
            SetCurrency(currentCurrency);
        }
        else if (type.ToLower().Substring(0, 12) == "enemyladybug")
        {
            currentCurrency += ladybugGain;
            SetCurrency(currentCurrency);
        }
        else if (type.ToLower().Substring(0, 12) == "enemytermite")
        {
            currentCurrency += termiteGain;
            SetCurrency(currentCurrency);
        }
    }

    public bool SubstractCurrencyOnTowerPlacement(string type)
    {
        if (type.ToLower().Substring(0, 3) == "bee" && (currentCurrency-beeCost) >= 0)
        {
            currentCurrency -= beeCost;
            SetCurrency(currentCurrency);
            return true;
        }
        else if (type.ToLower().Substring(0, 6) == "bumbee" && (currentCurrency - bumbeeCost) >= 0)
        {
            currentCurrency -= bumbeeCost;
            SetCurrency(currentCurrency);
            return true;
        }
        else if (type.ToLower().Substring(0, 6) == "hornet" && (currentCurrency - hornetCost) >= 0)
        {
            currentCurrency -= hornetCost;
            SetCurrency(currentCurrency);
            return true;
        }
        else if (type.ToLower().Substring(0, 4) == "wasp" && (currentCurrency - waspCost) >= 0)
        {
            currentCurrency -= waspCost;
            SetCurrency(currentCurrency);
            return true;
        } else
        {
            return false;
        }
    }
}
