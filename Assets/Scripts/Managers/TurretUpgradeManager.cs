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
    private bool isUpgrading = false;

    [SerializeField] private TurretStatsDisplay statsDisplay;

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

    public bool UpgradeTurret()
    {
        if (isUpgrading)
        {
            Debug.Log("Une amélioration est déjà en cours.");
            return false;
        }

        if (currentLevel < 3)
        {
            StartCoroutine(PerformUpgrade());
            return true;
        }
        else
        {
            Debug.Log("Niveau maximum atteint. Impossible d'améliorer");
            return false;
        }
    }

    private System.Collections.IEnumerator PerformUpgrade()
    {
        isUpgrading = true;
        currentLevel++;
        Debug.Log($"Amélioration de la tourelle. Nouveau niveau : {currentLevel}");
        ApplyUpgrade(currentLevel);

        yield return new WaitForSeconds(1.5f);

        isUpgrading = false;
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

            TurretStatsDisplay statsDisplay = FindObjectOfType<TurretStatsDisplay>();
            if (statsDisplay != null)
            {
                statsDisplay.UpdateStats(turretName, currentLevel, damage, targetingRange, damageInterval);
            }
        }
        else
        {
            Debug.LogWarning($"Aucune amélioration trouvée pour la tourelle '{turretName}' niveau {level}.");
        }
    }
}
