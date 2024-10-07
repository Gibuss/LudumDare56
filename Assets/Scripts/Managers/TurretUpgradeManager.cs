using UnityEngine;

public class TurretUpgradeManager : MonoBehaviour
{
    public string turretName;
    public int currentLevel = 1;

    [SerializeField] private TurretData turretData; // Référence dans l'inspecteur

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
        TurretType turretType = turretData.turretTypes.Find(type => type.name == turretName); // Cherchez le type de tourelle

        if (turretType != null)
        {
            TurretUpgrade upgrade = turretType.upgrades.Find(upg => upg.level == level); // Récupérez l'amélioration

            if (upgrade != null)
            {
                targetingRange = upgrade.targetingRange;
                damage = (int)upgrade.damage;
                damageInterval = upgrade.damageInterval;

                Debug.Log($"Amélioration trouvée : Portée = {targetingRange}, Dégâts = {damage}, Intervalle = {damageInterval}");
                turret.SetStats(targetingRange, damage, damageInterval);

                // Mettez à jour les statistiques à afficher
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
        else
        {
            Debug.LogWarning($"Aucun type de tourelle trouvé avec le nom '{turretName}'.");
        }
    }

}
