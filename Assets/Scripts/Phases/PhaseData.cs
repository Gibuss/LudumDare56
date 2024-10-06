using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PhaseData
{
    //simple array pour stocker les etapes dans une phase
    //on pourra donc facilement créer no phases et nos étapes directement dans l'éditeur en créant une database de phase sur unity
    //pour rappel, il faut écrire un nom d'insecte pour le faire sapwn, ou bien un float pour attendre une durée
    [SerializeField] private string[] steps;

    public string[] GetSteps() { return steps; }
}


