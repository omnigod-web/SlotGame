using UnityEngine;

/// <summary>
/// Holds data for a single slot symbol (sprite, name, payout).
/// Used as a ScriptableObject so symbols can be created as assets.
/// </summary>
[CreateAssetMenu(fileName = "SymbolData", menuName = "SlotGame/Symbol")]
public class SymbolData : ScriptableObject
{
    [Header("Symbol Info")]
    public string symbolName;      // e.g. "Cherry", "Seven", "Bell"
    public Sprite symbolSprite;    // The image shown on the reel
    public int payoutMultiplier;   // Win amount = bet x this value
}