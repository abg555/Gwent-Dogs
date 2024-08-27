using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using Unity.Collections;

public class Board : MonoBehaviour
{
    List<Cards> cards = new List<Cards>();
    public TMP_Text text;


    public void Push(Cards card)
    {
        cards.Add(card);
        int number = Number();
        text.text = number + 1.ToString();

    }
    public int Number()
    {
        string number = text.text.ToString();
        int number2 = Convert.ToInt32(number);
        return number2;
    }

    public void SendBottom(Cards card)
    {
        cards.Insert(0, card);
        int number = Number();
        text.text = number + 1.ToString();

    }
    public Cards Pop()
    {
        Cards card = cards[cards.Count - 1];
        cards.RemoveAt(cards.Count - 1);
        int number = Number();
        text.text = number--.ToString();
        return card;
    }

    public void Remove(Cards card)
    {
        int number = Number();
        text.text = number--.ToString();
        cards.Remove(card);
    }

    public void Shuffle()
    {
        cards = cards.OrderBy(x => UnityEngine.Random.value).ToList();
    }
}
