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
            Debug.LogError("Le composant pas trouv�");
            return;
        }

        if (turretData == null)
        {
            Debug.LogError("TurretData n'est pas assign� dans l'inspecteur.");
            return;
        }

        Debug.Log($"Nom de la tourelle : {turretName}, Niveau actuel : {currentLevel}");
        ApplyUpgrade(currentLevel);
    }

    public bool UpgradeTurret()
    {
        if (isUpgrading)
        {
            Debug.Log("Une am�lioration est d�j� en cours.");
            return false;
        }

        TurretType turretType = turretData.turretTypes.Find(type => type.name == turretName);
        if (turretType != null)
        {
            TurretUpgrade upgrade = turretType.upgrades.Find(upg => upg.level == currentLevel);

            Debug.Log($"Tentative d'am�lioration de la tourelle : {turretName}, Niveau actuel : {currentLevel}");

            if (upgrade != null)
            {
                Debug.Log($"Co�t de l'am�lioration (Niveau {currentLevel + 1}): {upgrade.cost}");

                CurrencyManager currencyManager = FindObjectOfType<CurrencyManager>();
                if (currencyManager != null)
                {
                    Debug.Log($"Monnaie actuelle : {currencyManager.currentCurrency}");

                    if (currencyManager.CanUpgrade(upgrade.cost))
                    {
                        StartCoroutine(PerformUpgrade());
                        Debug.Log("Am�lioration r�ussie.");
                        return true;
                    }
                    else
                    {
                        Debug.Log("Pas assez de monnaie pour effectuer l'am�lioration.");
                        return false;
                    }
                }
                else
                {
                    Debug.LogError("CurrencyManager non trouv� dans la sc�ne.");
                    return false;
                }
            }
            else
            {
                Debug.Log("Pas d'am�lioration disponible.");
                return false;
            }
        }
        else
        {
            Debug.LogError("Type de tourelle non trouv�.");
            return false;
        }
    }

    private System.Collections.IEnumerator PerformUpgrade()
    {
        isUpgrading = true;
        currentLevel++;
        Debug.Log($"Am�lioration de la tourelle. Nouveau niveau : {currentLevel}");
        ApplyUpgrade(currentLevel);

        yield return new WaitForSeconds(1.5f);

        isUpgrading = false;
    }

    private void ApplyUpgrade(int level)
    {
        Debug.Log($"Application de l'am�lioration pour la tourelle '{turretName}' au niveau {level}");
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

                Debug.Log($"Am�lioration trouv�e : Port�e = {targetingRange}, D�g�ts = {damage}, Intervalle = {damageInterval}, Co�t = {cost}");
                turret.SetStats(targetingRange, damage, damageInterval);

                if (statsDisplay != null)
                {
                    statsDisplay.UpdateStats(turretName, currentLevel, damage, targetingRange, damageInterval, cost);
                }
            }
            else
            {
                Debug.LogWarning($"Aucune am�lioration trouv�e pour la tourelle '{turretName}' niveau {level}.");
            }
        }
        else
        {
            Debug.LogWarning($"Aucun type de tourelle trouv� avec le nom '{turretName}'.");
        }
    }
}
