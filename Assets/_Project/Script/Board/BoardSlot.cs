using UnityEngine;

public class BoardSlot : MonoBehaviour
{
    [Header("Stato Slot")]
    public CardView currentCard;
    public bool isOccupied => currentCard != null;
    
    //Posiziona una carta in questo slot
    public bool PlaceCard(CardView card)
    {
        if (isOccupied) return false;

        currentCard = card;
        //Sposta fisicamente la carta al centro dello slot
        card.transform.position = transform.position;
        card.transform.rotation = transform.rotation;
        card.transform.SetParent(transform);
        return true;
    }
    
    //Rimuove la carta (per sacrifici o distruzioni)
    public void ClearSlot()
    {
        currentCard = null;
    }
}
