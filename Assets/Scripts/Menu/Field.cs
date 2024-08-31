using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.Data;

public class Field : Game
{
    public GameObject cards1;
    public GameObject cards2;

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
        Delete(card);
        return card;
    }

    public new void Remove(Cards card)
    {
        Delete(card);
        cards.Remove(card);
    }

    public new void Shuffle()
    {
        cards = cards.OrderBy(x => UnityEngine.Random.value).ToList();
    }
    public void Delete(Cards card)
    {
        foreach (Transform transform in cards1.transform)
        {
            foreach (Transform trans in transform)
            {
                if (trans.gameObject == card)
                {
                    Destroy(trans.gameObject);
                    break;
                }
            }
        }
        foreach (Transform transform in cards2.transform)
        {
            foreach (Transform trans in transform)
            {
                if (trans.gameObject == card)
                {
                    Destroy(trans.gameObject);
                    break;
                }
            }
        }
    }
}
