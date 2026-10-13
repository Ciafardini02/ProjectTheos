using System;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class CardView : MonoBehaviour
{
    [Header("Dati")] public CardData data;

    [Header("Riferimenti Grafici")]
    public TMP_Text nameText;
    public TMP_Text costText;
    public TMP_Text powerText;
    //[SerializeField] private SpriteRenderer artworkRenderer;

    public void SetupCard(CardData newData)
    {
        data = newData;
        if (nameText != null) nameText.text = data.cardName;
        if (costText != null) costText.text = data.manaCost.ToString();
        if (powerText != null) powerText.text = data.basePower.ToString();
        /*if (artworkRenderer != null) artworkRenderer.sprite = data.artwork;
        {
            artworkRenderer.sprite = data.artwork;
        }*/
    }
}
