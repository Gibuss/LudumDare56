using UnityEngine;
using TMPro;

public class TurretStatsDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;        
    [SerializeField] private TMP_Text rangeText;        
    [SerializeField] private TMP_Text atkSpeedText;     
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private TMP_Text costText;


    public void UpdateStats(string turretName, int level, int damage, float range, float interval, int cost)
    {
        levelText.text = $"{turretName} (Lvl. {level})";
        rangeText.text = $"Range | {range}";
        atkSpeedText.text = $"Atk Speed | {1 / interval}";
        damageText.text = $"Dmg | {damage}";
        costText.text = $"{cost}"; 
    }
}
