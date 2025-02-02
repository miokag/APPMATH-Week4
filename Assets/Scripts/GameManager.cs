using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int playerHP = 5;
    public int gold = 0;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI goldText;
    public GameObject gameOverUI;
    public GameObject shopUI;
    
    private bool isPaused = false;

    public EnemySpawner enemySpawner; // Reference to the spawner

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (hpText == null || goldText == null)
        {
            Debug.LogError("Text fields not assigned in the Inspector.");
        }
    }
    
    public void PauseGame()
    {
        isPaused = !isPaused;  // Toggle the pause state

        if (isPaused)
        {
            Time.timeScale = 0f;  // Pauses the game
            shopUI.SetActive(true);  // Show the shop UI
        }
        else
        {
            Time.timeScale = 1f;  // Resumes the game
            shopUI.SetActive(false);  // Hide the shop UI
        }
    }


    void Start()
    {
        if (enemySpawner == null)
        {
            enemySpawner = FindObjectOfType<EnemySpawner>(); // Auto-assign if not set
        }
        UpdateUI();
    }

    public void TakeDamage()
    {
        playerHP--;
        UpdateUI();  // Ensure the UI is updated
        if (playerHP <= 0)
        {
            GameOver();
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateUI();  // Ensure the UI is updated
    }


    public void UpdateUI()
    {
        hpText.text = "HP: " + playerHP;
        goldText.text = "Gold: " + gold;
    }

    void GameOver()
    {
        if (enemySpawner != null)
        {
            enemySpawner.StopSpawning();
        }
        
        // Destroy all active enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);  // Destroy each enemy
        }

        hpText.text = "HP: 0";
        gameOverUI.SetActive(true);
    }
}