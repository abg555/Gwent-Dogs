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
    private bool isReallyEnd = false;
    public DeckButton deckButton;
    public DeckButton2 deckButton2;
    public TurnButton turnButton;
    public int winPlayer;
    public int winEnemy;
    public GameObject[] specificZones;

    public List<GameObject> cementerys = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        turnButton.isPlayerTurn = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isEnd && playerArea.transform.childCount == 0 && enemyArea.transform.childCount == 0)
        {
            int cardInZone = 0;
            foreach (GameObject zone in specificZones)
            {
                cardInZone += zone.transform.childCount;
            }
            if (cardInZone >= 10)
            {
                count.Update();
                count2.Update();
                DetermineWinner();
                isEnd = true;
                StartCoroutine(ResetGame());

            }
        }
        if (!isReallyEnd && playerArea.transform.childCount == 0 && enemyArea.transform.childCount == 0 && cementerys.Count >= 30)
        {
            Debug.Log("Condiciones para DetermineFinalWinner cumplidas");
            int cardInZone2 = 0;
            foreach (GameObject zone in specificZones)
            {
                cardInZone2 += zone.transform.childCount;
            }
            if (cardInZone2 >= 10)
            {
                Debug.Log("Llamando a DetermineFinalWinner");
                DetermineFinalWinner();
                isReallyEnd = true;
            }
        }


    }
    // void DetermineWinner()
    // {
    //     if (count.TotalPlayerPower > count2.TotalPlayerPower2)
    //     {
    //         Debug.Log("gana player");
    //         Invoke("MoveCardsToHolder", 3.0f);
    //         Invoke("MoveCardsToHolder2", 3.0f);
    //         Invoke("CallHand", 3.0f);
    //         Invoke("CallHand2", 3.0f);



    //     }
    //     if (count.TotalPlayerPower < count2.TotalPlayerPower2)
    //     {
    //         Debug.Log("gana enemy");
    //         Invoke("MoveCardsToHolder", 3.0f);
    //         Invoke("MoveCardsToHolder2", 3.0f);
    //         Invoke("CallHand", 3.0f);
    //         Invoke("CallHand2", 3.0f);

    //     }
    //     if (count.TotalPlayerPower == count2.TotalPlayerPower2)
    //     {
    //         Debug.Log("empate");
    //         Invoke("MoveCardsToHolder", 3.0f);
    //         Invoke("MoveCardsToHolder2", 3.0f);
    //         Invoke("CallHand", 3.0f);
    //         Invoke("CallHand2", 3.0f);

    //     }
    // }
    void DetermineWinner()
    {



        if (count.TotalPlayerPower > count2.TotalPlayerPower2)
        {

            winPlayer++;
        }
        else if (count.TotalPlayerPower < count2.TotalPlayerPower2)
        {

            winEnemy++;
        }
        else
        {
        }
        if (count.TotalPlayerPower != 0 && count2.TotalPlayerPower2 != 0 && count.TotalPlayerPower == count2.TotalPlayerPower2)
        {

            winPlayer++;
            winEnemy++;
        }
        Debug.Log(winPlayer);
        Debug.Log(winEnemy);

        Invoke("MoveCardsToHolder", 3.0f);
        Invoke("MoveCardsToHolder2", 3.0f);
        // Invoke("CallHand", 3.0f);
        // Invoke("CallHand2", 3.0f);


    }
    void DetermineFinalWinner()
    {
        if (winPlayer > winEnemy)
        {
            Debug.Log("gana player");

        }
        else if (winPlayer < winEnemy)
        {
            Debug.Log("gana enemy");

        }
        else
        {
            Debug.Log("empate");


        }
    }
    IEnumerator ResetGame()
    {

        yield return new WaitForSeconds(3.0f);
        DetermineWinner();
        CallHand();
        CallHand2();

        isEnd = false;
        turnButton.isPlayerTurn = false;
    }
    void MoveCardsToHolder()
    {

        foreach (GameObject position in positions)
        {
            foreach (Transform card in position.transform)
            {

                string zoneName = card.GetComponent<Cardview>().cardZone;

                int cardPower = card.GetComponent<Cardview>().cardPower;


                position.GetComponent<PowerZoneManager>().RemoveCardPower(zoneName, cardPower);

                
                cementerys.Add(card.gameObject);
                Destroy(card.gameObject);

            }
        }
        Debug.Log("Cementerys count: " + cementerys.Count);
    foreach (GameObject card in cementerys)
    {
        Debug.Log("Card added: " + card.name);
    }
    }
    void MoveCardsToHolder2()
    {

        foreach (GameObject position in positions2)
        {
            foreach (Transform card in position.transform)
            {

                string zoneName = card.GetComponent<Cardview>().cardZone;

                int cardPower = card.GetComponent<Cardview>().cardPower;


                position.GetComponent<PowerZoneManager2>().RemoveCardPower2(zoneName, cardPower);

                cementerys.Add(card.gameObject);
                Destroy(card.gameObject);
            }
        }

    }
    void CallHand()
    {
        deckButton.Hand();


    }
    void CallHand2()
    {
        deckButton2.Hand2();

    }



}

