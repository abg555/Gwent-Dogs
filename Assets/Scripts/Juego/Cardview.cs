using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

public class Cardview : MonoBehaviour
{
    public Cards cardview;                       /*Estas son las propiedades de vista de las cartas*/
    public string cardZone;
    public string cardZone2;
    public string cardZone3;
    public int viewCardNumber;
    public int cardNumber;
    public int cardParent;
    public int cardKind;
    public GameObject gameObject;

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
    {   /*se inicializan las propiedades de Cardview con los valores de la carta correspondiente*/
        cardParent = cardview.Owner;
        cardKind = cardview.cardKind;
        cardZone = cardview.zone;
        cardZone2 = cardview.zone2;
        cardZone3 = cardview.zone3;
        cardNumber = cardview.cardNumber;
        cardName = cardview.cardName;
        cardPower = cardview.cardPower;
        cardHability = cardview.cardHability;
        cardBackgroundImage = cardview.cardBackgroundImage;
        cardDogImage = cardview.cardDogImage;
        cardPowerImage = cardview.cardPowerImage;
        cardDescriptionImage = cardview.cardDescriptionImage;
        cardPlaceboardImage = cardview.cardPlaceboardImage;
        gameObject = cardview.gameObject;
        nameText.text = cardName;
        habilityText.text = cardHability;
        powerText.text = cardPower.ToString();
        backgroundImage.sprite = cardBackgroundImage;
        placeImage.sprite = cardPlaceboardImage;
        dogImage.sprite = cardDogImage;
        powerImage.sprite = cardPowerImage;
        descriptionImage.sprite = cardDescriptionImage;
    }

}

