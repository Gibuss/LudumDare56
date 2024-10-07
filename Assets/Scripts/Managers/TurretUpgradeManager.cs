using UnityEngine;

public class TurretUpgradeManager : MonoBehaviour
{
    public string turretName;
    public int currentLevel = 1;

    [SerializeField] private TurretData turretData;
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

        if (turretData == null)
        {
            Debug.LogError("TurretData n'est pas assigné dans l'inspecteur.");
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

        TurretType turretType = turretData.turretTypes.Find(type => type.name == turretName);
        if (turretType != null)
        {
            TurretUpgrade upgrade = turretType.upgrades.Find(upg => upg.level == currentLevel);

            Debug.Log($"Tentative d'amélioration de la tourelle : {turretName}, Niveau actuel : {currentLevel}");

            if (upgrade != null)
            {
                Debug.Log($"Coût de l'amélioration (Niveau {currentLevel + 1}): {upgrade.cost}");

                CurrencyManager currencyManager = FindObjectOfType<CurrencyManager>();
                if (currencyManager != null)
                {
                    Debug.Log($"Monnaie actuelle : {currencyManager.currentCurrency}");

                    if (currencyManager.CanUpgrade(upgrade.cost))
                    {
                        StartCoroutine(PerformUpgrade());
                        Debug.Log("Amélioration réussie.");
                        return true;
                    }
                    else
                    {
                        Debug.Log("Pas assez de monnaie pour effectuer l'amélioration.");
                        return false;
                    }
                }
                else
                {
                    Debug.LogError("CurrencyManager non trouvé dans la scène.");
                    return false;
                }
            }
            else
            {
                Debug.Log("Pas d'amélioration disponible.");
                return false;
            }
        }
        else
        {
            Debug.LogError("Type de tourelle non trouvé.");
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
        TurretType turretType = turretData.turretTypes.Find(type => type.name == turretName);

        if (turretType != null)
        {
            TurretUpgrade upgrade = turretType.upgrades.Find(upg => upg.level == level);

            if (upgrade != null)
            {
                targetingRange = upgrade.targetingRange;
                damage = (int)upgrade.damage;
                damageInterval = upgrade.damageInterval;
                int cost = upgrade.cost;

                Debug.Log($"Amélioration trouvée : Portée = {targetingRange}, Dégâts = {damage}, Intervalle = {damageInterval}, Coût = {cost}");
                turret.SetStats(targetingRange, damage, damageInterval);

                if (statsDisplay != null)
                {
                    statsDisplay.UpdateStats(turretName, currentLevel, damage, targetingRange, damageInterval, cost);
                }
            }
            else
            {
                Debug.LogWarning($"Aucune amélioration trouvée pour la tourelle '{turretName}' niveau {level}.");
            }
        }
        else
        {
            Debug.LogWarning($"Aucun type de tourelle trouvé avec le nom '{turretName}'.");
        }
    }
}
