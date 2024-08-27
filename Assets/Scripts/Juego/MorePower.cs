using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorePower : MonoBehaviour
{
    public GameObject[] positions;
    public PowerZoneManager2 powerZoneManager;
    public PowerZoneManager2 powerZoneManager2;
    public PowerZoneManager2 powerZoneManager3;
    public GameManager gameManager;


    public void MorePower1()
    {

        Debug.Log("masi");
        GameObject maxPowerCard = null;
        float maxPower = float.MinValue;

        foreach (GameObject position in positions) //recorre todos los objetos en el arreglo positions
        {
            foreach (Transform child in position.transform)//recorre todos los hijos del objeto position actual en el bucle
            {
                Cardview cardvio = child.GetComponent<Cardview>();
                if (cardvio != null && cardvio.cardKind == 1 && cardvio.cardPower > maxPower)
                {
                    maxPower = cardvio.cardPower;
                    maxPowerCard = child.gameObject;
                }
            }//Estas líneas obtienen el componente Cardview del hijo actual en el bucle y verifican si el componente existe, si cardKind es 1 y si cardPower es mayor que maxPower. Si se cumplen estas condiciones, se actualizan maxPower y maxPowerCard
        }

        if (maxPowerCard != null)
        {
            Cardview cardView = maxPowerCard.GetComponent<Cardview>();
            if (cardView != null)
            {
                Debug.Log("maii");
                int powerToRemove = cardView.cardPower;
                cardView.cardPower = 0;//guardan el poder de la carta en powerToRemove y luego establecen el poder de la carta en 0.
                if (cardView.cardZone == "c2")
                {
                    powerZoneManager.RemoveCardPower2(cardView.cardZone, powerToRemove);
                }
                if (cardView.cardZone == "ar2")
                {
                    powerZoneManager2.RemoveCardPower2(cardView.cardZone, powerToRemove);
                }
                if (cardView.cardZone == "as2")
                {
                    powerZoneManager3.RemoveCardPower2(cardView.cardZone, powerToRemove);
                }// se elimina el poder de la carta

                gameManager.cementerys.Add(maxPowerCard);//la manda al cementerio
            }
            Destroy(maxPowerCard);//detruye la carta



        }

    }
}
