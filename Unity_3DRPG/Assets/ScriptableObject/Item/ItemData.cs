using System;
using UnityEngine;
public enum ItemType
{
    Consumable, // ¼·Ãë°¡´É
    Coin // °× µ·
}

[Serializable]
public class ItemDataConsumable
{
    public ItemType type;
    public float value;
    public float duration;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;
}
