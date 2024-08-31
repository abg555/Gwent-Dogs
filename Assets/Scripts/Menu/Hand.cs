using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using Unity.VisualScripting;
public class Hand : Game
{

    public GameObject prefab;


    public new List<Cards> Find(Func<Cards, bool> function)
    {
        return cards.Where(function).ToList();
    }
    public new void Push(Cards card)
    {
        cards.Add(card);
        GameObject card2 = Instantiate(card.gameObject);
        card2.transform.SetParent(prefab.transform);
    }

    public new void SendBottom(Cards card)
    {
        cards.Insert(0, card);
        GameObject card2 = Instantiate(card.gameObject);
        card2.transform.SetParent(prefab.transform);
    }

    public new Cards Pop()
    {
        Cards card = cards[cards.Count - 1];
        Remove(card);
        return card;
    }
    public new void Remove(Cards card)
    {
        foreach (Transform transform in prefab.transform)
        {
            if (transform.gameObject.GetComponent<Cards>() == card)
            {
                Destroy(transform.gameObject);
                break;
            }
        }
        cards.Remove(card);
    }

    public new void Shuffle()
    {
        cards = cards.OrderBy(x => UnityEngine.Random.value).ToList();
    }
}
