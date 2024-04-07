using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Cards : ScriptableObject
{
    public int cardNumber;
    public string cardName;
    public int cardPower;
    public string cardHability;
    public Sprite cardBackgroundImage;
    public Sprite cardDogImage;
    public Sprite cardPowerImage;
    public Sprite cardDescriptionImage;
    public Sprite cardPlaceboardImage;
    public string zone;


public Cards(){

}


}
