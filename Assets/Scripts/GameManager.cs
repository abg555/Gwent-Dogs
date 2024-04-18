using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    private bool cards = false;
    private bool isReallyEnd = false;
    public DeckButton deckButton;
    public DeckButton2 deckButton2;
    public TurnButton turnButton;
    public int winPlayer;
    public int winEnemy;
    public GameObject[] specificZones;
    public TMP_Text textWinner;
    public GameObject dropZone;
    public GameObject au;
    public GameObject cl;
    public GameObject c;

    public Pluss pluss;
    public Pluss2 pluss2;
    public Pluss3 pluss3;
    public Weather weather;
    public Weather2 weather2;
    public Weather3 weather3;
    public MorePower morePower;

    int cardPlayer;
    int cardDropZone;
    private int previousChildCount = 0;

    public List<GameObject> cementerys = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Sum();
        Sum2();
        Sum3();
        Weather();
        Weather2();
        Weather3();
        MorePower();
        foreach (Transform zone in dropZone.transform)
        {
            cardDropZone += zone.transform.childCount;
            Debug.Log(cardDropZone);
        }
        if (cardDropZone == 1)
        {
            Card();
            cards = true;
        }
        CheckAndDestroyExtraCards();
        if (!isEnd && playerArea.transform.childCount == 0 && enemyArea.transform.childCount == 0)
        {
            int cardInZone = 0;
            foreach (GameObject zone in specificZones)
            {
                cardInZone += zone.transform.childCount;
            }
            if (cardInZone >= 9)
            {
                count.Update();
                count2.Update();
                DetermineWinner();
                isEnd = true;
                StartCoroutine(ResetGame());

            }
        }


        if (!isReallyEnd && playerArea.transform.childCount == 0 && enemyArea.transform.childCount == 0 && cementerys.Count >= 29)
        {
            Debug.Log("Condiciones para DetermineFinalWinner cumplidas");

            Debug.Log("Llamando a DetermineFinalWinner");
            DetermineFinalWinner();
            isReallyEnd = true;
        }


    }

    void Card()
    {
        Debug.Log("Iniciando el método Card()...");
        turnButton.shouldChangeTurn = false;

        foreach (Transform zone in playerArea.transform)
        {
            cardPlayer += zone.transform.childCount;
            Debug.Log(cardPlayer);
        }
        int cardInZone = 0;
        foreach (GameObject zone in specificZones)
        {
            cardInZone += zone.transform.childCount;
        }

        if (cardDropZone == 1 && cardPlayer == 9 && cardInZone == 0)
        {
            foreach (Transform child in dropZone.transform)
            {
                GameObject card = child.gameObject;
                deckButton.cards.Add(card);
                Destroy(card);
                int randomIndex = Random.Range(0, deckButton.cards.Count);
                GameObject playerCard1 = Instantiate(deckButton.cards[randomIndex], new Vector3(0, 0, 0), Quaternion.identity);
                playerCard1.transform.SetParent(deckButton.PlayerArea.transform, false);
                deckButton.cards.RemoveAt(randomIndex);
                playerCard1.SetActive(true);
            }
        }
        turnButton.shouldChangeTurn = true;
    }

    void Sum()
    {
        foreach (Transform child in au.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardHability == "aumento")
            {

                pluss.Plusss();
                break;
            }

        }
    }

    void Sum2()
    {
        foreach (Transform child in au.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardHability == "aumento2")
            {

                pluss2.Plusss2();
                break;
            }

        }
    }
    void Sum3()
    {
        foreach (Transform child in au.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardHability == "ssw")
            {

                pluss3.Plusss3();
                break;
            }

        }
    }

    void Weather()
    {

        foreach (Transform child in cl.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardHability == "Reduce en dos")
            {

                weather.Weather1();
                break;
            }

        }
    }

    void Weather2()
    {

        foreach (Transform child in cl.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardNumber == 2)
            {

                weather2.Weather22();
                break;
            }

        }
    }
    void Weather3()
    {

        foreach (Transform child in cl.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardNumber == 3)
            {

                weather3.Weather33();
                break;
            }

        }
    }
    void MorePower()
    {
        Debug.Log("hola");
        foreach (Transform child in c.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardNumber == 11)
            {

                morePower.MorePower1();
                break;
            }

        }
    }


    void DetermineWinner()
    {



        if (count.TotalPlayerPower > count2.TotalPlayerPower2)
        {

            winPlayer++;

        }
        else if (count.TotalPlayerPower < count2.TotalPlayerPower2)
        {

            winEnemy++;
            turnButton.isPlayerTurn = false;
            CallHand2();


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
            textWinner.text = "Gano Player";


        }
        else if (winPlayer < winEnemy)
        {
            Debug.Log("gana enemy");
            textWinner.text = "Gano enemy";


        }
        else
        {
            Debug.Log("empate");
            textWinner.text = "empate";



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

    void CheckAndDestroyExtraCards()
    {

        if (playerArea.transform.childCount > 10)
        {
            int extraCardsCount = playerArea.transform.childCount - 10;
            for (int i = 0; i < extraCardsCount; i++)
            {
                GameObject extraCard = playerArea.transform.GetChild(playerArea.transform.childCount - 1).gameObject;


                cementerys.Add(extraCard);
                Debug.Log("a;adida" + extraCard.name);
                Destroy(playerArea.transform.GetChild(playerArea.transform.childCount - 1).gameObject);
            }
        }


        if (enemyArea.transform.childCount > 10)
        {
            int extraCardsCount = enemyArea.transform.childCount - 10;
            for (int i = 0; i < extraCardsCount; i++)
            {
                GameObject extraCard = enemyArea.transform.GetChild(enemyArea.transform.childCount - 1).gameObject;


                cementerys.Add(extraCard);
                Destroy(enemyArea.transform.GetChild(enemyArea.transform.childCount - 1).gameObject);
            }
        }

        if (cl.transform.childCount > 3)
        {
            int extraCardsCount = cl.transform.childCount - 3;
            for (int i = 0; i < extraCardsCount; i++)
            {
                GameObject extraCard = cl.transform.GetChild(cl.transform.childCount - 1).gameObject;

                Cardview cardParentComponent = extraCard.GetComponent<Cardview>();

                if (cardParentComponent.cardParent == 1)
                {
                    // Devuelve la carta a la posición player area
                    extraCard.transform.SetParent(playerArea.transform, false);
                    extraCard.SetActive(false);

                    Debug.Log("Devuelta: " + extraCard.name);
                }
                else if (cardParentComponent.cardParent == 2)
                {
                    // Devuelve la carta a la posición enemy area
                    extraCard.transform.SetParent(enemyArea.transform, false);
                    Debug.Log("Devuelta: " + extraCard.name);
                    extraCard.SetActive(false);
                }


            }
        }
    }

}

