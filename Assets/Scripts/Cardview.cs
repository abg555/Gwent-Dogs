using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

public class Cardview : MonoBehaviour
{   
    public Cards cardview;
    public string cardZone;
    public int viewCardNumber;
    public int cardNumber;
    public string cardName;
    public int cardPower;
    public string cardHability;
    public Sprite cardBackgroundImage;
    public Sprite cardDogImage;
    public Sprite cardPowerImage;
    public Sprite cardDescriptionImage;
    public Sprite cardPlaceboardImage;
    
    public TMP_Text nameText;
    public TMP_Text habilityText;
    public TMP_Text powerText;
    public Image backgroundImage;
    public Image placeImage; 
    public Image dogImage;
    public Image powerImage;
    public Image descriptionImage;


    void Start()
    {
        cardZone = cardview.zone;
        cardNumber = cardview.cardNumber;
        cardName = cardview.cardName;
        cardPower = cardview.cardPower;
        cardHability = cardview.cardHability;
        cardBackgroundImage = cardview.cardBackgroundImage;
        cardDogImage = cardview.cardDogImage;
        cardPowerImage = cardview.cardPowerImage;
        cardDescriptionImage = cardview.cardDescriptionImage;
        cardPlaceboardImage = cardview.cardPlaceboardImage;
    
        nameText.text =  cardName;
        habilityText.text = cardHability;
        powerText.text = cardPower.ToString();
        backgroundImage.sprite =  cardBackgroundImage;
        placeImage.sprite = cardPlaceboardImage;
        dogImage.sprite = cardDogImage;
        powerImage.sprite = cardPowerImage;
        descriptionImage.sprite = cardDescriptionImage;
    }

}

