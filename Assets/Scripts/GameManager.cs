using UnityEngine;
using TMPro;

/// <summary>
/// Manages player balance, bet amount, and overall game state.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton

    [Header("Player Stats")]
    public int playerBalance = 1000;   // Starting balance
    public int currentBet = 10;        // Default bet amount

    [Header("UI References")]
    public TextMeshProUGUI balanceText;
    public TextMeshProUGUI betText;
    public TextMeshProUGUI winText;

    void Awake()
    {
        // Singleton pattern - only one GameManager exists
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    // Deduct bet from balance before spinning
    public bool PlaceBet()
    {
        if (playerBalance >= currentBet)
        {
            playerBalance -= currentBet;
            UpdateUI();
            return true;
        }
        return false; // Not enough balance
    }

    // Add winnings to balance
    public void AddWinnings(int amount)
    {
        playerBalance += amount;
        winText.text = "WIN! +" + amount;
        UpdateUI();
    }

    // Update all UI text elements
    public void UpdateUI()
    {
        balanceText.text = "Balance: $" + playerBalance;
        betText.text = "Bet: $" + currentBet;
    }

    // Increase bet amount
    public void IncreaseBet()
    {
        if (currentBet + 10 <= playerBalance)
        {
            currentBet += 10;
            UpdateUI();
        }
    }

    // Decrease bet amount
    public void DecreaseBet()
    {
        if (currentBet - 10 >= 10)
        {
            currentBet -= 10;
            UpdateUI();
        }
    }
}