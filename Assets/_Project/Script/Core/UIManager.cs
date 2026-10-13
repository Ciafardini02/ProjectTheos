using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}
    
    [Header("Riferimenti UI")]
    public TMP_Text manaText;
    public TMP_Text turnText;
    public TMP_Text balanceText;
    public Button endTurnButton;

    [Header("Riferimento Controller")] 
    public GameTurnController turnController;

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Start()
    {
        if (endTurnButton != null && turnController != null)
        {
            endTurnButton.onClick.AddListener(turnController.EndTurn);
        }

        UpdateAllUI();
    }

    public void UpdateManaDisplay(int current, int max)
    {
        if (manaText != null)
            manaText.text = $"Mana: {current}/{max}";
    }

    public void UpdateTurnDisplay(int current, int maxTurns)
    {
        if (turnText != null)
            turnText.text = $"Turno: {current}/{maxTurns}";
    }

    public void UpdateBalanceDisplay(int balance)
    {
        if (balanceText != null)
        {
            string sign = balance > 0 ? "+" : "";
            balanceText.text = $"Bilancia: {sign}{balance}";
        }
    }

    public void UpdateAllUI()
    {
        if (ManaManager.Instance != null)
            UpdateManaDisplay(ManaManager.Instance.currentMana, ManaManager.Instance.maxManaThisTurn);
        
        if (turnController != null)
            UpdateTurnDisplay(turnController.currentTurn, GameTurnController.maxTurns);
        
        if (turnController != null && turnController.scaleManager != null)
            UpdateBalanceDisplay(turnController.scaleManager.currentBalance);
    }
}
