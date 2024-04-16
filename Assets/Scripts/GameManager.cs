using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerArea;
    public GameObject enemyArea;
    public GameObject cementery;
    public GameObject[] positions;
    public GameObject cementery2;
    public GameObject[] positions2;
    public Count count;
    public Count2 count2;
    private bool isEnd = false;
    public DeckButton deckButton;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!isEnd && playerArea.transform.childCount == 0 && enemyArea.transform.childCount == 0)
        {
            count.Update();
            count2.Update();
            DetermineWinner();
            isEnd = true;

        }
    }
    void DetermineWinner()
    {
        if (count.TotalPlayerPower > count2.TotalPlayerPower2)
        {
            Debug.Log("gana player");
            Invoke("MoveCardsToHolder", 8.0f);
            Invoke("MoveCardsToHolder2", 8.0f);
            deckButton.Hand();
        }
        if (count.TotalPlayerPower < count2.TotalPlayerPower2)
        {
            Debug.Log("gana enemy");
            Invoke("MoveCardsToHolder", 8.0f);
            Invoke("MoveCardsToHolder2", 8.0f);
            deckButton.Hand();
        }
        if (count.TotalPlayerPower == count2.TotalPlayerPower2)
        {
            Debug.Log("empate");
            Invoke("MoveCardsToHolder", 8.0f);
            Invoke("MoveCardsToHolder2", 8.0f);
            deckButton.Hand();
        }
    }
    void MoveCardsToHolder()
    {

        foreach (GameObject position in positions)
        {

            foreach (Transform card in position.transform)
            {

                {
                    card.SetParent(cementery.transform);
                    card.gameObject.SetActive(false);
                }
            }
        }
    }
    void MoveCardsToHolder2()
    {

        foreach (GameObject position in positions2)
        {

            foreach (Transform card in position.transform)
            {

                {
                    Debug.Log("Moviendo carta a cementerio: " + card.name);
                    card.SetParent(cementery2.transform);
                    card.gameObject.SetActive(false);
                }
            }
        }
    }

}
