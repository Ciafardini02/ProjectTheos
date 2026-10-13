using System;
using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    public static DeckManager instance { get; private set; }
    [Header("Impostazioni Mazzo")] 
    public List<CardData> myDeck = new List<CardData>(); //Le carte del mazzo .asset
    public GameObject cardPrefab; // Il "Corpo Fisico" da generare
    
    [Header("Impostazioni Mano")]
    public Transform handContainer;     //Il punto centrale in basso dove appaiono
    public float spacing = 1.5f;        //Distanza tra una carta e l'altra
    
    //Lista per tenere traccia delle carte fisiche che hai attualmente in mano
    private List<GameObject> cardsInHand = new List<GameObject>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    //Metodo per pescare N carte
    public void DrawCards(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            //Controllo di sicurezza: se il mazzo è vuoto, smettiamo di pescare
            if (myDeck.Count == 0)
            {
                Debug.Log("il mazzo è vuoto!");
                break;
            }
            
            //1. Prendi la prima carta e toglila dal mazzo virtuale
            CardData drawCardData = myDeck[0];
            myDeck.RemoveAt(0);
            
            //2. Evoca fisicamente il Prefab mettendolo dentro all'_HandContainer
            GameObject newCard = Instantiate(cardPrefab, handContainer);
            
            //3. Inserisci i dati dentro il corpo fisico
            CardView view = newCard.GetComponent<CardView>();
            if (view != null)
            {
                view.SetupCard(drawCardData);
            }
            
            //4.Aggiungila alla lista delle carte in mano
            cardsInHand.Add(newCard);
        }
        
        //Dopo aver pescato, riordiniamo le carte sul tavolo a ventaglio in fila
        OrganizeHand();
    }
    
    //Mette le carte in fila indiana e le centra rispetto all'_HandContainer
    public void OrganizeHand()
    {
        int cardCount = cardsInHand.Count;
        if (cardCount == 0) return;
        
        //Calcola la larghezza totale occupata dalle carte
        float totalWidth = (cardCount - 1) * spacing;
        float startX = -totalWidth / 2f;    //Troviamo il punto di partenza più a sinistra per tenerle centrate

        for (int i = 0; i < cardCount; i++)
        {
            //Se una carta è distrutta o giocata ignoriamola
            if(cardsInHand[i] == null) continue;
            
            //Spostiamo la carta lungo l'asse X
            float xPos = startX + (i * spacing);
            
            //Impostiamo la sua posizione realtiva all'_HandController
            cardsInHand[i].transform.localPosition = new Vector3(xPos, 0, 0);
        }
    }

    public void RemoveCardFromHand(GameObject cardObj)
    {
        if (cardsInHand.Contains(cardObj))
        {
            cardsInHand.Remove(cardObj);
            OrganizeHand();
        }
    }
}
