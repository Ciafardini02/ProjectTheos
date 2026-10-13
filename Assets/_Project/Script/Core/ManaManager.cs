using UnityEngine;

public class ManaManager : MonoBehaviour
{
    //Il singleton ci permette di leggere il mana da qualsiasi script senza dover fare il collegamento nell'ispector
    public static ManaManager Instance { get; private set; }
    
    [Header("Stato Mana")]
    public int currentMana;

    public int maxManaThisTurn;

    private void Awake()
    {
        //Setup del Singleton
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    //Viene chiamato all'inizio del turno
    public void SetupForTurn(int turnNumber)
    {
        //Il mana massimo corrisponde al numero del turno
        maxManaThisTurn = Mathf.Min(turnNumber, 9);
        currentMana = maxManaThisTurn;
        
        Debug.Log($"<color=blue>Mana Ricaricato: {currentMana} / {maxManaThisTurn}</color>");
        if (UIManager.Instance != null)
            UIManager.Instance.UpdateManaDisplay(currentMana, maxManaThisTurn);
    }

    public bool CanAfford(int cost)
    {
        return currentMana >= cost;
    }

    public void SpendMana(int cost)
    {
        if (CanAfford(cost))
        {
            currentMana -= cost;
            Debug.Log($"Spesi {cost} Mana. Riferimenti: {currentMana} / {maxManaThisTurn}");
            
            if (UIManager.Instance != null)
                UIManager.Instance.UpdateManaDisplay(currentMana, maxManaThisTurn);
        }
    }
}
