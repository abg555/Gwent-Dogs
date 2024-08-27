using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleamWeather4 : MonoBehaviour
{
    public GameObject cl;
    public PowerZoneManager powerZoneManager;
    public PowerZoneManager2 powerZoneManager1;
    public GameObject ar;
    public GameObject ar2;

    bool hasCardNumber4 = false;
    bool hasCardNumber30 = false;


    public void Cleam()
    {

        foreach (Transform child in cl.transform)
        {

            Cardview cardvio = child.GetComponent<Cardview>();
            if (cardvio != null)
            {

                if (cardvio.cardNumber == 3)
                {
                    hasCardNumber4 = true;
                }
                else if (cardvio.cardNumber == 29)
                {
                    hasCardNumber30 = true;
                }

            }

            if (hasCardNumber4 && !hasCardNumber30)
            {
                Increase();
                Increase2();
                Destroy(child.gameObject);
            }
            else if (!hasCardNumber4 && hasCardNumber30)
            {
                Increase();
                Increase2();
                Destroy(child.gameObject);
            }
            else if (hasCardNumber4 && hasCardNumber30)
            {
                // Increase3();
                // Increase4();
                Increase();
                Increase2();
                Destroy(child.gameObject);
            }
        }

    }
    void Increase()
    {
        foreach (Transform child in ar.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardKind == 1)
            {
                cardView.cardPower += 1;
                Debug.Log("El nuevo valor de cardPower para " + cardView.cardName + " es: " + cardView.cardPower);
                powerZoneManager.AddCardPower(cardView.cardZone, 1);
            }
        }
    }
    void Increase2()
    {
        foreach (Transform child in ar2.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardKind == 1)
            {
                cardView.cardPower += 1;
                Debug.Log("El nuevo valor de cardPower para " + cardView.cardName + " es: " + cardView.cardPower);
                powerZoneManager1.AddCardPower2(cardView.cardZone, 1);
            }
        }
    }
    void Increase3()
    {
        foreach (Transform child in ar.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardKind == 1)
            {
                cardView.cardPower += 2;
                Debug.Log("El nuevo valor de cardPower para " + cardView.cardName + " es: " + cardView.cardPower);
                powerZoneManager.AddCardPower(cardView.cardZone, 2);
            }
        }
    }
    void Increase4()
    {
        foreach (Transform child in ar2.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardKind == 1)
            {
                cardView.cardPower += 2;
                Debug.Log("El nuevo valor de cardPower para " + cardView.cardName + " es: " + cardView.cardPower);
                powerZoneManager1.AddCardPower2(cardView.cardZone, 2);
            }
        }
    }
}
