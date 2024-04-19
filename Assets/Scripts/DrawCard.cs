using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCard : MonoBehaviour
{
    public GameObject enemyArea;
    public GameObject playerArea;
    public DeckButton deckButton;



    public void Draw()
    {
        int childCount = enemyArea.transform.childCount;

        if (childCount > 0)
        {

            Transform randomChild = null;
            Cardview cardView = null;
            int attempts = 0;
            int maxAttempts = 100;

            while (childCount > 0 && (cardView == null || deckButton.cards.Find(card => card.GetComponent<Cardview>().cardview.cardName == cardView.cardName) == null) && attempts < maxAttempts)
            {
                int randomIndex = Random.Range(0, childCount);
                randomChild = enemyArea.transform.GetChild(randomIndex);
                cardView = randomChild.GetComponent<Cardview>();
            }



            if (cardView != null && deckButton.cards.Find(card => card.GetComponent<Cardview>().cardview.cardName == cardView.cardName) != null)
            {

                GameObject matchingCard = deckButton.cards.Find(card => card.GetComponent<Cardview>().cardview.cardName == cardView.cardName);


                if (matchingCard != null)
                {
                    Cards matchingCard2 = matchingCard.GetComponent<Cardview>().cardview;
                    GameObject newCard = Instantiate(matchingCard, new Vector3(0, 0, 0), Quaternion.identity);

                    newCard.transform.SetParent(playerArea.transform, false);

                    Cardview newCardView = newCard.GetComponent<Cardview>();
                    newCardView.cardview = matchingCard2;
                    newCard.SetActive(false);


                    Destroy(randomChild.gameObject);
                }
            }
            else
            {
                Debug.Log("No hay más cartas en enemyArea para dibujar.");
            }
        }

    }
}
