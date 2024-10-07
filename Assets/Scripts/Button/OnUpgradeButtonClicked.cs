using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private TurretUpgradeManager selectedTurretUpgradeManager;

    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Button upgradeButton;

    private void Start()
    {
        upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
    }

    public void OnUpgradeButtonClicked()
    {
        if (selectedTurretUpgradeManager != null)
        {
            selectedTurretUpgradeManager.UpgradeTurret();
            UpdateButtonText();
        }
    }

    private void UpdateButtonText()
    {
        if (selectedTurretUpgradeManager != null)
        {
            int currentLevel = selectedTurretUpgradeManager.currentLevel;
            buttonText.text = $"Upgrade to Level {currentLevel + 1}";
        }
    }

    public void SetSelectedTurret(TurretUpgradeManager newSelectedTurret)
    {
        selectedTurretUpgradeManager = newSelectedTurret;
        UpdateButtonText();
    }
}
