using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Cardsview : MonoBehaviour
 {
    public Cards card;
    public TMP_Text nameText;
    public TMP_Text habilityText;
    public TMP_Text power;
    public Image backgroundImage;
    public Image placeImage; 
    public Image mainImage;
    public Image powerImage;
    public Image descriptionImage;
    
    void Start(){
    nameText.text=  card.cardName;
    habilityText.text = card.cardHability;
    power.text = card.cardPower.ToString();
    backgroundImage.sprite = card.cardBackgroundIm;
    placeImage.sprite = card.cardPlaceboardIm;
    mainImage.sprite = card.cardMainIm;
    powerImage.sprite = card.cardPowerIm;
    descriptionImage.sprite = card.cardHabilityIm;
    }
}
