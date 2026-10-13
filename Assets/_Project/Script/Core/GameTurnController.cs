using UnityEngine;

public class GameTurnController : MonoBehaviour
{
    [Header("Riferimenti Core")]
    public ScaleManager scaleManager;
    public ManaManager manaManager;
    public DeckManager deckManager;

    [Header("Stato Partita")]
    public int currentTurn = 1;
    public const int maxTurns = 9;

    private void Start()
    {
        // 1. Setup Mana iniziale (Turno 1)
        if (manaManager != null) 
            manaManager.SetupForTurn(currentTurn);

        // 2. Pescata Iniziale: 3 Carte
        if (deckManager != null) 
            deckManager.DrawCards(3);
    }

    private void Update()
    {
        // Temporaneo con la barra spaziatrice
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
        }
    }

    public void EndTurn()
    {
        Debug.Log($"--- Fine del Turno {currentTurn} ---");

        // Risoluzione dell'Era solo ai turni 3, 6 e 9
        if (currentTurn == 3 || currentTurn == 6 || currentTurn == 9)
        {
            if (scaleManager != null)
            {
                scaleManager.EvaluateEra();
            }
        }

        currentTurn++;

        if (currentTurn <= maxTurns)
        {
            // Ricarica e scala del Mana per il nuovo turno
            if (manaManager != null) 
                manaManager.SetupForTurn(currentTurn);

            // Pescata di inizio turno: 1 carta
            if (deckManager != null) 
                deckManager.DrawCards(1);
        }
        else
        {
            Debug.Log("<color=yellow>Partita Conclusa (Turno 9 terminato)!</color>");
        }

        if (UIManager.Instance != null)
            UIManager.Instance.UpdateAllUI();
    }
}