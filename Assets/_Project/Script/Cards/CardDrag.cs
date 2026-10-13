using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(CardView))]
public class CardDrag : MonoBehaviour
{
    private Vector3 originalPosition;
    private Camera mainCamera;
    private CardView cardView;
    private bool isPlaced = false;

    private void Start()
    {
        mainCamera = Camera.main;
        cardView = GetComponent<CardView>();
    }

    private void OnMouseDown()
    {
        if (isPlaced) return; 

        originalPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        if (isPlaced) return;

        // Trascina la carta su un piano fisso (all'altezza Y originale + un pochino)
        Plane dragPlane = new Plane(Vector3.up, new Vector3(0, originalPosition.y + 0.3f, 0));
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (dragPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            transform.position = new Vector3(hitPoint.x, originalPosition.y + 0.3f, hitPoint.z);
        }
    }

    private void OnMouseUp()
    {
        if (isPlaced) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        // NUOVO: Spara un laser perforante che prende TUTTO quello che c'è sotto il mouse
        RaycastHit[] hits = Physics.RaycastAll(ray, 100f);
        
        foreach (RaycastHit hit in hits)
        {
            // Cerchiamo se tra gli oggetti colpiti (anche sotto la carta) c'è uno Slot
            BoardSlot slot = hit.collider.GetComponent<BoardSlot>();
            
            if (slot != null && !slot.isOccupied)
            {
                //1. Leggiamo quanto costa la carta
                int cost = cardView.data.manaCost;
                
                //2. Chiediamo al Manager se ce la possiamo permettere
                if (ManaManager.Instance.CanAfford(cost))
                {
                    ManaManager.Instance.SpendMana(cost);   //Paghiamo

                    slot.PlaceCard(cardView);
                    isPlaced = true;
                    
                    //1. Rimuovi la carta dalla gestione della mano
                    if (DeckManager.instance != null)
                    {
                        DeckManager.instance.RemoveCardFromHand(gameObject);
                    }
                    
                    //2. Imposta lo slot come nuovo genitore per fissare la gerarchia
                    transform.SetParent(slot.transform);

                    string nomeCarta = (cardView.data != null) ? cardView.data.cardName : "Sconosciuta";
                    Debug.Log($"Carta {nomeCarta} giocata in slot {slot.gameObject.name}!");
                    return;
                }
                else
                {
                    //Se non hai mana, stampa l'errero e non fare il "return",
                    //così lo script prosegue e fa tornare la carta indietro
                    Debug.Log($"<color=red>Mana insufficiente!</color> Costa {cost}, ma hai {ManaManager.Instance.currentMana} Mana.");
                }
            }
        }

        // Se il ciclo finisce e non abbiamo trovato nessuno slot valido, torna indietro
        transform.position = originalPosition;
    }
}