using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="card",menuName = "card")]

public class Cards : ScriptableObject
{
    public int cardNumber;
    public string cardName;
    public int cardPower;
    public string cardHability;
    public Sprite cardBackgroundIm;
    public Sprite cardMainIm;
    public Sprite cardPowerIm;
    public Sprite cardHabilityIm;
    public Sprite cardPlaceboardIm;


public Cards(int cardNumber, string cardName, int cardPower, string cardHability, Sprite cardBackgroundIm, Sprite cardMainImage, Sprite cardPowerImage, Sprite cardHabilityImage, Sprite cardPlaceboardImage){
   this.cardNumber = cardNumber;
   this.cardName = cardName;
   this.cardPower = cardPower;
   this.cardHability = cardHability;
   this.cardBackgroundIm = cardBackgroundIm;
   this.cardMainIm = cardMainImage;
   this.cardPowerIm = cardPowerImage;
   this.cardHabilityIm = cardHabilityImage;
   this.cardPlaceboardIm = cardPlaceboardImage; 
}


}
