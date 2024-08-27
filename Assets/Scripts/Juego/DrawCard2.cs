using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCard2 : MonoBehaviour
{
    public GameObject PlayerArea;
    public GameObject EnemyArea;
    public DeckButton2 deckButton2;
    public GameManager gameManager;



    public void Draw()
    {
        int childCount = PlayerArea.transform.childCount;

        if (childCount > 0)
        {

            int randomIndex = Random.Range(0, childCount);
            Transform randomChild = PlayerArea.transform.GetChild(randomIndex);


            gameManager.cementerys.Add(randomChild.gameObject);
            Destroy(randomChild.gameObject);


            int randomCardIndex = Random.Range(0, deckButton2.cards2.Count);
            GameObject randomCard = deckButton2.gameObjects2[randomCardIndex];

            GameObject newCard = Instantiate(randomCard, new Vector3(0, 0, 0), Quaternion.identity);
            newCard.transform.SetParent(EnemyArea.transform, false);
            newCard.SetActive(false);
            Debug.Log("holiss");
        }
        else
        {
            Debug.Log("No hay más cartas en PlayerArea para dibujar.");
        }
    }
}
