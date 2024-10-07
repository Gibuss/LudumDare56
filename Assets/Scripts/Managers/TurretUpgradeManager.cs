using UnityEngine;

public class TurretUpgradeManager : MonoBehaviour
{
    public string turretName;
    public int currentLevel = 1;

    [SerializeField] private TurretData turretData; // R�f�rence dans l'inspecteur

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

        if (currentLevel < 3)
        {
            StartCoroutine(PerformUpgrade());
            return true;
        }
        else
        {
            Debug.Log("Niveau maximum atteint. Impossible d'am�liorer");
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
        TurretType turretType = turretData.turretTypes.Find(type => type.name == turretName); // Cherchez le type de tourelle

        if (turretType != null)
        {
            TurretUpgrade upgrade = turretType.upgrades.Find(upg => upg.level == level); // R�cup�rez l'am�lioration

            if (upgrade != null)
            {
                targetingRange = upgrade.targetingRange;
                damage = (int)upgrade.damage;
                damageInterval = upgrade.damageInterval;

                Debug.Log($"Am�lioration trouv�e : Port�e = {targetingRange}, D�g�ts = {damage}, Intervalle = {damageInterval}");
                turret.SetStats(targetingRange, damage, damageInterval);

                // Mettez � jour les statistiques � afficher
                if (statsDisplay != null)
                {
                    statsDisplay.UpdateStats(turretName, currentLevel, damage, targetingRange, damageInterval);
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
