using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorePower : MonoBehaviour
{
    public GameObject[] positions;


    public void MorePower1()
    {

        GameObject maxPowerCard = null;
        float maxPower = float.MinValue;

        foreach (GameObject position in positions)
        {
            foreach (Transform child in position.transform)
            {
                Cardview cardvio = child.GetComponent<Cardview>();
                if (cardvio != null && cardvio.cardPower > maxPower)
                {
                    maxPower = cardvio.cardPower;
                    maxPowerCard = child.gameObject;
                }
            }
        }

        if (maxPowerCard != null)
        {
            Destroy(maxPowerCard);
        }

    }
}
