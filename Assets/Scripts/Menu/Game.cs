using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public abstract class Game : MonoBehaviour
{
    public List<Cards> cards = new List<Cards>();

    public List<Cards> Find(Func<Cards, bool> function)
    {
        return cards.Where(function).ToList();
    }
    public void Push(Cards card)
    {
        cards.Add(card);
    }

    public void SendBottom(Cards card)
    {
        cards.Insert(0, card);
    }

    public Cards Pop()
    {
        Cards card = cards[cards.Count - 1];
        cards.RemoveAt(cards.Count - 1);
        return card;
    }

    public void Remove(Cards card)
    {
        cards.Remove(card);
    }

    public void Shuffle()
    {
        cards = cards.OrderBy(x => UnityEngine.Random.value).ToList();
    }

}
