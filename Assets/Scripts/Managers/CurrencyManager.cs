using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text displayedCurrency;
    [SerializeField] private int startingCurrency;

   
    void Start()
    {
        displayedCurrency.text = string.Format("{0}", startingCurrency);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
