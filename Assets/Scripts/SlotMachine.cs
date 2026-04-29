using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Main controller - handles spin button, checks win condition,
/// and calculates payouts across all reels.
/// </summary>
public class SlotMachine : MonoBehaviour
{
    [Header("Reels")]
    public Reel[] reels;           // Array of 3 reels

    [Header("UI")]
    public Button spinButton;      // The spin button
    public GameObject winPanel;    // Panel shown on win

    private bool isSpinning = false;

    void Start()
    {
        winPanel.SetActive(false);  // Hide win panel at start
    }

    /// <summary>
    /// Called when player presses Spin button.
    /// </summary>
    public void OnSpinButtonPressed()
    {
        if (isSpinning) return;  // Prevent double spinning

        // Try to place bet - returns false if not enough balance
        if (!GameManager.Instance.PlaceBet())
        {
            Debug.Log("Not enough balance!");
            return;
        }

        winPanel.SetActive(false);  // Hide previous win message
        StartCoroutine(SpinAllReels());
    }

    /// <summary>
    /// Spins all reels with a stagger delay between each.
    /// </summary>
    private IEnumerator SpinAllReels()
    {
        isSpinning = true;
        spinButton.interactable = false;  // Disable button while spinning

        // Start each reel with a slight delay for visual effect
        for (int i = 0; i < reels.Length; i++)
        {
            StartCoroutine(reels[i].Spin(i * 0.3f));
        }

        // Wait for all reels to finish (last reel delay + spin duration)
        float totalWaitTime = reels[reels.Length - 1].spinDuration 
                              + (reels.Length - 1) * 0.3f + 0.5f;
        yield return new WaitForSeconds(totalWaitTime);

        CheckWinCondition();

        isSpinning = false;
        spinButton.interactable = true;  // Re-enable spin button
    }

    /// <summary>
    /// Checks if all reels show the same symbol - if yes, player wins.
    /// </summary>
    private void CheckWinCondition()
    {
        SymbolData first = reels[0].GetCurrentSymbol();

        bool allMatch = true;
        foreach (Reel reel in reels)
        {
            if (reel.GetCurrentSymbol().symbolName != first.symbolName)
            {
                allMatch = false;
                break;
            }
        }

        if (allMatch)
        {
            // Calculate payout: bet x symbol multiplier
            int winAmount = GameManager.Instance.currentBet 
                           * first.payoutMultiplier;
            GameManager.Instance.AddWinnings(winAmount);
            winPanel.SetActive(true);  // Show win panel
            Debug.Log("WIN! Amount: " + winAmount);
        }
        else
        {
            GameManager.Instance.winText.text = "Try Again!";
            Debug.Log("No match - try again!");
        }
    }
}