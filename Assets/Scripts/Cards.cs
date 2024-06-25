using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]  /* [CreateAssetMenu(fileName = "New Card", menuName = "Card")] es un atributo que permite crear instancias de esta clase en el menú de Unity*/
public class Cards : ScriptableObject      /*ScriptableObject es una clase base en Unity utilizada para crear objetos que no necesitan estar vinculados a un objeto de juego.*/
{
    public int cardNumber;                      /*Estas son los atributos de las cartas*/
    public string cardName;
    public int cardPower;
    public int cardParent;
    public int cardKind;
    public string cardHability;
    public Sprite cardBackgroundImage;
    public Sprite cardDogImage;
    public Sprite cardPowerImage;
    public Sprite cardDescriptionImage;
    public Sprite cardPlaceboardImage;
    public string zone;
    public string zone2;


    public Cards()
    {

    }


}
