using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
public class Hand : MonoBehaviour
{
    public List<Cards> cards = new List<Cards>();
    public GameObject prefab;

    public void Push(Cards card)
    {
        cards.Add(card);
        GameObject card2 = Instantiate(card.gameObject);
        card2.transform.SetParent(prefab.transform);
    }

    public void SendBottom(Cards card)
    {
        cards.Insert(0, card);
        GameObject card2 = Instantiate(card.gameObject);
        card2.transform.SetParent(prefab.transform);
    }

    public Cards Pop()
    {
        Cards card = cards[cards.Count - 1];
        Remove(card);
        return card;
    }

    public void Remove(Cards card)
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

    public void Shuffle()
    {
        cards = cards.OrderBy(x => Random.value).ToList();
    }
}
