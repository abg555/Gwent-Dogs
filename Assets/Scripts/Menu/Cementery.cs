using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
public class Cementery : Game
{


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
}
