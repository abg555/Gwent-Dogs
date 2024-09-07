using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class DeckButton2 : MonoBehaviour
{
    public GameObject Rey;
    public GameObject EnemyArea;
    public GameObject PlayerLider;
    public List<Cards> cards2 = new List<Cards>();

    public List<GameObject> gameObjects2 = new List<GameObject>();

    void Awake()
    {
        foreach (var card in gameObjects2)
        {
            cards2.Add(card.GetComponent<Cardview>().cardview);
        }
    }
    void Start()
    {

        Shuffle();
        Hand2();
        GameObject lider2 = Instantiate(Rey, new Vector3(0, 0, 0), Quaternion.identity);
        lider2.transform.SetParent(PlayerLider.transform, false);
        for (int i = 0; i < gameObjects2.Count; i++)
        {
            Cardview cardview = gameObjects2[i].GetComponent<Cardview>();
            if (cardview != null && cardview.cardview != null)
            {
                cards2.Add(cardview.cardview);
            }
        }
    }
    public new List<Cards> Find(Func<Cards, bool> function)
    {
        return cards2.Where(function).ToList();
    }
    public new void Push(Cards card)
    {
        cards2.Add(card);
    }

    public new void SendBottom(Cards card)
    {
        cards2.Insert(0, card);

    }

    public new Cards Pop()
    {
        Cards card = cards2[cards2.Count - 1];
        cards2.RemoveAt(cards2.Count - 1);
        return card;
    }

    public new void Remove(Cards card)
    {
        cards2.Remove(card);
    }

    public new void Shuffle()
    {
        cards2 = cards2.OrderBy(x => UnityEngine.Random.value).ToList();
    }

    void OnClick2()
    {
        if (gameObjects2.Count > 0)
        {
            int randomIndex2 = UnityEngine.Random.Range(0, gameObjects2.Count);
            GameObject enemyCard = Instantiate(gameObjects2[randomIndex2], new Vector3(0, 0, 0), Quaternion.identity);
            enemyCard.transform.SetParent(EnemyArea.transform, false);
            gameObjects2.RemoveAt(randomIndex2);
        }
        else
        {
            Debug.Log("No hay m�s cartas en el mazo.");
        }
    }
    public void Hand2()
    {
        for (var i = 0; i < 9; i++)
        {
            int randomIndex2 = UnityEngine.Random.Range(0, gameObjects2.Count);
            GameObject enemyCard1 = Instantiate(gameObjects2[randomIndex2], new Vector3(0, 0, 0), Quaternion.identity);
            enemyCard1.transform.SetParent(EnemyArea.transform, false);
            Cardview cardview = enemyCard1.GetComponent<Cardview>();

            gameObjects2.RemoveAt(randomIndex2);
            enemyCard1.SetActive(false);
        }
    }
    public void Hand3()
    {
        for (var i = 0; i < 2; i++)
        {
            if (gameObjects2.Count > 0)
            {
                int randomIndex2 = UnityEngine.Random.Range(0, gameObjects2.Count);
                GameObject card = gameObjects2[randomIndex2];
                GameObject enemyCard1 = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
                enemyCard1.transform.SetParent(EnemyArea.transform, false);
                Cardview cardview = enemyCard1.GetComponent<Cardview>();

                gameObjects2.RemoveAt(randomIndex2);
                enemyCard1.SetActive(true);
            }
        }
    }
    public void Hand4()
    {
        for (var i = 0; i < 2; i++)
        {
            if (gameObjects2.Count > 0)
            {
                int randomIndex2 = UnityEngine.Random.Range(0, gameObjects2.Count);
                GameObject card = gameObjects2[randomIndex2];
                GameObject enemyCard1 = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
                enemyCard1.transform.SetParent(EnemyArea.transform, false);
                Cardview cardview = enemyCard1.GetComponent<Cardview>();

                gameObjects2.RemoveAt(randomIndex2);
                enemyCard1.SetActive(false);
            }
            else
            {
                Debug.Log("No hay más cartas en gameObjects2");
                break;
            }
        }
    }
}

