using UnityEngine;

public class TurretUpgradeManager : MonoBehaviour
{
    public string turretName;
    public int currentLevel = 1;
    private TurretData turretData;

    private float targetingRange;
    private int damage;
    private float damageInterval;

    private Turret turret;

    private void Start()
    {
        turret = GetComponent<Turret>();
        turretData = Resources.Load<TurretData>("TurretData");

        ApplyUpgrade(currentLevel);
    }

    private void Update()
    {
    }

    public void UpgradeTurret()
    {
        if (currentLevel < 3)
        {
            currentLevel++;
            ApplyUpgrade(currentLevel);
        }
    }

    private void ApplyUpgrade(int level)
    {
        TurretUpgrade upgrade = turretData.GetUpgrade(turretName, level);

        if (upgrade != null)
        {
            targetingRange = upgrade.targetingRange;
            damage = (int)upgrade.damage;
            damageInterval = upgrade.damageInterval;

            turret.SetStats(targetingRange, damage, damageInterval);
        }
    }
}
