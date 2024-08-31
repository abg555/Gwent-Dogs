using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
using Unity.VisualScripting;

public class DeckButton : Game
{
    /*son las referencias de cada carta*/
    public GameObject Rey;
    public GameObject PlayerArea;
    public GameObject PlayerLider;
    public GameObject specificDropZone;
    public List<GameObject> gameObjects = new List<GameObject>();
    public new List<Cards> cards = new List<Cards>();



    void Awake()
    {
        foreach (var card in gameObjects)
        {
            cards.Add(card.GetComponent<Cardview>().cardview);
        }
    }

    void Start()
    {

        Shuffle();
        GameObject lider = Instantiate(Rey, new Vector3(0, 0, 0), Quaternion.identity);
        lider.transform.SetParent(PlayerLider.transform, false);   /*al iniciar el juego instancia la carta lier en su pocision */
        Hand();

        for (int i = 0; i < gameObjects.Count; i++)
        {
            Cardview cardview = gameObjects[i].GetComponent<Cardview>();
            if (cardview != null && cardview.cardview != null)
            {
                cards.Add(cardview.cardview);
            }
        }
    }
    public new List<Cards> Find(Func<Cards, bool> function)
    {
        return cards.Where(function).ToList();
    }
    public new void Push(Cards card)
    {
        cards.Add(card);
    }

    public new void SendBottom(Cards card)
    {
        cards.Insert(0, card);
    }

    public new Cards Pop()
    {
        Cards card = cards[cards.Count - 1];
        cards.RemoveAt(cards.Count - 1);
        return card;
    }

    public new void Remove(Cards card)
    {
        cards.Remove(card);
    }

    public new void Shuffle()
    {
        cards = cards.OrderBy(x => UnityEngine.Random.value).ToList();
    }








    void OnClick()
    {
        if (gameObjects.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, gameObjects.Count);
            GameObject playerCard = Instantiate(gameObjects[randomIndex], new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.transform.SetParent(PlayerArea.transform, false);
            gameObjects.RemoveAt(randomIndex);
        }
        else
        {
            Debug.Log("No hay m�s cartas en el mazo.");
        }
    }

    public void Hand()
    {
        for (var i = 0; i < 9; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, gameObjects.Count);
            GameObject playerCard1 = Instantiate(gameObjects[randomIndex], new Vector3(0, 0, 0), Quaternion.identity);
            playerCard1.transform.SetParent(PlayerArea.transform, false);
            Cardview cardview = playerCard1.GetComponent<Cardview>();

            gameObjects.RemoveAt(randomIndex);
            playerCard1.SetActive(true);
            /*Selecciona 9 cartas random y las instancia en la posicion playerArea haciendo esta posicion su padre despues las elimina de la lista gameObjects y las gace visibles*/
        }
    }
    public void Hand2()
    {
        for (var i = 0; i < 2; i++)
        {
            if (gameObjects.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, gameObjects.Count);
                GameObject card = gameObjects[randomIndex];
                GameObject playerCard1 = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
                playerCard1.transform.SetParent(PlayerArea.transform, false);
                Cardview cardview = playerCard1.GetComponent<Cardview>();

                gameObjects.RemoveAt(randomIndex);
                playerCard1.SetActive(true);
            }
            /*Selecciona 2 cartas random y las instancia en la posicion playerArea haciendo esta posicion su padre despues las elimina de la lista gameObjects y las gace visibles*/
        }
    }
    public void Hand3()
    {
        for (var i = 0; i < 2; i++)
        {
            if (gameObjects.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, gameObjects.Count);
                GameObject card = gameObjects[randomIndex];
                GameObject playerCard1 = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
                playerCard1.transform.SetParent(PlayerArea.transform, false);
                Cardview cardview = playerCard1.GetComponent<Cardview>();

                gameObjects.RemoveAt(randomIndex);
                playerCard1.SetActive(false);
            }
        }
        /*Selecciona 2 cartas random y las instancia en la posicion playerArea haciendo esta posicion su padre despues las elimina de la lista gameObjects y las gace invisibles*/

    }




}



