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
        UpdateButtonText();
    }

    public void OnUpgradeButtonClicked()
    {
        if (selectedTurretUpgradeManager != null)
        {
            upgradeButton.interactable = false;

            bool upgraded = selectedTurretUpgradeManager.UpgradeTurret();
            if (upgraded)
            {
                UpdateButtonText();
            }
            else
            {
                Debug.Log("Pas assez d'argent pour améliorer.");
            }

            Invoke(nameof(ReactivateButton), 0.5f);
        }
    }

    private void ReactivateButton()
    {
        upgradeButton.interactable = true;
    }

    private void UpdateButtonText()
    {
        if (selectedTurretUpgradeManager != null)
        {
            int currentLevel = selectedTurretUpgradeManager.currentLevel;
            if (currentLevel < 3)
            {
                buttonText.text = "Upgrade";
            }
            else
            {
                buttonText.text = "Max Lvl";
            }
        }
    }

    public void SetSelectedTurret(TurretUpgradeManager newSelectedTurret)
    {
        selectedTurretUpgradeManager = newSelectedTurret;
        UpdateButtonText();
    }
}
