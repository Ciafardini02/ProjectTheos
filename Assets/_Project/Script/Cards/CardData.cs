using UnityEngine;

public enum FactionType
{
    Tyrant,
    Abomination,
    Neutral
}

[CreateAssetMenu(fileName = "New_Card", menuName = "Theos")]
public class CardData : ScriptableObject
{
    [Header("Identità")]
    public string cardId;
    public string cardName;
    public FactionType faction;
    [TextArea(2, 4)] public string abilityDescription;

    [Header("Statistiche Base")] 
    [Range(1, 9)] public int manaCost = 1;
    [Range(0, 20)] public int basePower = 1;
    
    [Header("Elementi Visivi")]
    public Sprite artwork;
}
