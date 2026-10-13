using UnityEngine;
using System.Collections.Generic;

public class LaneController : MonoBehaviour
{
    [Header("Slot della Corsia")]
    public List<BoardSlot> playerSlots;
    public List<BoardSlot> opponentSlots;

    [Header("Punteggi Correnti (Debug)")] 
    public int playerTotalPower = 0;
    public int opponentTotalPower = 0;
    
    //Ricalcola la forza totale di entrambi i giocatori in questa corsia
    public void CalculateLanePower()
    {
        playerTotalPower = 0;
        opponentTotalPower = 0;
        
        //Somma la forza delle carte del giocatore
        foreach (BoardSlot slot in playerSlots)
        {
            if (slot.isOccupied)
            {
                playerTotalPower += slot.currentCard.data.basePower;
            }
        }
        
        //Somma la forza delle carte dell'avversario
        foreach (BoardSlot slot in opponentSlots)
        {
            if (slot.isOccupied)
            {
                opponentTotalPower += slot.currentCard.data.basePower;
            }
        }
    }
    
    //Restituisce chi sta vincendo la corsia (+1 Giocatore, -1 Avversario, 0 Pareggio)
    public int GetLaneWinner()
    {
        if (playerTotalPower > opponentTotalPower) return 1;
        if (opponentTotalPower > playerTotalPower) return -1;
        return 0;
    }
}
