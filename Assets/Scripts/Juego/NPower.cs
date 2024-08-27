using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPower : MonoBehaviour
{
    public GameObject as1;
    public PowerZoneManager powerZoneManager;

    public void NPowerChau()
    {
        int sameCardNameCount = 0;
        string targetCardName = "Chau Chau Pescadu";
        Cardview specificCard = null;
        int newPower = 0;


        foreach (Transform child in as1.transform) //busca en los hijos de as1
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardName == targetCardName && cardView.cardNumber != 21)
            {

                sameCardNameCount++;
                // Debug.Log("masss");


            }
            else if (cardView != null && cardView.cardNumber == 21)
            {
                specificCard = cardView;
            }
            //obtienen el componente Cardview del hijo actual en el bucle y verifican si el componente existe, si el nombre de la carta es igual a targetCardName y si el número de la carta es distinto de 21. Si se cumplen estas condiciones, se incrementa sameCardNameCount. Si el número de la carta es 21, se guarda una referencia a la carta en specificCard

        }

        if (specificCard != null)//Si se encontró una tarjeta con número 21, se calcula el nuevo poder multiplicando sameCardNameCount por el poder de la carta específica
        {

            newPower = sameCardNameCount * specificCard.cardPower;



            Debug.Log(newPower);
        }

        powerZoneManager.RemoveCardPower("as", specificCard.cardPower);
        powerZoneManager.AddCardPower("as", newPower);
        //se elmina el poder de la carta específica de la zona “as” y luego agregan el nuevo poder a la misma zona
    }
}

