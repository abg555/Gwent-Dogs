using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy : MonoBehaviour
{
    public GameObject c;
    public GameObject playerArea;
    public PowerZoneManager powerZoneManager;



    public void DecoySalchicha()
    {
        int childCount = c.transform.childCount;

        if (childCount > 0)
        {
            Debug.Log("hi");
            Transform randomChild = null;
            Cardview cardView = null;
            int attempts = 0;
            int maxAttempts = 15;

            while (childCount > 0 && (cardView == null || cardView.cardKind != 1) && attempts < maxAttempts)
            {

                int randomIndex = Random.Range(0, childCount);
                randomChild = c.transform.GetChild(randomIndex);
                cardView = randomChild.GetComponent<Cardview>();
                if (cardView == null || cardView.cardKind != 1)
                {
                    Debug.Log("Buscando otra carta...");

                }
            }

            if (cardView != null && cardView.cardKind == 1)
            {
                Debug.Log("hi2");
                GameObject newCard = Instantiate(randomChild.gameObject, playerArea.transform.position, Quaternion.identity);

                newCard.transform.SetParent(playerArea.transform, false);

                powerZoneManager.RemoveCardPower("c", cardView.cardPower);
                newCard.SetActive(false);


                Destroy(randomChild.gameObject);
            }

        }



    }
}
