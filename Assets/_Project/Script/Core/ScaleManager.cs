using UnityEngine;

public class ScaleManager : MonoBehaviour
{
    [Header("Stato Bilancia")]
    [Tooltip("0 è il centro. +12 vittoria Giocatore, -12 vittoria Avversario")]
    [Range(-12, 12)] public int currentBalance = 0;
    
    [Header("Moltiplicatori Snap")]
    public bool isSnapActive = false;
    public bool isDoubleSnapActive = false;

    [Header("Riferimenti Corsie (Drag and Drop)")]
    public LaneController leftLane;
    public LaneController rightLane;
    public LaneController centerLane;
    
    //Metodo chiamato alla fine di ogni Era (Turni 3, 6, 9)
    public void EvaluateEra()
    {
        
        //1. Ricalcola le forze attuali di tutte le corsie
        leftLane.CalculateLanePower();
        rightLane.CalculateLanePower();
        centerLane.CalculateLanePower();
        
        //2. Conta chi ha vinto quante corsie
        int playerLanes = 0;
        int opponentLanes = 0;
        
        CheckLaneResult(leftLane.GetLaneWinner(), ref playerLanes, ref opponentLanes);
        CheckLaneResult(centerLane.GetLaneWinner(), ref playerLanes, ref opponentLanes);
        CheckLaneResult(rightLane.GetLaneWinner(), ref playerLanes, ref opponentLanes);
        
        //3. Calcola lo spostamento della bilancia
        CalculateScoreShift(playerLanes, opponentLanes);
        
        //4. Verific se qualcuno ha fatto KO
        CheckForKnockout();
    }

    private void CheckLaneResult(int result, ref int pLanes, ref int oLanes)
    {
        if (result == 1) pLanes++;
        else if (result == -1) oLanes++;
    }

    private void CalculateScoreShift(int playerLanes, int opponentLanes)
    {
        //Uno scatto per ogni campo vinto, sottratto ai campi persi
        int scoreShift = playerLanes - opponentLanes;

        // Applica lo Snap se qualcuno ha vinto l'Era
        if (isSnapActive)
        {
            if (scoreShift > 0) scoreShift += 1; // Bonus se stai vincendo
            else if (scoreShift < 0) scoreShift -= 1; // Penalità ulteriore se stai perdendo
        }

        // Il Contro-Snap raddoppia l'impatto finale
        if (isDoubleSnapActive)
        {
            scoreShift *= 2;
        }

        // Aggiorna la bilancia
        currentBalance += scoreShift;
        currentBalance = Mathf.Clamp(currentBalance, -12, 12);
    
        Debug.Log($"Fine Era! Campi vinti [Tu: {playerLanes} | Nemico: {opponentLanes}]. Spostamento netto: {scoreShift}. Bilancia: {currentBalance}");
        
        if (UIManager.Instance != null)
            UIManager.Instance.UpdateBalanceDisplay(currentBalance);
    }

    private void CheckForKnockout()
    {
        if (currentBalance >= 12)
        {
            Debug.Log("KO! Il giocatore ha vinto");
        }
        else if (currentBalance <= -12)
        {
            Debug.Log("KO! L'avversario ha vinto");
        }
    }

}
