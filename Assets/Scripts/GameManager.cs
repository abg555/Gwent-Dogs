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
    private bool isEnd3 = false;
    private bool cards = false;
    private bool cards2 = false;
    private bool cards3 = false;
    private bool cards4 = false;
    private bool cards5 = false;
    private bool cards6 = false;
    private bool cards7 = false;
    private bool cards8 = false;
    private bool cards9 = false;
    private bool cards10 = false;
    private bool cards11 = false;
    private bool cards12 = false;
    private bool cards13 = false;
    private bool cards14 = false;
    private bool cards15 = false;
    private bool cards16 = false;
    private bool cards17 = false;
    private bool cards18 = false;
    private bool cards19 = false;
    private bool cards20 = false;
    private bool cards21 = false;
    private bool cards22 = false;
    private bool cards23 = false;
    private bool cards24 = false;
    private bool cards25 = false;
    private bool cards26 = false;
    private bool cards27 = false;
    private bool cards30 = false;

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
    public GameObject au2;
    public GameObject cl;
    public GameObject c;
    public GameObject c2;
    public GameObject ar;
    public GameObject ar2;
    public GameObject as1;
    public GameObject as2;
    public Pluss pluss;
    public Pluss2 pluss2;
    public Pluss3 pluss3;
    public Pluss4 pluss4;
    public Pluss5 pluss5;
    public Pluss6 pluss6;
    public Weather weather;
    public Weather2 weather2;
    public Weather3 weather3;
    public Weather4 weather4;
    public Weather5 weather5;
    public Weather6 weather6;
    public MorePower morePower;
    public MorePower2 morePower2;
    public MorePower3 morePower3;
    public MorePower4 morePower4;
    public LessPower lessPower;
    public LessPower2 lessPower2;
    public LessPower3 lessPower3;
    public LessPower4 lessPower4;
    public CleamWeather cleamWeather;
    public CleamWeather2 cleamWeather2;
    public CleamWeather3 cleamWeather3;
    public CleamWeather4 cleamWeather4;
    public DrawCard drawCard3;
    public DrawCard2 drawCard2;
    public Decoy decoy;
    public Decoy2 decoy2;
    public NPower nPower;
    public NPower2 nPower1;
    public NPower3 nPower2;
    public NPower4 nPower3;
    public NPower5 nPower4;
    public NPower6 nPower5;
    public Row0 row0;
    public Row02 row02;
    public Average average;
    public Average2 average2;

    public GameObject cardzone;
    public GameObject cardzone2;
    public GameObject playerButton;
    public GameObject playerButton2;
    public GameObject playerButton3;
    public GameObject buttZone;






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
        Sum4();
        Sum5();
        Sum6();
        Weather();
        Weather2();
        Weather3();
        Weather4();
        Weather5();
        Weather6();
        MorePower();
        MorePower2();
        MorePower3();
        MorePower4();
        LessPower();
        LessPower2();
        LessPower3();
        LessPower4();
        Cleam();
        Cleam2();
        Cleam3();
        Cleam4();
        DrawCard();
        DrawCard2();
        Decoy();
        Decoy2();
        NPower();
        NPower2();
        NPower3();
        NPower4();
        NPower5();
        NPower6();
        Row0();
        Row02();
        Average();
        Average2();
        Card();
        Card2();

        CheckAndDestroyExtraCards();
        if (!isEnd && turnButton.Count == 1 && turnButton.Count2 == 1)
        {

            count.Update();
            count2.Update();
            DetermineWinner();
            isEnd = true;
            turnButton.turn = false;
            // StartCoroutine(ResetGame());



        }
        if (!isEnd2 && turnButton.Count == 2 && turnButton.Count2 == 2)
        {

            count.Update();
            count2.Update();
            DetermineWinner();

            isEnd2 = true;
            turnButton.turn = false;
            // StartCoroutine(ResetGame());






        }
        if (!isEnd3 && turnButton.Count == 3 && turnButton.Count2 == 3)
        {

            count.Update();
            count2.Update();
            DetermineWinner();
            isEnd3 = true;
            turnButton.turn = false;
            DetermineFinalWinner();
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
            if (cardView != null && cardView.cardNumber == 6)
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
            if (cardView != null && cardView.cardNumber == 7)
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
            if (cardView != null && cardView.cardNumber == 8)
            {

                pluss3.Plusss3();
                break;
            }

        }
    }
    void Sum4()
    {
        foreach (Transform child in au2.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardNumber == 32)
            {

                pluss4.Plusss();
                break;
            }

        }
    }
    void Sum5()
    {
        foreach (Transform child in au2.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardNumber == 33)
            {

                pluss5.Plusss();
                break;
            }

        }
    }
    void Sum6()
    {
        foreach (Transform child in au2.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardNumber == 34)
            {

                pluss6.Plusss3();
                break;
            }

        }
    }

    void Weather()
    {

        foreach (Transform child in cl.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardNumber == 1)
            {

                weather.Weather1();
                break;
            }

        }
    }
    void Weather4()
    {

        foreach (Transform child in cl.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardNumber == 27)
            {

                weather4.Weather1();
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
    void Weather5()
    {

        foreach (Transform child in cl.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardNumber == 28)
            {

                weather5.Weather22();
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
    void Weather6()
    {

        foreach (Transform child in cl.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardNumber == 29)
            {

                weather6.Weather33();
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
    void MorePower2()
    {

        foreach (Transform child in ar.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards13 && cardView != null && cardView.cardNumber == 10)
            {

                Debug.Log("mai");
                morePower2.MorePower1();
                cards13 = true;
                break;
            }

        }
    }
    void MorePower3()
    {

        foreach (Transform child in ar2.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards14 && cardView != null && cardView.cardNumber == 35)
            {

                Debug.Log("mai");
                morePower3.MorePower1();
                cards14 = true;
                break;
            }

        }
    }
    void MorePower4()
    {

        foreach (Transform child in ar2.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards15 && cardView != null && cardView.cardNumber == 36)
            {

                Debug.Log("mai");
                morePower4.MorePower1();
                cards15 = true;
                break;
            }

        }
    }
    void LessPower()
    {

        foreach (Transform child in as1.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards23 && cardView != null && cardView.cardNumber == 19)
            {

                Debug.Log("mai");
                lessPower.LessPower1();
                cards23 = true;
                break;
            }

        }
    }
    void LessPower2()
    {

        foreach (Transform child in as1.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards24 && cardView != null && cardView.cardNumber == 20)
            {

                Debug.Log("mai");
                lessPower2.LessPower1();
                cards24 = true;
                break;
            }

        }
    }
    void LessPower3()
    {

        foreach (Transform child in as2.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards25 && cardView != null && cardView.cardNumber == 47)
            {

                Debug.Log("mai");
                lessPower3.LessPower1();
                cards25 = true;
                break;
            }

        }
    }
    void LessPower4()
    {

        foreach (Transform child in as2.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards26 && cardView != null && cardView.cardNumber == 48)
            {

                Debug.Log("mai");
                lessPower4.LessPower1();
                cards26 = true;
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
    void Cleam2()
    {
        foreach (Transform child in ar2.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards11 && cardView != null && cardView.cardNumber == 30)
            {

                cleamWeather2.Cleam();
                cards11 = true;
                break;
            }

        }
    }
    void Cleam3()
    {
        foreach (Transform child in as1.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards10 && cardView != null && cardView.cardNumber == 5)
            {

                cleamWeather3.Cleam();
                cards10 = true;
                break;
            }

        }
    }
    void Cleam4()
    {
        foreach (Transform child in as2.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards12 && cardView != null && cardView.cardNumber == 31)
            {

                cleamWeather4.Cleam();
                cards12 = true;
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

                drawCard3.Draw();
                cards3 = true;
                break;
            }

        }
    }
    void DrawCard2()
    {
        foreach (Transform child in c2.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards16 && cardView != null && cardView.cardNumber == 37)
            {

                drawCard2.Draw();
                cards16 = true;
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
    void Decoy2()
    {

        foreach (Transform child in c2.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards17 && cardView != null && cardView.cardNumber == 38)
            {
                Debug.Log("hola");
                decoy2.DecoySalchicha();
                cards17 = true;
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
    void NPower2()
    {

        foreach (Transform child in as1.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards18 && cardView != null && cardView.cardNumber == 22)
            {
                Debug.Log("k");
                nPower1.NPowerChau();
                cards18 = true;
                break;
            }

        }
    }
    void NPower3()
    {

        foreach (Transform child in as1.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards19 && cardView != null && cardView.cardNumber == 52)
            {
                Debug.Log("k");
                nPower2.NPowerChau();
                cards19 = true;
                break;
            }

        }
    }
    void NPower4()
    {

        foreach (Transform child in as2.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards20 && cardView != null && cardView.cardNumber == 45)
            {
                Debug.Log("k");
                nPower3.NPowerChau();
                cards20 = true;
                break;
            }

        }
    }
    void NPower5()
    {

        foreach (Transform child in as2.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards21 && cardView != null && cardView.cardNumber == 46)
            {
                Debug.Log("k");
                nPower4.NPowerChau();
                cards21 = true;
                break;
            }

        }
    }
    void NPower6()
    {

        foreach (Transform child in as2.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards22 && cardView != null && cardView.cardNumber == 53)
            {
                Debug.Log("k");
                nPower5.NPowerChau();
                cards22 = true;
                break;
            }

        }
    }
    void Row0()
    {

        foreach (Transform child in c.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards6 && cardView != null && cardView.cardNumber == 24)
            {
                Debug.Log("hola");
                row0.CleamRow();
                cards6 = true;
                break;
            }

        }
    }
    void Row02()
    {

        foreach (Transform child in c2.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards27 && cardView != null && cardView.cardNumber == 50)
            {
                Debug.Log("hola");
                row02.CleamRow();
                cards27 = true;
                break;
            }

        }
    }

    void Average()
    {

        foreach (Transform child in c.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards7 && cardView != null && cardView.cardNumber == 15)
            {
                Debug.Log("hola");
                average.Average1();
                cards7 = true;
                break;
            }

        }
    }
    void Average2()
    {

        foreach (Transform child in c2.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (!cards30 && cardView != null && cardView.cardNumber == 41)
            {
                Debug.Log("hola");
                average2.Average1();
                cards30 = true;
                break;
            }

        }
    }

    void Card()
    {

        int childCount = cardzone.transform.childCount;
        int childCount2 = playerArea.transform.childCount;
        if (!cards8 && childCount == 2 && childCount2 == 7)
        {
            List<GameObject> childrenToDestroy = new List<GameObject>();
            foreach (Transform child in cardzone.transform)
            {
                GameObject card = Instantiate(child.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
                deckButton.cards.Add(card);

                for (var i = 0; i < 1; i++)
                {
                    int randomIndex = Random.Range(0, deckButton.cards.Count);
                    GameObject playerCard = Instantiate(deckButton.cards[randomIndex], new Vector3(0, 0, 0), Quaternion.identity);
                    playerCard.transform.SetParent(playerArea.transform, false);
                    deckButton.cards.RemoveAt(randomIndex);
                    playerCard.SetActive(false);
                }
                childrenToDestroy.Add(child.gameObject);
            }
            foreach (GameObject child in childrenToDestroy)
            {
                Destroy(child);
            }
            cards8 = true;

        }
    }
    void Card2()
    {


        int childCount = cardzone2.transform.childCount;
        int childCount2 = enemyArea.transform.childCount;
        if (!cards9 && childCount == 2 && childCount2 == 7)
        {
            List<GameObject> childrenToDestroy = new List<GameObject>();
            foreach (Transform child in cardzone2.transform)
            {
                GameObject card = Instantiate(child.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
                deckButton2.cards2.Add(card);

                for (var i = 0; i < 1; i++)
                {
                    int randomIndex = Random.Range(0, deckButton2.cards2.Count);
                    GameObject playerCard = Instantiate(deckButton2.cards2[randomIndex], new Vector3(0, 0, 0), Quaternion.identity);
                    playerCard.transform.SetParent(enemyArea.transform, false);
                    deckButton2.cards2.RemoveAt(randomIndex);
                    playerCard.SetActive(false);
                }
                childrenToDestroy.Add(child.gameObject);
            }
            foreach (GameObject child in childrenToDestroy)
            {
                Destroy(child);
            }
            cards9 = true;
        }
    }

    void DetermineWinner()
    {



        if (count.TotalPlayerPower > count2.TotalPlayerPower2)
        {

            winPlayer++;
            CallHand4();
            CallHand();


        }
        else if (count.TotalPlayerPower < count2.TotalPlayerPower2)
        {

            winEnemy++;
            CallHand4();
            CallHand();
            turnButton.ChangeTurn();



        }
        else
        {

            winPlayer++;
            winEnemy++;
            CallHand4();
            CallHand();


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
            GameObject butt = Instantiate(playerButton2, new Vector3(0, 0, 0), Quaternion.identity);

            butt.transform.SetParent(buttZone.transform, false);
            butt.transform.position = new Vector3(butt.transform.position.x + 451, butt.transform.position.y, butt.transform.position.z);



        }
        else if (winPlayer < winEnemy)
        {
            Debug.Log("gana enemy");
            textWinner.text = "Gano enemy";
            GameObject butt = Instantiate(playerButton3, new Vector3(0, 0, 0), Quaternion.identity);

            butt.transform.SetParent(buttZone.transform, false);
            butt.transform.position = new Vector3(butt.transform.position.x + 451, butt.transform.position.y, butt.transform.position.z);

        }
        else
        {
            Debug.Log("empate");

            GameObject butt = Instantiate(playerButton, new Vector3(0, 0, 0), Quaternion.identity);

            butt.transform.SetParent(buttZone.transform, false);
            butt.transform.position = new Vector3(butt.transform.position.x + 451, butt.transform.position.y, butt.transform.position.z);

        }
    }
    void MoveCardsToHolder()
    {
        foreach (GameObject position in positions)
        {
            if (position == null)
            {
                Debug.Log("La posición es null.");
                continue;
            }

            List<GameObject> childrenToDestroy = new List<GameObject>();
            foreach (Transform child in position.transform)
            {
                if (child == null)
                {
                    Debug.Log("El hijo es null.");
                    continue;
                }


                string zoneName = child.GetComponent<Cardview>().cardZone;

                int cardPower = child.GetComponent<Cardview>().cardPower;



                position.GetComponent<PowerZoneManager>().RemoveCardPower(zoneName, cardPower);

                childrenToDestroy.Add(child.gameObject);


            }

            foreach (GameObject child in childrenToDestroy)
            {
                Destroy(child);
            }


        }
    }

    void MoveCardsToHolder2()
    {

        foreach (GameObject position in positions2)
        {
            if (position == null)
            {
                Debug.Log("La posición es null.");
                continue;
            }

            List<GameObject> childrenToDestroy = new List<GameObject>();
            foreach (Transform child in position.transform)
            {
                if (child == null)
                {
                    Debug.Log("El hijo es null.");
                    continue;
                }
                string zoneName = child.GetComponent<Cardview>().cardZone;

                int cardPower = child.GetComponent<Cardview>().cardPower;



                position.GetComponent<PowerZoneManager2>().RemoveCardPower2(zoneName, cardPower);

                childrenToDestroy.Add(child.gameObject);

            }

            foreach (GameObject child in childrenToDestroy)
            {
                Destroy(child);
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

