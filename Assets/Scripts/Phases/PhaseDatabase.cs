using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPhaseDatabase", menuName = "Database/Phase", order = 0)]

public class PhaseDatabase : ScriptableObject
{
    //Une database très simple, qui stock un array de PhaseData, dont chaque PhaseData stock un arrray de strings
    //La PhaseDatabase représente le niveau entier avec toutes ses phases, les PhaseData représentent une phase, et les string représentent une action dans la phase
    [SerializeField] private PhaseData[] phases;

    public PhaseData[] GetPhases() { return phases; }
}
