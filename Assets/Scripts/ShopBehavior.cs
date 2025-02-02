using UnityEngine;
using UnityEngine.UI;

public class ShopBehavior : MonoBehaviour
{
    [Header("Upgrade Buttons")]
    [SerializeField] private Button speedButton;
    [SerializeField] private Button rangeButton;
    [SerializeField] private Button killDistanceButton;

    [Header("Upgrade Costs")]
    [SerializeField] private int speedUpgradeCost = 10;
    [SerializeField] private int rangeUpgradeCost = 15;
    [SerializeField] private int killDistanceUpgradeCost = 20;

    [Header("Turret Stats")]
    [SerializeField] private TurretBehavior turret; // Reference to the turret

    private void Start()
    {
        // Add listeners for the buttons
        speedButton.onClick.AddListener(UpgradeSpeed);
        rangeButton.onClick.AddListener(UpgradeRange);
        killDistanceButton.onClick.AddListener(UpgradeKillDistance);
    }

    private void Update()
    {
        // Dynamically update button interactability based on available gold
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        // Check if there is enough gold for each upgrade and update the button state
        bool canUpgradeSpeed = GameManager.Instance.gold >= speedUpgradeCost;
        bool canUpgradeRange = GameManager.Instance.gold >= rangeUpgradeCost;
        bool canUpgradeKillDistance = GameManager.Instance.gold >= killDistanceUpgradeCost;

        SetButtonState(speedButton, canUpgradeSpeed);
        SetButtonState(rangeButton, canUpgradeRange);
        SetButtonState(killDistanceButton, canUpgradeKillDistance);
    }

    private void SetButtonState(Button button, bool canUpgrade)
    {
        // Disable or enable button based on the canUpgrade condition
        button.interactable = canUpgrade;

        // Change button color to gray if not interactable
        ColorBlock colors = button.colors;
        if (canUpgrade)
        {
            colors.normalColor = Color.white; // Active color (can be set to any color you want)
            colors.disabledColor = Color.gray; // Disabled color
        }
        else
        {
            colors.normalColor = Color.gray; // Gray color when not enough gold
        }
        button.colors = colors;
    }

    private void UpgradeSpeed()
    {
        if (GameManager.Instance.gold >= speedUpgradeCost)
        {
            GameManager.Instance.gold -= speedUpgradeCost; // Deduct gold
            turret.UpgradeSpeed(1); // Upgrade speed (e.g., increase by 5)
            Debug.Log("Speed upgraded!");
            GameManager.Instance.UpdateUI();
        }
        else
        {
            Debug.Log("Not enough gold to upgrade speed.");
        }
    }

    private void UpgradeRange()
    {
        if (GameManager.Instance.gold >= rangeUpgradeCost)
        {
            GameManager.Instance.gold -= rangeUpgradeCost; // Deduct gold
            turret.UpgradeRange(1); // Upgrade range (e.g., increase by 5)
            Debug.Log("Range upgraded!");
            GameManager.Instance.UpdateUI();
        }
        else
        {
            Debug.Log("Not enough gold to upgrade range.");
        }
    }

    private void UpgradeKillDistance()
    {
        if (GameManager.Instance.gold >= killDistanceUpgradeCost)
        {
            GameManager.Instance.gold -= killDistanceUpgradeCost; // Deduct gold
            turret.UpgradeKillDistance(0.2f); // Upgrade kill distance (e.g., increase by 1)
            Debug.Log("Kill distance upgraded!");
            GameManager.Instance.UpdateUI();
        }
        else
        {
            Debug.Log("Not enough gold to upgrade kill distance.");
        }
    }
}
