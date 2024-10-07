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
        float atkSpeed = 1 / interval;
        atkSpeed = Mathf.Round(atkSpeed * 10.0f) * 0.1f;
        atkSpeedText.text = $"Atk Speed | {atkSpeed}";
        damageText.text = $"Dmg | {damage}";
        costText.text = $"{cost}"; 
    }
}
