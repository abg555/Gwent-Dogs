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
    private bool isEnd2 = false;
    private bool cards = false;
    private bool cards2 = false;
    private bool cards3 = false;
    private bool cards4 = false;
    private bool cards5 = false;
    private bool cards6 = false;
    private bool cards7 = false;
    private bool cards8 = false;
    private bool cards9 = false;
    public DeckButton deckButton;
    public DeckButton2 deckButton2;
    public TurnButton turnButton;
    public int winPlayer;
    public int winEnemy;


    public GameObject[] specificZones;
    public GameObject[] specificZones2;
    public TMP_Text textWinner;
    public GameObject dropZone;
    public GameObject dropZone2;
    public GameObject au;
    public GameObject cl;
    public GameObject c;
    public GameObject ar;
    public GameObject as1;
    public Pluss pluss;
    public Pluss2 pluss2;
    public Pluss3 pluss3;
    public Weather weather;
    public Weather2 weather2;
    public Weather3 weather3;
    public MorePower morePower;
    public CleamWeather cleamWeather;
    public DrawCard drawCard;
    public Decoy decoy;
    public NPower nPower;
    public Row0 row0;
    public GameObject cardzone;
    public GameObject cardzone2;






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
        Cleam();
        DrawCard();
        Decoy();
        NPower();
        Row0();
        Average();
        Card();
        Card2();

        CheckAndDestroyExtraCards();
        if (!isEnd && turnButton.Count == 1 && turnButton.Count2 == 1)
        {

            // count.Update();
            // count2.Update();
            DetermineWinner();
            isEnd = true;
            turnButton.turn = false;
            // StartCoroutine(ResetGame());



        }
        if (!isEnd2 && turnButton.Count == 2 && turnButton.Count2 == 2)
        {

            // count.Update();
            // count2.Update();
            DetermineWinner();
            isEnd2 = true;
            turnButton.turn = false;
            // StartCoroutine(ResetGame());



        }


        // if (!isReallyEnd && playerArea.transform.childCount == 0 && enemyArea.transform.childCount == 0 && cementerys.Count >= 29)
        // {
        //     Debug.Log("Condiciones para DetermineFinalWinner cumplidas");

        //     Debug.Log("Llamando a DetermineFinalWinner");
        //     DetermineFinalWinner();
        //     isReallyEnd = true;
        // }


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

        foreach (Transform child in ar.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards && cardView != null && cardView.cardNumber == 9)
            {

                Debug.Log("mai");
                morePower.MorePower1();
                cards = true;
                break;
            }

        }
    }
    void Cleam()
    {
        foreach (Transform child in ar.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards2 && cardView != null && cardView.cardNumber == 4)
            {

                cleamWeather.Cleam();
                cards2 = true;
                break;
            }

        }
    }
    void DrawCard()
    {
        foreach (Transform child in c.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards3 && cardView != null && cardView.cardNumber == 11)
            {

                drawCard.Draw();
                cards3 = true;
                break;
            }

        }
    }
    void Decoy()
    {

        foreach (Transform child in c.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards4 && cardView != null && cardView.cardNumber == 12)
            {
                Debug.Log("hola");
                decoy.DecoySalchicha();
                cards4 = true;
                break;
            }

        }
    }
    void NPower()
    {

        foreach (Transform child in as1.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards5 && cardView != null && cardView.cardNumber == 21)
            {
                Debug.Log("k");
                nPower.NPowerChau();
                cards5 = true;
                break;
            }

        }
    }
    void Row0()
    {

        foreach (Transform child in c.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards6 && cardView != null && cardView.cardNumber == 15)
            {
                Debug.Log("hola");
                row0.CleamRow();
                cards6 = true;
                break;
            }

        }
    }
    void Average()
    {

        foreach (Transform child in c.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards7 && cardView != null && cardView.cardNumber == 24)
            {
                Debug.Log("hola");
                // average.Average1();
                cards7 = true;
                break;
            }

        }
    }

    void Card()
    {
        int childCount = cardzone.transform.childCount;
        int childCount2 = playerArea.transform.childCount;
        if (!cards8 && childCount == 2 && childCount2 == 8)
        {
            foreach (Transform child in cardzone.transform)
            {
                deckButton.cards.Add(child.gameObject);

                for (var i = 0; i < 2; i++)
                {
                    int randomIndex = Random.Range(0, deckButton.cards.Count);
                    GameObject playerCard = Instantiate(deckButton.cards[randomIndex], new Vector3(0, 0, 0), Quaternion.identity);
                    playerCard.transform.SetParent(playerArea.transform, false);
                    deckButton.cards.RemoveAt(randomIndex);
                    playerCard.SetActive(false);

                }
                Destroy(child.gameObject);
            }
            cards8 = true;
        }


    }
    void Card2()
    {
        int childCount = cardzone2.transform.childCount;
        int childCount2 = enemyArea.transform.childCount;
        if (!cards9 && childCount == 2 && childCount2 == 8)
        {
            foreach (Transform child in cardzone2.transform)
            {
                deckButton2.cards2.Add(child.gameObject);

                for (var i = 0; i < 2; i++)
                {
                    int randomIndex = Random.Range(0, deckButton2.cards2.Count);
                    GameObject playerCard = Instantiate(deckButton2.cards2[randomIndex], new Vector3(0, 0, 0), Quaternion.identity);
                    playerCard.transform.SetParent(enemyArea.transform, false);
                    deckButton2.cards2.RemoveAt(randomIndex);
                    playerCard.SetActive(false);

                }
                Destroy(child.gameObject);
            }
            cards9 = true;
        }


    }

    void DetermineWinner()
    {



        if (count.TotalPlayerPower > count2.TotalPlayerPower2)
        {

            winPlayer++;

            for (var i = 0; i < 2; i++)
            {
                if (deckButton.cards.Count > 0)
                {
                    int randomIndex = Random.Range(0, deckButton.cards.Count);
                    GameObject playerCard1 = Instantiate(deckButton.cards[randomIndex], new Vector3(0, 0, 0), Quaternion.identity);
                    playerCard1.transform.SetParent(playerArea.transform, false);
                    deckButton.cards.RemoveAt(randomIndex);
                    playerCard1.SetActive(true);

                }
            }
            for (var i = 0; i < 2; i++)

            {
                if (deckButton2.cards2.Count > 0)
                {
                    int randomIndex2 = Random.Range(0, deckButton2.cards2.Count);
                    GameObject enemyCard1 = Instantiate(deckButton2.cards2[randomIndex2], new Vector3(0, 0, 0), Quaternion.identity);
                    enemyCard1.transform.SetParent(enemyArea.transform, false);
                    deckButton2.cards2.RemoveAt(randomIndex2);
                    enemyCard1.SetActive(false);



                }
            }


        }
        else if (count.TotalPlayerPower < count2.TotalPlayerPower2)
        {

            winEnemy++;
            turnButton.ChangeTurn();

            for (var i = 0; i < 2; i++)
            {

                if (deckButton.cards.Count > 0)
                {
                    int randomIndex3 = Random.Range(0, deckButton.cards.Count);
                    GameObject playerCard1 = Instantiate(deckButton.cards[randomIndex3], new Vector3(0, 0, 0), Quaternion.identity);
                    playerCard1.transform.SetParent(playerArea.transform, false);
                    deckButton.cards.RemoveAt(randomIndex3);
                    playerCard1.SetActive(false);
                }

            }
            for (var i = 0; i < 2; i++)
            {

                if (deckButton2.cards2.Count > 0)
                {
                    int randomIndex23 = Random.Range(0, deckButton2.cards2.Count);
                    GameObject enemyCard1 = Instantiate(deckButton2.cards2[randomIndex23], new Vector3(0, 0, 0), Quaternion.identity);
                    enemyCard1.transform.SetParent(enemyArea.transform, false);
                    deckButton2.cards2.RemoveAt(randomIndex23);
                    enemyCard1.SetActive(true);
                }



            }



        }
        else
        {

            winPlayer++;
            winEnemy++;
            for (var i = 0; i < 2; i++)
            {

                if (deckButton.cards.Count > 0)
                {
                    int randomIndex4 = Random.Range(0, deckButton.cards.Count);
                    GameObject playerCard1 = Instantiate(deckButton.cards[randomIndex4], new Vector3(0, 0, 0), Quaternion.identity);
                    playerCard1.transform.SetParent(playerArea.transform, false);
                    deckButton.cards.RemoveAt(randomIndex4);
                    playerCard1.SetActive(true);
                }

            }
            for (var i = 0; i < 2; i++)
            {


                if (deckButton2.cards2.Count > 0)
                {
                    int randomIndex24 = Random.Range(0, deckButton2.cards2.Count);
                    GameObject enemyCard1 = Instantiate(deckButton2.cards2[randomIndex24], new Vector3(0, 0, 0), Quaternion.identity);
                    enemyCard1.transform.SetParent(enemyArea.transform, false);
                    deckButton2.cards2.RemoveAt(randomIndex24);
                    enemyCard1.SetActive(false);

                }


            }

        }






        Debug.Log(winPlayer);
        Debug.Log(winEnemy);

        MoveCardsToHolder();
        MoveCardsToHolder2();


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
        deckButton.Hand2();

    }
    void CallHand3()
    {
        deckButton.Hand3();

    }
    void CallHand2()
    {
        deckButton2.Hand3();

    }
    void CallHand4()
    {
        deckButton2.Hand4();

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

