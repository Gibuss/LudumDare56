using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretType
{
    public string name;
    public GameObject prefab;
    public List<TurretUpgrade> upgrades;
}
