using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy2 : MonoBehaviour
{
    public GameObject c2;
    public GameObject enemyArea;
    public PowerZoneManager2 powerZoneManager2;



    public void DecoySalchicha()
    {
        int childCount = c2.transform.childCount;

        if (childCount > 0)
        {
            Transform randomChild = null;
            Cardview cardView = null;
            int attempts = 0;
            int maxAttempts = 15;

            // Selecciona una carta al azar de c que tenga un cardNumber distinto de 12
            while (attempts < maxAttempts)
            {
                int randomIndex = Random.Range(0, childCount);
                randomChild = c2.transform.GetChild(randomIndex);
                cardView = randomChild.GetComponent<Cardview>();
                if (cardView != null && cardView.cardNumber != 38 && cardView.cardKind == 1)
                {
                    break;
                }
                attempts++;
            }

            if (cardView != null && cardView.cardNumber != 38 && cardView.cardKind == 1)
            {
                // Crea una nueva carta en playerArea que es una copia de la carta seleccionada

                GameObject newCard = Instantiate(randomChild.gameObject, enemyArea.transform.position, Quaternion.identity);
                newCard.transform.SetParent(enemyArea.transform, false);
                newCard.SetActive(false);

                // Destruye la carta original en c
                powerZoneManager2.RemoveCardPower2("c2", cardView.cardPower);

                Destroy(randomChild.gameObject);
            }
        }
        else
        {
            Debug.Log("No hay más cartas en c para dibujar.");
        }
    }
}
