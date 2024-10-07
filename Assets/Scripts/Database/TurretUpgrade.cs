using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretUpgrade
{
    public int level;
    public float targetingRange;
    public float damage;
    public float damageInterval;
}

[System.Serializable]
public class TurretType
{
    public string name;
    public GameObject prefab;
    public List<TurretUpgrade> upgrades;
}

[CreateAssetMenu(fileName = "TurretData", menuName = "ScriptableObjects/TurretData", order = 1)]
public class TurretData : ScriptableObject
{
    public List<TurretType> turretTypes;

    public TurretUpgrade GetUpgrade(string turretName, int level)
    {
        TurretType turretType = turretTypes.Find(type => type.name == turretName);
        if (turretType != null)
        {
            return turretType.upgrades.Find(upgrade => upgrade.level == level);
        }
        return null;
    }
}
