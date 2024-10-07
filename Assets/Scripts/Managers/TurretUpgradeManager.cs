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
        if (turret == null)
        {
            Debug.LogError("Le composant pas trouvé");
            return;
        }

        turretData = Resources.Load<TurretData>("TurretData");
        if (turretData == null)
        {
            Debug.LogError("TurretData introuvable dans Resources.");
            return;
        }

        Debug.Log($"Nom de la tourelle : {turretName}, Niveau actuel : {currentLevel}");

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
            Debug.Log($"Amélioration de la tourelle. Nouveau niveau : {currentLevel}");
            ApplyUpgrade(currentLevel);
        }
        else
        {
            Debug.Log("Niveau maximum atteint. Impossible d'améliorer");
        }
    }

    private void ApplyUpgrade(int level)
    {
        Debug.Log($"Application de l'amélioration pour la tourelle '{turretName}' au niveau {level}");
        TurretUpgrade upgrade = turretData.GetUpgrade(turretName, level);

        if (upgrade != null)
        {
            targetingRange = upgrade.targetingRange;
            damage = (int)upgrade.damage;
            damageInterval = upgrade.damageInterval;

            Debug.Log($"Amélioration trouvée : Portée = {targetingRange}, Dégâts = {damage}, Intervalle = {damageInterval}");
            turret.SetStats(targetingRange, damage, damageInterval);
        }
        else
        {
            Debug.LogWarning($"Aucune amélioration trouvée pour la tourelle '{turretName}' niveau {level}.");
        }
    }
}
