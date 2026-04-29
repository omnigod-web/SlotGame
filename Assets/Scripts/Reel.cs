using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Controls a single reel - spinning animation and final symbol display.
/// </summary>
public class Reel : MonoBehaviour
{
    [Header("Symbol Settings")]
    public SymbolData[] possibleSymbols;  // All symbols this reel can show
    public Image symbolImage;             // UI Image that displays the symbol

    [Header("Spin Settings")]
    public float spinDuration = 2f;       // How long the reel spins
    public float spinSpeed = 0.05f;       // How fast symbols change during spin

    private SymbolData currentSymbol;     // The final landed symbol
    private bool isSpinning = false;

    /// <summary>
    /// Returns the symbol this reel landed on.
    /// </summary>
    public SymbolData GetCurrentSymbol()
    {
        return currentSymbol;
    }

    /// <summary>
    /// Starts the spinning animation and picks a random symbol to land on.
    /// </summary>
    public IEnumerator Spin(float delay)
    {
        yield return new WaitForSeconds(delay); // Stagger reel starts

        isSpinning = true;
        float elapsed = 0f;

        // Spin animation - rapidly cycle through symbols
        while (elapsed < spinDuration)
        {
            // Pick a random symbol to display during spin
            int randomIndex = Random.Range(0, possibleSymbols.Length);
            symbolImage.sprite = possibleSymbols[randomIndex].symbolSprite;
            elapsed += spinSpeed;
            yield return new WaitForSeconds(spinSpeed);
        }

        // Pick the FINAL symbol using RNG
        int finalIndex = Random.Range(0, possibleSymbols.Length);
        currentSymbol = possibleSymbols[finalIndex];
        symbolImage.sprite = currentSymbol.symbolSprite;

        isSpinning = false;
    }
}