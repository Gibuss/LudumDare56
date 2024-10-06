using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPhaseDatabase", menuName = "Database/Phase", order = 0)]

public class PhaseDatabase : ScriptableObject
{
    //Une database tr�s simple, qui stock un array de PhaseData, dont chaque PhaseData stock un arrray de strings
    //La PhaseDatabase repr�sente le niveau entier avec toutes ses phases, les PhaseData repr�sentent une phase, et les string repr�sentent une action dans la phase
    [SerializeField] private PhaseData[] phases;

    public PhaseData[] GetPhases() { return phases; }
}
