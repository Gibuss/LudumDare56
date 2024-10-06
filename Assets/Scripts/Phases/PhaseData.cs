using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PhaseData
{
    //simple array pour stocker les etapes dans une phase
    //on pourra donc facilement cr�er no phases et nos �tapes directement dans l'�diteur en cr�ant une database de phase sur unity
    //pour rappel, il faut �crire un nom d'insecte pour le faire sapwn, ou bien un float pour attendre une dur�e
    [SerializeField] private string[] steps;

    public string[] GetSteps() { return steps; }
}


